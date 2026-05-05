using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Queries.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Email;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Path;
using MediatR;
using Shared.Entities;

namespace Application.UseCases.Queries.Configuration.Setup.Email;

public record GetAllEmailsQry(DataGridIntent Intent) : IRequest<(IEnumerable<EmailDataGridDTO> Data, int Count)>;

public class GetAllEmailsQryHandler(
    IEmailReadRepo emailReadRepo)
    : IRequestHandler<GetAllEmailsQry, (IEnumerable<EmailDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<EmailDataGridDTO> Data, int Count)> Handle(GetAllEmailsQry request, CancellationToken cancellationToken)
    {
        var dto = await emailReadRepo.GetEmailTableDetails(request.Intent);

        return dto;
    }
}
