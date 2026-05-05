using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Email;
using MediatR;

namespace Application.UseCases.Queries.Configuration.Setup.Email;

public record GetEmailQry(Guid EmailId) : IRequest<EmailSetupDTO?>;

public class GetEmailQryHandler(
    IEmailReadRepo emailReadRepo)
    : IRequestHandler<GetEmailQry, EmailSetupDTO?>
{
    public async Task<EmailSetupDTO?> Handle(GetEmailQry request, CancellationToken cancellationToken)
    {
        var emailDTO = await emailReadRepo.GetEmailDetails(request.EmailId);

        return emailDTO;
    }
}
