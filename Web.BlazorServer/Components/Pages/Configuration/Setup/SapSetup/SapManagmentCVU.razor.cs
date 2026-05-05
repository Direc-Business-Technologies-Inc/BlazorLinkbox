using Mapster;
using Microsoft.AspNetCore.Components;
using Shared.Utilities;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.ViewModels.Configuration.Setup.Sap;
using Web.BlazorServer.ViewModels.Enums;
using KernelEnumHelper = Shared.Kernel.EnumHelper;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.SapSetup;

public partial class SapManagmentCVU
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
    [Inject] ISapManagementHandler SapManagementHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }
    bool DbPasswordVisibility { get; set; } = false;
    bool SapPasswordVisibility { get; set; } = false;

    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Updating => PageAction == PageActionTypeEnum.Update;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionCreateSap) || AppBusyService.IsBusy(ActionGetSap) || AppBusyService.IsBusy(ActionUpdateSap);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetSap);

    readonly string ActionCreateSap = KernelEnumHelper.GetEnumDescription(AppActions.CreateSap);
    readonly string ActionGetSap = KernelEnumHelper.GetEnumDescription(AppActions.ViewSap);
    readonly string ActionUpdateSap = KernelEnumHelper.GetEnumDescription(AppActions.UpdateSap);
    #endregion Primitives

    #region Custom Classes
    SapSetupVM? Sap { get; set; } = null;
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
        AppBusyService.SetBusy(ActionGetSap, true);
        if (!Can.Do("OSPS", KernelEnumHelper.GetEnumDescription(PageAction)))
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
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Sap Update"))
                return;
        NavManager.NavigateTo($"/configuration/setup/sap-setup/view?ref={Ref}", true);
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
        NavManager.NavigateTo($"/configuration/setup/sap-setup/update?ref={Ref}", true);
    }
    #endregion Overrides

    #region Custom Functions
    async Task ToggleVisibility(int field)
    {
        if (field == 0)
            DbPasswordVisibility = !DbPasswordVisibility;
        if (field == 1)
            SapPasswordVisibility = !SapPasswordVisibility;

        await InvokeAsync(StateHasChanged);
    }

    async Task LoadDataAsync()
    {
        if(Viewing || Updating)
        {
            var action = await AppActionFactory.RunAsync(async () =>
            {
                AppBusyService.SetBusy(ActionGetSap, IsBusy);

                var path = await SapManagementHandler.GetSapAsync(Ref);
                path.Adapt(FormData);

                AppBusyService.SetBusy(ActionGetSap, false);

            }, AppActionOptionPresets.Loading(ActionGetSap));
        }
        else
        {
            AppBusyService.SetBusy(ActionGetSap, false);
        }

        await InvokeAsync(StateHasChanged);
    }

    async Task Create()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionCreateSap, true);

            var result = await SapManagementHandler.CreateSapAsync(FormData);
            
            AppBusyService.SetBusy(ActionCreateSap, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionCreateSap));

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
            AppBusyService.SetBusy(ActionUpdateSap, true);

            var result = await SapManagementHandler.UpdateSapAsync(FormData);

            AppBusyService.SetBusy(ActionUpdateSap, false);
            return result;

        }, AppActionOptionPresets.Confirmed(ActionUpdateSap));

        action.OnSuccess(async (args) =>
        {
            UnsavedChangesService.MarkClean();
            await Return();
        });
    }

    async Task Return()
    {
        if (UnsavedChangesService.HasChanges)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Sap Setup"))
                return;

        NavManager.NavigateTo($"/configuration/setup/sap-setup", true);
    }
    #endregion Custom Functions
}
