using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Api;
using MediatR;

namespace Application.UseCases.Queries.Configuration.Setup.Api;

public record GetApiQry(Guid ApiId) : IRequest<ApiSetupDTO?>;

public class GetApiQryHandler(
    IApiReadRepo apiReadRepo)
    : IRequestHandler<GetApiQry, ApiSetupDTO?>
{
    public async Task<ApiSetupDTO?> Handle(GetApiQry request, CancellationToken cancellationToken)
    {
        var apiDTO = await apiReadRepo.GetApiDetails(request.ApiId);

        return apiDTO;
    }
}
