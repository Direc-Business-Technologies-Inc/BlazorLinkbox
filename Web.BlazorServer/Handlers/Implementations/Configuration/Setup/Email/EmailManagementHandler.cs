using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;
using Web.BlazorServer.ViewModels.Configuration.Setup.Email;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Email;

public class EmailManagementHandler : IEmailManagementHandler
{
    public Task<bool> CreateEmailAsync(EmailSetupVM Email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEmailAsync(Guid EmailId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<EmailDataGridVM> data, int count)> GetAllEmailsAsync(DataGridIntent intent)
    {
        throw new NotImplementedException();
    }

    public Task<EmailSetupVM?> GetEmailAsync(Guid EmailId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEmailAsync(EmailSetupVM Email)
    {
        throw new NotImplementedException();
    }
}
