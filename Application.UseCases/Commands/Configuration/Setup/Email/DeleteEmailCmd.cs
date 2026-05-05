using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Email;
using Domain.Entities.Entities.Configuration.Setup.Path;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Email;

public record DeleteEmailCmd(Guid EmailId) : ITransactionalRequest<bool>;
public class DeleteEmailCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<DeleteEmailCmd, bool>
{
    public async Task<bool> Handle(DeleteEmailCmd request, CancellationToken cancellationToken)
    {
        // Get email to delete
        var email = await appReadRepo.FirstOrDefaultAsync<EmailDEM>(x => x.Id == request.EmailId)
            ?? throw new Exception("Email not found.");

        // Delete (soft delete if implemented, hard delete otherwise)
        await appCommandRepo.DeleteManyAsync<EmailDEM>(x => x.Id == request.EmailId);

        return true;
    }
}
