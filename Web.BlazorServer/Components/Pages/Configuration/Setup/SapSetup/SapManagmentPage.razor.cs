using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Entities;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Configuration.Setup.Sap;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.SapSetup;

public partial class SapManagmentPage
{
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Inject] ISapManagementHandler SapManagementHandler { get; set; } = default!;

    AppDataGrid<SapDataGridVM> SapsDataGrid { get; set; }
    DataGridSettings SapsDataGridSettings { get; set; }

    string ActionGetSaps { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllSaps);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await LoadGridSettings();
            await InvokeAsync(StateHasChanged);
        }
    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(SapsDataGrid.DataGrid, settings => SapsDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await SapsDataGrid.DataGrid.ReloadSettings();
        await SapsDataGrid.DataGrid.Reload();
    }

    async Task<DataGridResultVM<SapDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetSaps, true);

            var response = await SapManagementHandler.GetAllSapsAsync(intent);

            return response;

        }, AppActionOptionPresets.Loading(ActionGetSaps));

        AppBusyService.SetBusy(ActionGetSaps, false);
        return DataGridResultVM<SapDataGridVM>.New(action.Result.data ?? [], action.Result.count);
    }

    void CreateSap() => NavManager.NavigateTo("/configuration/setup/sap-setup/create", true);
    void ViewSap(SapDataGridVM sap) => NavManager.NavigateTo($"/configuration/setup/sap-setup/view?ref{sap.Id}", true);
}
