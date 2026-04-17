using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Commands.Configuration.Setup.Path;
using Application.UseCases.Queries.Configuration.Setup.Path;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup;
using Web.BlazorServer.ViewModels.Configuration.Setup.Path;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup;

public class PathManagementHandler(
    ISender Sender)
    : IPathManagementHandler
{
    public async Task<bool> CreatePathAsync(PathSetupVM path)
    {
        var dto = path.Adapt<PathSetupDTO>();

        CreatePathCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<(IEnumerable<PathDataGridVM> data, int count)> GetAllPathsAsync(DataGridIntent intent)
    {
        GetAllPathsQry qry = new(intent);
        var dto = await Sender.Send(qry);

        return (dto.Data.Adapt<IEnumerable<PathDataGridVM>>(), dto.Count);
    }

    public async Task<PathSetupVM?> GetPathAsync(Guid pathId)
    {
        GetPathQry qry = new(pathId);
        var response = await Sender.Send(qry);

        return response.Adapt<PathSetupVM>();
    }

    public async Task<bool> UpdatePathAsync(PathSetupVM path)
    {
        var dto = path.Adapt<PathSetupDTO>();

        UpdatePathCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<bool> DeletePathAsync(Guid pathId)
    {
        DeletePathCmd cmd = new(pathId);
        await Sender.Send(cmd);

        return true;
    }
}

