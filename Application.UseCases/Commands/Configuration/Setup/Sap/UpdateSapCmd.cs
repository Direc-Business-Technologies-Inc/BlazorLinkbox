using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Configuration.Setup.Sap;
using MediatR;

namespace Application.UseCases.Commands.Configuration.Setup.Sap;

public record UpdateSapCmd(SapSetupDTO Sap) : ITransactionalRequest<bool>;

public class UpdateSapCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<UpdateSapCmd, bool>
{
    public async Task<bool> Handle(UpdateSapCmd request, CancellationToken cancellationToken)
    {
        // Get existing SAP configuration
        var existingSap = await appReadRepo.FirstOrDefaultAsync<SapDEM>(x => x.Id == request.Sap.Id)
            ?? throw new Exception("SAP configuration not found.");

        // Check if another SAP uses this code
        var duplicateCode = await appReadRepo.ExistsAsync<SapDEM>(x => 
            x.Id != request.Sap.Id && 
            x.SapCode.ToLower() == request.Sap.SapCode.ToLower());
        
        if (duplicateCode)
            throw new Exception("SAP code already in use by another SAP configuration.");

        // Update domain entity
        existingSap.Update(
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

        // Persist changes
        appCommandRepo.Update(existingSap);

        return true;
    }
}
