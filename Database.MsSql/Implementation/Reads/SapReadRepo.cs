using Application.DataTransferObjects.Configuration.Setup.Sap;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Sap;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Entities.Configuration.Setup.Sap;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Database.MsSql.Implementation.Reads;

public class SapReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<SapReadRepo>, ISapReadRepo
{
    public Task<(IEnumerable<SapDataGridDTO> data, int count)> GetSapTableDetails(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<SapDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from s in ctx.Set<SapDEM>().AsNoTracking()
                        select new SapDataGridDTO
                        {
                            Id = s.Id,
                            SapCode = s.SapCode,
                            DbVersion = s.DbVersion,
                            LicensePort = int.Parse(s.LicensePort),
                            IpAddress = s.IpAddress,
                            Version = s.Version,
                            DbPort = s.DbPort,
                            DbName = s.DbName,
                            DbUser = s.DbUser,
                            SapUser = s.SapUser,
                            Active = s.Active
                        };

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<SapDataGridDTO>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
            {
                query = query.OrderByProperty(sort.Property, sort.Direction);
            }

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<SapDataGridDTO> data = await query.Distinct().ToListAsync();

            return (data, count);
        });
    }

    public Task<SapSetupDTO?> GetSapDetails(Guid sapId)
    {
        return ExecuteAppDbWork<SapSetupDTO?>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from s in ctx.Set<SapDEM>().AsNoTracking()
                        where s.Id == sapId
                        select new SapSetupDTO
                        {
                            Id = s.Id,
                            SapCode = s.SapCode,
                            DbVersion = s.DbVersion,
                            DbPort = s.DbPort,
                            SldServer = s.SldServer,
                            ServerName = s.ServerName,
                            LicensePort = s.LicensePort,
                            IpAddress = s.IpAddress,
                            Version = s.Version,
                            DbName = s.DbName,
                            DbUser = s.DbUser,
                            DbPassword = s.DbPassword,
                            SapUser = s.SapUser,
                            SapPassword = s.SapPassword,
                            Active = s.Active,
                            CreatedBy = s.CreatedBy,
                            CreatedDate = s.CreatedDate,
                            UpdatedBy = s.UpdatedBy,
                            UpdatedDate = s.UpdatedDate
                        };

            SapSetupDTO? data = await query.FirstOrDefaultAsync();

            return data;
        });
    }
}
