using Application.DataTransferObjects.Configuration.Setup.Api;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.Configuration.Setup.Api;

public interface IApiReadRepo
{
    Task<(IEnumerable<ApiDataGridDTO> data, int count)> GetApiTableDetails(DataGridIntent intent);
    Task<ApiSetupDTO?> GetApiDetails(Guid apiId);
}
