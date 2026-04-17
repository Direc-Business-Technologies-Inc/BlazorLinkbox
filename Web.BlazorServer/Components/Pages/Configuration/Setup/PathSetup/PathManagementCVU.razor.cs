using Mapster;
using Microsoft.AspNetCore.Components;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.ViewModels.Configuration.Setup.Path;
using Web.BlazorServer.ViewModels.Enums;
using KernelEnumHelper = Shared.Kernel.EnumHelper;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.PathSetup;

public partial class PathManagementCVU
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
    [Inject] IPathManagementHandler PathManagementHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }
    bool PasswordVisibility { get; set; } = false;
    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Updating => PageAction == PageActionTypeEnum.Update;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionCreatePath) || AppBusyService.IsBusy(ActionGetPath) || AppBusyService.IsBusy(ActionUpdatePath);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetPath);

    readonly string ActionCreatePath = KernelEnumHelper.GetEnumDescription(AppActions.CreatePath);
    readonly string ActionGetPath = KernelEnumHelper.GetEnumDescription(AppActions.ViewPath);
    readonly string ActionUpdatePath = KernelEnumHelper.GetEnumDescription(AppActions.UpdatePath);
    #endregion Primitives

    #region Custom Classes
    PathSetupVM? Path { get; set; } = null;
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
        AppBusyService.SetBusy(ActionGetPath, true);
        if (!Can.Do("OPTS", KernelEnumHelper.GetEnumDescription(PageAction)))
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
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Path Update"))
                return;
        NavManager.NavigateTo($"/configuration/setup/path-setup/view?ref={Ref}", true);
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
        NavManager.NavigateTo($"/configuration/setup/path-setup/update?ref={Ref}", true);
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
                AppBusyService.SetBusy(ActionGetPath, true);

                var path = await PathManagementHandler.GetPathAsync(Ref);
                path.Adapt(FormData);

                AppBusyService.SetBusy(ActionGetPath, false);

            }, AppActionOptionPresets.Loading(ActionGetPath));
        }
        else
        {
            AppBusyService.SetBusy(ActionGetPath, false);
        }

        await InvokeAsync(StateHasChanged);
    }

    async Task Create()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionCreatePath, true);

            var result = await PathManagementHandler.CreatePathAsync(FormData);

            AppBusyService.SetBusy(ActionCreatePath, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionCreatePath));

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
            AppBusyService.SetBusy(ActionUpdatePath, true);

            var result = await PathManagementHandler.UpdatePathAsync(FormData);

            AppBusyService.SetBusy(ActionUpdatePath, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionUpdatePath));

        action.OnSuccess(async (args) =>
        {
            UnsavedChangesService.MarkClean();
            await Return();
        });
    }

    async Task Return()
    {
        if (UnsavedChangesService.HasChanges)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Path Setup"))
                return;

        NavManager.NavigateTo($"/configuration/setup/path-setup", true);
    }
    #endregion Custom Functions
}
