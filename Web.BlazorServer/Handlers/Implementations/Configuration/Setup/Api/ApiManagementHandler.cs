using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Api;

public class ApiManagementHandler : IApiManagementHandler
{
    public Task<bool> CreateApiAsync(ApiSetupVM api)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteApiAsync(Guid apiId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<ApiDataGridVM> data, int count)> GetAllApisAsync(DataGridIntent intent)
    {
        throw new NotImplementedException();
    }

    public Task<ApiSetupVM?> GetApiAsync(Guid apiId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateApiAsync(ApiSetupVM api)
    {
        throw new NotImplementedException();
    }
}
