using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Sap;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Sap;

public record DeleteSapCmd(Guid SapId) : ITransactionalRequest<bool>;

public class DeleteSapCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<DeleteSapCmd, bool>
{
    public async Task<bool> Handle(DeleteSapCmd request, CancellationToken cancellationToken)
    {
        // Get SAP to delete
        var sap = await appReadRepo.FirstOrDefaultAsync<SapDEM>(x => x.Id == request.SapId)
            ?? throw new Exception("SAP not found.");

        // Delete (soft delete if implemented, hard delete otherwise)
        await appCommandRepo.DeleteManyAsync<SapDEM>(x => x.Id == request.SapId);

        return true;
    }
}
