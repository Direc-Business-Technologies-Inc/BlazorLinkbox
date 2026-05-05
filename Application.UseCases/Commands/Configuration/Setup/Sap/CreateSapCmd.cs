using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Sap;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Sap;

public record CreateSapCmd(SapSetupDTO Sap) : ITransactionalRequest<bool>;

public class CreateSapCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<CreateSapCmd, bool>
{
    public async Task<bool> Handle(CreateSapCmd request, CancellationToken cancellationToken)
    {
        if (await appReadRepo.ExistsAsync<SapDEM>(x => x.SapCode.ToLower() == request.Sap.SapCode.ToLower()))
            throw new Exception("SAP code already in use");

        SapDEM dem = SapDEM.Create( 
            request.Sap.SapCode,
            request.Sap.DbVersion,
            request.Sap.DbPort,
            request.Sap.SldServer,
            request.Sap.ServerName,
            request.Sap.LicensePort,
            request.Sap.IpAddress,
            request.Sap.DbName,
            request.Sap.DbUser,
            request.Sap.DbPassword,
            request.Sap.SapUser,
            request.Sap.SapPassword,
            request.Sap.Version,
            request.Sap.Active);

        await appCommandRepo.AddAsync(dem);

        return true;
    }
}
