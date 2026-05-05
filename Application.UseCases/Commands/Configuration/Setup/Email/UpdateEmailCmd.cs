using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Email;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Email;

public record UpdateEmailCmd(EmailSetupDTO Email) : ITransactionalRequest<bool>;

public class UpdateEmailCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<UpdateEmailCmd, bool>
{
    public async Task<bool> Handle(UpdateEmailCmd request, CancellationToken cancellationToken)
    {
        // Get existing email configuration
        var existingEmail = await appReadRepo.FirstOrDefaultAsync<EmailDEM>(x => x.Id == request.Email.Id)
            ?? throw new Exception("Email configuration not found.");

        // Check if another email uses this code
        var duplicateCode = await appReadRepo.ExistsAsync<EmailDEM>(x => 
            x.Id != request.Email.Id && 
            x.EmailCode.ToLower() == request.Email.EmailCode.ToLower());
        
        if (duplicateCode)
            throw new Exception("Email code already in use by another email configuration.");

        // Update domain entity
        existingEmail.Update(
            request.Email.EmailCode,
            request.Email.Description,
            request.Email.EmailAddress,
            request.Email.DisplayName,
            request.Email.EmailPassword,
            request.Email.SMTPClient,
            request.Email.Port,
            request.Email.Active);

        // Persist changes
        appCommandRepo.Update(existingEmail);

        return true;
    }
}
