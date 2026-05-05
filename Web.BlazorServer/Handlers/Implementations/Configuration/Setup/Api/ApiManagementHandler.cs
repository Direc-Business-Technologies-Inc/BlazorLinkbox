using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Commands.Configuration.Setup.Api;
using Application.UseCases.Queries.Configuration.Setup.Api;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Api;

public class ApiManagementHandler(
    ISender Sender)
    : IApiManagementHandler
{
    public async Task<bool> CreateApiAsync(ApiSetupVM api)
    {
        var dto = api.Adapt<ApiSetupDTO>();

        CreateApiCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<(IEnumerable<ApiDataGridVM> data, int count)> GetAllApisAsync(DataGridIntent intent)
    {
        GetAllApisQry qry = new(intent);
        var dto = await Sender.Send(qry);

        return (dto.Data.Adapt<IEnumerable<ApiDataGridVM>>(), dto.Count);
    }

    public async Task<ApiSetupVM?> GetApiAsync(Guid apiId)
    {
        GetApiQry qry = new(apiId);
        var response = await Sender.Send(qry);

        return response.Adapt<ApiSetupVM>();
    }

    public async Task<bool> UpdateApiAsync(ApiSetupVM api)
    {
        var dto = api.Adapt<ApiSetupDTO>();

        UpdateApiCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<bool> DeleteApiAsync(Guid apiId)
    {
        DeleteApiCmd cmd = new(apiId);
        await Sender.Send(cmd);

        return true;
    }
}
