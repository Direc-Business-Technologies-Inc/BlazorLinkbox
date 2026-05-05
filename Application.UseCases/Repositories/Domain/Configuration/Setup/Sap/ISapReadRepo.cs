using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.DataTransferObjects.Configuration.Setup.Sap;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.Configuration.Setup.Sap;

public interface ISapReadRepo
{
    Task<(IEnumerable<SapDataGridDTO> data, int count)> GetSapTableDetails(DataGridIntent intent);
    Task<SapSetupDTO?> GetSapDetails(Guid sapId);
}
