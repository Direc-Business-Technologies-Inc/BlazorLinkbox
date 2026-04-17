using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Entities;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.ApiSetup;

public partial class ApiManagementPage
{
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Inject] IApiManagementHandler ApiManagementHandler { get; set; } = default!;

    AppDataGrid<ApiDataGridVM> ApisDataGrid { get; set; }
    DataGridSettings ApisDataGridSettings { get; set; }

    string ActionGetApis { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllApis);

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
        await GridSettingsService.SetGridSettings(ApisDataGrid.DataGrid, settings => ApisDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await ApisDataGrid.DataGrid.ReloadSettings();
        await ApisDataGrid.DataGrid.Reload();
    }

    async Task<DataGridResultVM<ApiDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetApis, true);

            var response = await ApiManagementHandler.GetAllApisAsync(intent);

            return response;

        }, AppActionOptionPresets.Loading(ActionGetApis));

        AppBusyService.SetBusy(ActionGetApis, false);
        return DataGridResultVM<ApiDataGridVM>.New(action.Result.data ?? [], action.Result.count);
    }

    void CreateApi() => NavManager.NavigateTo("/configuration/setup/api-setup/create", true);
    void ViewApi(ApiDataGridVM api) => NavManager.NavigateTo($"/configuration/setup/api-setup/view?ref={api.Id}", true);
}
