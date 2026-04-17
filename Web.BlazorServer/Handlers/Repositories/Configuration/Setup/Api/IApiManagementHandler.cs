using Shared.Entities;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;

namespace Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;

public interface IApiManagementHandler
{
    Task<ApiSetupVM?> GetApiAsync(Guid apiId);
    Task<(IEnumerable<ApiDataGridVM> data, int count)> GetAllApisAsync(DataGridIntent intent);
    Task<bool> CreateApiAsync(ApiSetupVM api);
    Task<bool> UpdateApiAsync(ApiSetupVM api);
    Task<bool> DeleteApiAsync(Guid apiId);
}
