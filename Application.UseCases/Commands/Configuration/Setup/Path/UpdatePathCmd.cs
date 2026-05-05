using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Path;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Path;

public record UpdatePathCmd(PathSetupDTO Path) : ITransactionalRequest<bool>;

public class UpdatePathCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<UpdatePathCmd, bool>
{
    public async Task<bool> Handle(UpdatePathCmd request, CancellationToken cancellationToken)
    {
        // Get existing path
        var existingPath = await appReadRepo.FirstOrDefaultAsync<PathDEM>(x => x.Id == request.Path.Id)
            ?? throw new Exception("Path not found.");

        // Check if another path uses this code
        var duplicateCode = await appReadRepo.ExistsAsync<PathDEM>(x => 
            x.Id != request.Path.Id && 
            x.PathCode.ToLower() == request.Path.PathCode.ToLower());
        
        if (duplicateCode)
            throw new Exception("Path code already in use by another path.");

        // Update domain entity
        existingPath.Update(
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

        // Persist changes
        appCommandRepo.Update(existingPath);

        return true;
    }
}
