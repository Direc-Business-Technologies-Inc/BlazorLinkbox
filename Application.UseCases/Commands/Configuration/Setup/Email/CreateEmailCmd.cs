using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Email;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Email;

public record CreateEmailCmd(EmailSetupDTO Email) : ITransactionalRequest<bool>;

public class CreateEmailCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<CreateEmailCmd, bool>
{
    public async Task<bool> Handle(CreateEmailCmd request, CancellationToken cancellationToken)
    {
        if (await appReadRepo.ExistsAsync<EmailDEM>(x => x.EmailCode.ToLower() == request.Email.EmailCode.ToLower()))
            throw new Exception("Email code already in use");

        EmailDEM dem = EmailDEM.Create(
            request.Email.EmailCode,
            request.Email.Description,
            request.Email.EmailAddress,
            request.Email.DisplayName,
            request.Email.EmailPassword,
            request.Email.SMTPClient,
            request.Email.Port,
            request.Email.Active);

        await appCommandRepo.AddAsync(dem);

        return true;
    }
}
