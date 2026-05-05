using Application.DataTransferObjects.Configuration.Setup.Email;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Email;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Entities.Configuration.Setup.Email;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Database.MsSql.Implementation.Reads;

public class EmailReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<EmailReadRepo>, IEmailReadRepo
{
    public Task<(IEnumerable<EmailDataGridDTO> data, int count)> GetEmailTableDetails(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<EmailDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from e in ctx.Set<EmailDEM>().AsNoTracking()
                        select new EmailDataGridDTO
                        {
                            Id = e.Id,
                            EmailCode = e.EmailCode,
                            Description = e.Description,
                            Active = e.Active
                        };

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<EmailDataGridDTO>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
            {
                query = query.OrderByProperty(sort.Property, sort.Direction);
            }

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<EmailDataGridDTO> data = await query.Distinct().ToListAsync();

            return (data, count);
        });
    }

    public Task<EmailSetupDTO?> GetEmailDetails(Guid emailId)
    {
        return ExecuteAppDbWork<EmailSetupDTO?>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from e in ctx.Set<EmailDEM>().AsNoTracking()
                        where e.Id == emailId
                        select new EmailSetupDTO
                        {
                            Id = e.Id,
                            EmailCode = e.EmailCode,
                            Description = e.Description,
                            EmailAddress = e.EmailAddress,
                            DisplayName = e.DisplayName,
                            EmailPassword = e.EmailPassword,
                            SMTPClient = e.SMTPClient,
                            Port = e.Port,
                            Active = e.Active,
                            CreatedBy = e.CreatedBy,
                            CreatedDate = e.CreatedDate,
                            UpdatedBy = e.UpdatedBy,
                            UpdatedDate = e.UpdatedDate
                        };

            EmailSetupDTO? data = await query.FirstOrDefaultAsync();

            return data;
        });
    }
}
