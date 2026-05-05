using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Path;
using MediatR;
using Shared.Entities;

namespace Application.UseCases.Queries.Configuration.Setup.Path;

public record GetAllPathsQry(DataGridIntent Intent) : IRequest<(IEnumerable<PathDataGridDTO> Data, int Count)>;

public class GetAllPathsQryHandler(
    IPathReadRepo pathReadRepo)
    : IRequestHandler<GetAllPathsQry, (IEnumerable<PathDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<PathDataGridDTO> Data, int Count)> Handle(GetAllPathsQry request, CancellationToken cancellationToken)
    {
        var dto = await pathReadRepo.GetPathTableDetails(request.Intent);

        return dto;
    }
}


