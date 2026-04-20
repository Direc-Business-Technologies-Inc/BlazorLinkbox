using Mapster;
using Microsoft.AspNetCore.Components;
using Radzen;
using Sprache;
using Web.BlazorServer.Components.Shared.Others;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.Services.Implementation;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;
using Web.BlazorServer.ViewModels.Enums;
using KernelEnumHelper = Shared.Kernel.EnumHelper;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.ApiSetup;

public partial class ApiManagementCVU
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
    [Inject] IApiManagementHandler ApiManagementHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }
    bool PasswordVisibility { get; set; } = false;
    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Updating => PageAction == PageActionTypeEnum.Update;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionCreateApi) || AppBusyService.IsBusy(ActionGetApi) || AppBusyService.IsBusy(ActionUpdateApi);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetApi);

    readonly string ActionCreateApi = KernelEnumHelper.GetEnumDescription(AppActions.CreateApi);
    readonly string ActionGetApi = KernelEnumHelper.GetEnumDescription(AppActions.ViewApi);
    readonly string ActionUpdateApi = KernelEnumHelper.GetEnumDescription(AppActions.UpdateApi);
    #endregion Primitives

    #region Custom Classes
    ApiSetupVM? Api { get; set; } = null;
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
        AppBusyService.SetBusy(ActionGetApi, true);
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
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Api Update"))
                return;
        NavManager.NavigateTo($"/configuration/setup/Api-setup/view?ref={Ref}", true);
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
        NavManager.NavigateTo($"/configuration/setup/Api-setup/update?ref={Ref}", true);
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
                AppBusyService.SetBusy(ActionGetApi, true);

                var Api = await ApiManagementHandler.GetApiAsync(Ref);
                Api.Adapt(FormData);

                AppBusyService.SetBusy(ActionGetApi, false);

            }, AppActionOptionPresets.Loading(ActionGetApi));
        }
        else
        {
            AppBusyService.SetBusy(ActionGetApi, false);
        }

        await InvokeAsync(StateHasChanged);
    }

    async Task Create()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionCreateApi, true);

            var result = await ApiManagementHandler.CreateApiAsync(FormData);

            AppBusyService.SetBusy(ActionCreateApi, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionCreateApi));

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
            AppBusyService.SetBusy(ActionUpdateApi, true);

            var result = await ApiManagementHandler.UpdateApiAsync(FormData);

            AppBusyService.SetBusy(ActionUpdateApi, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionUpdateApi));

        action.OnSuccess(async (args) =>
        {
            UnsavedChangesService.MarkClean();
            await Return();
        });
    }

    async Task Return()
    {
        if (UnsavedChangesService.HasChanges)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Api Setup"))
                return;

        NavManager.NavigateTo($"/configuration/setup/Api-setup", true);
    }

    async Task HandleSidebar()
    {
        await DialogService.OpenSideAsync<HiddenOptions>(
            "Connection Settings",
            new Dictionary<string, object> 
            { 
                { nameof(HiddenOptions.FormData), FormData },
                { nameof(HiddenOptions.Viewing), Viewing },
                { nameof(HiddenOptions.IsBusy), IsBusy }
            },
            options: new SideDialogOptions { 
                CloseDialogOnOverlayClick = true, 
                Resizable = true, 
                Position = DialogPosition.Right, 
                ShowMask = true, 
                MinHeight = 250.0, 
                MinWidth = 350.0 }
            );
    }
    #endregion Custom Functions
}
