using Shared.Entities;
using Web.BlazorServer.ViewModels.Configuration.Setup.Email;

namespace Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;

public interface IEmailManagementHandler
{
    Task<EmailSetupVM?> GetEmailAsync(Guid EmailId);
    Task<(IEnumerable<EmailDataGridVM> data, int count)> GetAllEmailsAsync(DataGridIntent intent);
    Task<bool> CreateEmailAsync(EmailSetupVM Email);
    Task<bool> UpdateEmailAsync(EmailSetupVM Email);
    Task<bool> DeleteEmailAsync(Guid EmailId);
}
