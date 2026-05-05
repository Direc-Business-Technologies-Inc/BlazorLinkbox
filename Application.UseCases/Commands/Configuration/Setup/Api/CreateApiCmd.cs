using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Api;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Api;

public record CreateApiCmd(ApiSetupDTO Api) : ITransactionalRequest<bool>;

public class CreateApiCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<CreateApiCmd, bool>
{
    public async Task<bool> Handle(CreateApiCmd request, CancellationToken cancellationToken)
    {
        if (await appReadRepo.ExistsAsync<ApiDEM>(x => x.ApiCode.ToLower() == request.Api.ApiCode.ToLower()))
            throw new Exception("API code already in use");

        ApiDEM dem = ApiDEM.Create(
            request.Api.ApiCode,
            request.Api.Method,
            request.Api.Module,
            request.Api.Url,
            request.Api.ApiKey,
            request.Api.ApiSecretKey,
            request.Api.ApiToken,
            request.Api.ApiLoginUrl,
            request.Api.ApiLoginBody,
            request.Api.Active);

        await appCommandRepo.AddAsync(dem);

        return true;
    }
}
