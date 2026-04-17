using Application.UseCases.Repositories.Bases;
using Domain.Entities.Configuration.Setup;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Path;

public record DeletePathCmd(Guid PathId) : ITransactionalRequest<bool>;

public class DeletePathCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<DeletePathCmd, bool>
{
    public async Task<bool> Handle(DeletePathCmd request, CancellationToken cancellationToken)
    {
        // Get path to delete
        var path = await appReadRepo.FirstOrDefaultAsync<PathDEM>(x => x.Id == request.PathId)
            ?? throw new Exception("Path not found.");

        // Delete (soft delete if implemented, hard delete otherwise)
        await appCommandRepo.DeleteManyAsync<PathDEM>(x => x.Id == request.PathId);

        return true;
    }
}
