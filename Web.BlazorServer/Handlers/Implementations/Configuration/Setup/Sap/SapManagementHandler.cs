using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Commands.Configuration.Setup.Sap;
using Application.UseCases.Queries.Configuration.Setup.Sap;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;
using Web.BlazorServer.ViewModels.Configuration.Setup.Sap;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Sap;

public class SapManagementHandler(
    ISender Sender)
    : ISapManagementHandler
{
    public async Task<bool> CreateSapAsync(SapSetupVM sap)
    {
        var dto = sap.Adapt<SapSetupDTO>();

        CreateSapCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<(IEnumerable<SapDataGridVM> data, int count)> GetAllSapsAsync(DataGridIntent intent)
    {
        GetAllSapsQry qry = new(intent);
        var dto = await Sender.Send(qry);

        return (dto.Data.Adapt<IEnumerable<SapDataGridVM>>(), dto.Count);
    }

    public async Task<SapSetupVM?> GetSapAsync(Guid sapId)
    {
        GetSapQry qry = new(sapId);
        var response = await Sender.Send(qry);

        return response.Adapt<SapSetupVM>();
    }

    public async Task<bool> UpdateSapAsync(SapSetupVM sap)
    {
        var dto = sap.Adapt<SapSetupDTO>();

        UpdateSapCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<bool> DeleteSapAsync(Guid sapId)
    {
        DeleteSapCmd cmd = new(sapId);
        await Sender.Send(cmd);

        return true;
    }
}
