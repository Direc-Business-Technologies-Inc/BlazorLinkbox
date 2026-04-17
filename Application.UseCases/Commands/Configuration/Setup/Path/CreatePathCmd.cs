using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Configuration.Setup;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Path;

public record CreatePathCmd(PathSetupDTO Path) : ITransactionalRequest<bool>;

public class CreatePathCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<CreatePathCmd, bool>
{
    public async Task<bool> Handle(CreatePathCmd request, CancellationToken cancellationToken)
    {
        if (await appReadRepo.ExistsAsync<PathDEM>(x => x.PathCode.ToLower() == request.Path.PathCode.ToLower()))
            throw new Exception("Path code already in use");

        PathDEM dem = PathDEM.Create(
            request.Path.PathCode,
            request.Path.LocalPath,
            request.Path.FileSearchOption,
            request.Path.BackupPath,
            request.Path.ErrorPath,
            request.Path.RemotePath,
            request.Path.RemoteServer,
            request.Path.RemoteIpAddress,
            request.Path.RemotePort,
            request.Path.RemoteUserId,
            request.Path.RemotePassword,
            request.Path.Active);

        await appCommandRepo.AddAsync(dem);

        return true;
    }
}
