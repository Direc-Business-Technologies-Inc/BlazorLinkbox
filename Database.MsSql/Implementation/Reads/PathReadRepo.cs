using Application.DataTransferObjects.Configuration.Setup.Path;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Path;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Entities.Configuration.Setup.Path;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Database.MsSql.Implementation.Reads;

public class PathReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<PathReadRepo>, IPathReadRepo
{
    public Task<(IEnumerable<PathDataGridDTO> data, int count)> GetPathTableDetails(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<PathDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from p in ctx.Set<PathDEM>().AsNoTracking()
                        select new PathDataGridDTO
                        {
                            Id = p.Id,
                            PathCode = p.PathCode,
                            LocalPath = p.LocalPath,
                            BackupPath = p.BackupPath,
                            ErrorPath = p.ErrorPath,
                            Active = p.Active
                        };

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<PathDataGridDTO>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
            {
                query = query.OrderByProperty(sort.Property, sort.Direction);
            }

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<PathDataGridDTO> data = await query.Distinct().ToListAsync();

            return (data, count);
        });
    }

    public Task<PathSetupDTO?> GetPathDetails(Guid pathId)
    {
        return ExecuteAppDbWork<PathSetupDTO?>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from p in ctx.Set<PathDEM>().AsNoTracking()
                        where p.Id == pathId
                        select new PathSetupDTO
                        {
                            Id = p.Id,
                            PathCode = p.PathCode,
                            LocalPath = p.LocalPath,
                            FileSearchOption = p.FileSearchOption,
                            BackupPath = p.BackupPath,
                            ErrorPath = p.ErrorPath,
                            RemotePath = p.RemotePath,
                            RemoteServer = p.RemoteServer,
                            RemoteIpAddress = p.RemoteIpAddress,
                            RemotePort = p.RemotePort,
                            RemoteUserId = p.RemoteUserId,
                            RemotePassword = p.RemotePassword,
                            Active = p.Active,
                            CreatedBy = p.CreatedBy,
                            CreatedDate = p.CreatedDate,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedDate = p.UpdatedDate
                        };

            PathSetupDTO? data = await query.FirstOrDefaultAsync();

            return data;
        });
    }
}
