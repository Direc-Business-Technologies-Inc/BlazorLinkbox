using Application.DataTransferObjects.Configuration.Setup.Path;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.Configuration.Setup.Path;

public interface IPathReadRepo
{
    Task<(IEnumerable<PathDataGridDTO> data, int count)> GetPathTableDetails(DataGridIntent intent);
    Task<PathSetupDTO?> GetPathDetails(Guid pathId);
}
