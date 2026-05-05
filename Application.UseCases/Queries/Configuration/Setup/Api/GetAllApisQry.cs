using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Api;
using MediatR;
using Shared.Entities;

namespace Application.UseCases.Queries.Configuration.Setup.Api;

public record GetAllApisQry(DataGridIntent Intent) : IRequest<(IEnumerable<ApiDataGridDTO> Data, int Count)>;

public class GetAllApisQryHandler(
    IApiReadRepo apiReadRepo)
    : IRequestHandler<GetAllApisQry, (IEnumerable<ApiDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<ApiDataGridDTO> Data, int Count)> Handle(GetAllApisQry request, CancellationToken cancellationToken)
    {
        var dto = await apiReadRepo.GetApiTableDetails(request.Intent);

        return dto;
    }
}
