using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;
using Web.BlazorServer.ViewModels.Configuration.Setup.Sap;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Sap;

public class SapManagementHandler : ISapManagementHandler
{
    public Task<bool> CreateSapAsync(SapSetupVM sap)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSapAsync(Guid sapId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<SapDataGridVM> data, int count)> GetAllSapsAsync(DataGridIntent intent)
    {
        throw new NotImplementedException();
    }

    public Task<SapSetupVM?> GetSapAsync(Guid sapId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateSapAsync(SapSetupVM sap)
    {
        throw new NotImplementedException();
    }
}
