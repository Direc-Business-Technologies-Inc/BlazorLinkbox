using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Api;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Api;

public record DeleteApiCmd(Guid ApiId) : ITransactionalRequest<bool>;

public class DeleteApiCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<DeleteApiCmd, bool>
{
    public async Task<bool> Handle(DeleteApiCmd request, CancellationToken cancellationToken)
    {
        // Get API to delete
        var api = await appReadRepo.FirstOrDefaultAsync<ApiDEM>(x => x.Id == request.ApiId)
            ?? throw new Exception("API not found.");

        // Delete (soft delete if implemented, hard delete otherwise)
        await appCommandRepo.DeleteManyAsync<ApiDEM>(x => x.Id == request.ApiId);

        return true;
    }
}