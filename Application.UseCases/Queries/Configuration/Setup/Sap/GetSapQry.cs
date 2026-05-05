using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Sap;
using MediatR;

namespace Application.UseCases.Queries.Configuration.Setup.Sap;

public record GetSapQry(Guid SapId) : IRequest<SapSetupDTO?>;

public class GetSapQryHandler(
    ISapReadRepo sapReadRepo)
    : IRequestHandler<GetSapQry, SapSetupDTO?>
{
    public async Task<SapSetupDTO?> Handle(GetSapQry request, CancellationToken cancellationToken)
    {
        var sapDTO = await sapReadRepo.GetSapDetails(request.SapId);

        return sapDTO;
    }
}

