using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Api;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Api;

public record UpdateApiCmd(ApiSetupDTO Api) : ITransactionalRequest<bool>;

public class UpdateApiCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<UpdateApiCmd, bool>
{
    public async Task<bool> Handle(UpdateApiCmd request, CancellationToken cancellationToken)
    {
        // Get existing API configuration
        var existingApi = await appReadRepo.FirstOrDefaultAsync<ApiDEM>(x => x.Id == request.Api.Id)
            ?? throw new Exception("API configuration not found.");

        // Check if another API uses this code
        var duplicateCode = await appReadRepo.ExistsAsync<ApiDEM>(x => 
            x.Id != request.Api.Id && 
            x.ApiCode.ToLower() == request.Api.ApiCode.ToLower());
        
        if (duplicateCode)
            throw new Exception("API code already in use by another API configuration.");

        // Update domain entity
        existingApi.Update(
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

        // Persist changes
        appCommandRepo.Update(existingApi);

        return true;
    }
}
