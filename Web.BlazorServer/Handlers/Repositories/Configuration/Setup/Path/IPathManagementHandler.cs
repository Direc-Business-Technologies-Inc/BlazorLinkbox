using Shared.Entities;
using Web.BlazorServer.ViewModels.Configuration.Setup.Path;

namespace Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Path;

public interface IPathManagementHandler
{
    Task<PathSetupVM?> GetPathAsync(Guid pathId);
    Task<(IEnumerable<PathDataGridVM> data, int count)> GetAllPathsAsync(DataGridIntent intent);
    Task<bool> CreatePathAsync(PathSetupVM path);
    Task<bool> UpdatePathAsync(PathSetupVM path);
    Task<bool> DeletePathAsync(Guid pathId);
}

