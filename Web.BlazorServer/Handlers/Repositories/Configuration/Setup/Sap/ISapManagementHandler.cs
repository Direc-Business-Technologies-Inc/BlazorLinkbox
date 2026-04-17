using Shared.Entities;
using Web.BlazorServer.ViewModels.Configuration.Setup.Path;
using Web.BlazorServer.ViewModels.Configuration.Setup.Sap;

namespace Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;

public interface ISapManagementHandler
{
    Task<SapSetupVM?> GetSapAsync(Guid sapId);
    Task<(IEnumerable<SapDataGridVM> data, int count)> GetAllSapsAsync(DataGridIntent intent);
    Task<bool> CreateSapAsync(SapSetupVM sap);
    Task<bool> UpdateSapAsync(SapSetupVM sap);
    Task<bool> DeleteSapAsync(Guid sapId);
}
