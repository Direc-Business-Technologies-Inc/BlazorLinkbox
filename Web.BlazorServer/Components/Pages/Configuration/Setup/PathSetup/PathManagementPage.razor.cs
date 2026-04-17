using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Entities;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Path;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Configuration.Setup.Path;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.PathSetup;

public partial class PathManagementPage
{
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Inject] IPathManagementHandler PathManagementHandler { get; set; } = default!;

    AppDataGrid<PathDataGridVM> PathsDataGrid { get; set; }
    DataGridSettings PathsDataGridSettings { get; set; }

    string ActionGetPaths { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllPaths);

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
        await GridSettingsService.SetGridSettings(PathsDataGrid.DataGrid, settings => PathsDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await PathsDataGrid.DataGrid.ReloadSettings();
        await PathsDataGrid.DataGrid.Reload();
    }

    async Task<DataGridResultVM<PathDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetPaths, true);

            var response = await PathManagementHandler.GetAllPathsAsync(intent);

            return response;

        }, AppActionOptionPresets.Loading(ActionGetPaths));

        AppBusyService.SetBusy(ActionGetPaths, false);
        return DataGridResultVM<PathDataGridVM>.New(action.Result.data ?? [], action.Result.count);
    }

    void CreatePath() => NavManager.NavigateTo("/configuration/setup/path-setup/create", true);
    void ViewPath(PathDataGridVM path) => NavManager.NavigateTo($"/configuration/setup/path-setup/view?ref={path.Id}", true);
}
