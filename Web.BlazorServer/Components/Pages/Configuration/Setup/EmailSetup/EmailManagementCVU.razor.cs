using Mapster;
using Microsoft.AspNetCore.Components;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.ViewModels.Configuration.Setup.Email;
using Web.BlazorServer.ViewModels.Enums;
using KernelEnumHelper = Shared.Kernel.EnumHelper;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.EmailSetup;

public partial class EmailManagementCVU
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter]
    public Guid Ref { get; set; } = Guid.NewGuid();

    [Parameter]
    public bool ModalMode { get; set; } = false;

    [Parameter]
    public PageActionTypeEnum? ModalAction { get; set; } = null;
    #endregion Parameters

    #region Injects
    [Inject] IEmailManagementHandler EmailManagementHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }
    bool PasswordVisibility { get; set; } = false;
    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Updating => PageAction == PageActionTypeEnum.Update;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionCreateEmail) || AppBusyService.IsBusy(ActionGetEmail) || AppBusyService.IsBusy(ActionUpdateEmail);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetEmail);

    readonly string ActionCreateEmail = KernelEnumHelper.GetEnumDescription(AppActions.CreateEmail);
    readonly string ActionGetEmail = KernelEnumHelper.GetEnumDescription(AppActions.ViewEmail);
    readonly string ActionUpdateEmail = KernelEnumHelper.GetEnumDescription(AppActions.UpdateEmail);
    #endregion Primitives

    #region Custom Classes
    EmailSetupVM? Email { get; set; } = null;
    #endregion Custom Classes

    #region Overrides
    protected override void OnParametersSet()
    {
        if (!ModalMode)
            PageAction = PageActionHelper.GetPageActionType(NavManager.Uri);
        else
        {
            if (ModalMode && ModalAction != null)
                PageAction = (PageActionTypeEnum)ModalAction;
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppBusyService.SetBusy(ActionGetEmail, true);
        if (!Can.Do("OEMS", KernelEnumHelper.GetEnumDescription(PageAction)))
            NavManager.NavigateTo("/401", true);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await LoadDataAsync();
        }
    }
    protected override async Task CancelEditing()
    {
        if (UnsavedChangesService.HasChanges)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Email Update"))
                return;
        NavManager.NavigateTo($"/configuration/setup/Email-setup/view?ref={Ref}", true);
    }

    protected override async Task HandleSubmit()
    {
        if (Creating)
            await Create();
        if (Updating)
            await Update();
    }

    protected override async Task InitializeEditing()
    {
        NavManager.NavigateTo($"/configuration/setup/email-setup/update?ref={Ref}", true);
    }
    #endregion Overrides


    #region Custom Functions
    async Task ToggleVisibility()
    {
        PasswordVisibility = !PasswordVisibility;
        await InvokeAsync(StateHasChanged);
    }

    async Task LoadDataAsync()
    {
        if (Viewing || Updating)
        {
            var action = await AppActionFactory.RunAsync(async () =>
            {
                AppBusyService.SetBusy(ActionGetEmail, true);

                var Email = await EmailManagementHandler.GetEmailAsync(Ref);
                Email.Adapt(FormData);

                AppBusyService.SetBusy(ActionGetEmail, false);

            }, AppActionOptionPresets.Loading(ActionGetEmail));
        }
        else
        {
            AppBusyService.SetBusy(ActionGetEmail, false);
        }

        await InvokeAsync(StateHasChanged);
    }

    async Task Create()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionCreateEmail, true);

            var result = await EmailManagementHandler.CreateEmailAsync(FormData);

            AppBusyService.SetBusy(ActionCreateEmail, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionCreateEmail));

        action.OnSuccess(async (args) =>
        {
            UnsavedChangesService.MarkClean();
            await Return();
        });
    }

    async Task Update()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionUpdateEmail, true);

            var result = await EmailManagementHandler.UpdateEmailAsync(FormData);

            AppBusyService.SetBusy(ActionUpdateEmail, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionUpdateEmail));

        action.OnSuccess(async (args) =>
        {
            UnsavedChangesService.MarkClean();
            await Return();
        });
    }

    async Task Return()
    {
        if (UnsavedChangesService.HasChanges)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Email Setup"))
                return;

        NavManager.NavigateTo($"/configuration/setup/email-setup", true);
    }
    #endregion Custom Functions
}
