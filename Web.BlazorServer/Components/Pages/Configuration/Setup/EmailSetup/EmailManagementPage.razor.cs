using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Entities;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Configuration.Setup.Email;

namespace Web.BlazorServer.Components.Pages.Configuration.Setup.EmailSetup;

public partial class EmailManagementPage
{
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Inject] IEmailManagementHandler EmailManagementHandler { get; set; } = default!;

    AppDataGrid<EmailDataGridVM> EmailsDataGrid { get; set; }
    DataGridSettings EmailsDataGridSettings { get; set; }

    string ActionGetEmails { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllEmails);

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
        await GridSettingsService.SetGridSettings(EmailsDataGrid.DataGrid, settings => EmailsDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await EmailsDataGrid.DataGrid.ReloadSettings();
        await EmailsDataGrid.DataGrid.Reload();
    }

    async Task<DataGridResultVM<EmailDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetEmails, true);

            var response = await EmailManagementHandler.GetAllEmailsAsync(intent);

            return response;

        }, AppActionOptionPresets.Loading(ActionGetEmails));

        AppBusyService.SetBusy(ActionGetEmails, false);
        return DataGridResultVM<EmailDataGridVM>.New(action.Result.data ?? [], action.Result.count);
    }

    void CreateEmail() => NavManager.NavigateTo("/configuration/setup/email-setup/create", true);
    void ViewEmail(EmailDataGridVM email) => NavManager.NavigateTo($"/configuration/setup/email-setup/view?ref={email.Id}", true);
}
