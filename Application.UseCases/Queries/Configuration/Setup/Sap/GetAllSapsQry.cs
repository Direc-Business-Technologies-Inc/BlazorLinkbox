using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Queries.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Sap;
using MediatR;
using Shared.Entities;

namespace Application.UseCases.Queries.Configuration.Setup.Sap;

public record GetAllSapsQry(DataGridIntent Intent) : IRequest<(IEnumerable<SapDataGridDTO> Data, int Count)>;

public class GetAllSapsQryHandler(
    ISapReadRepo sapReadRepo)
    : IRequestHandler<GetAllSapsQry, (IEnumerable<SapDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<SapDataGridDTO> Data, int Count)> Handle(GetAllSapsQry request, CancellationToken cancellationToken)
    {
        var dto = await sapReadRepo.GetSapTableDetails(request.Intent);

        return dto;
    }
}