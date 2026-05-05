using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Commands.Configuration.Setup.Email;
using Application.UseCases.Queries.Configuration.Setup.Email;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;
using Web.BlazorServer.ViewModels.Configuration.Setup.Email;

namespace Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Email;

public class EmailManagementHandler(
    ISender Sender)
    : IEmailManagementHandler
{
    public async Task<bool> CreateEmailAsync(EmailSetupVM Email)
    {
        var dto = Email.Adapt<EmailSetupDTO>();

        CreateEmailCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<(IEnumerable<EmailDataGridVM> data, int count)> GetAllEmailsAsync(DataGridIntent intent)
    {
        GetAllEmailsQry qry = new(intent);
        var dto = await Sender.Send(qry);

        return (dto.Data.Adapt<IEnumerable<EmailDataGridVM>>(), dto.Count);
    }

    public async Task<EmailSetupVM?> GetEmailAsync(Guid EmailId)
    {
        GetEmailQry qry = new(EmailId);
        var response = await Sender.Send(qry);

        return response.Adapt<EmailSetupVM>();
    }

    public async Task<bool> UpdateEmailAsync(EmailSetupVM Email)
    {
        var dto = Email.Adapt<EmailSetupDTO>();

        UpdateEmailCmd cmd = new(dto);
        await Sender.Send(cmd);

        return true;
    }

    public async Task<bool> DeleteEmailAsync(Guid EmailId)
    {
        DeleteEmailCmd cmd = new(EmailId);
        await Sender.Send(cmd);

        return true;
    }
}
