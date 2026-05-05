using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Path;
using MediatR;

namespace Application.UseCases.Queries.Configuration.Setup.Path;

public record GetPathQry(Guid PathId) : IRequest<PathSetupDTO?>;

public class GetPathQryHandler(
    IPathReadRepo pathReadRepo)
    : IRequestHandler<GetPathQry, PathSetupDTO?>
{
    public async Task<PathSetupDTO?> Handle(GetPathQry request, CancellationToken cancellationToken)
    {
        var pathDTO = await pathReadRepo.GetPathDetails(request.PathId);

        return pathDTO;
    }
}

