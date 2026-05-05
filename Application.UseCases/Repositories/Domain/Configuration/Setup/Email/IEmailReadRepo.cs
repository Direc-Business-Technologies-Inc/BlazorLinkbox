using Application.DataTransferObjects.Configuration.Setup.Email;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.Configuration.Setup.Email;

public interface IEmailReadRepo
{
    Task<(IEnumerable<EmailDataGridDTO> data, int count)> GetEmailTableDetails(DataGridIntent intent);
    Task<EmailSetupDTO?> GetEmailDetails(Guid emailId);
}
