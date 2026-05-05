using Application.DataTransferObjects.Configuration.Setup.Api;
using Application.UseCases.Repositories.Domain.Configuration.Setup.Api;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Entities.Configuration.Setup.Api;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Database.MsSql.Implementation.Reads;

public class ApiReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<ApiReadRepo>, IApiReadRepo
{
    public Task<(IEnumerable<ApiDataGridDTO> data, int count)> GetApiTableDetails(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<ApiDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from a in ctx.Set<ApiDEM>().AsNoTracking()
                        select new ApiDataGridDTO
                        {
                            Id = a.Id,
                            ApiCode = a.ApiCode,
                            Method = a.Method,
                            Url = a.Url,
                            Active = a.Active
                        };

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<ApiDataGridDTO>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
            {
                query = query.OrderByProperty(sort.Property, sort.Direction);
            }

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<ApiDataGridDTO> data = await query.Distinct().ToListAsync();

            return (data, count);
        });
    }

    public Task<ApiSetupDTO?> GetApiDetails(Guid apiId)
    {
        return ExecuteAppDbWork<ApiSetupDTO?>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from a in ctx.Set<ApiDEM>().AsNoTracking()
                        where a.Id == apiId
                        select new ApiSetupDTO
                        {
                            Id = a.Id,
                            ApiCode = a.ApiCode,
                            Method = a.Method,
                            Module = a.Module,
                            Url = a.Url,
                            ApiKey = a.ApiKey,
                            ApiSecretKey = a.ApiSecretKey,
                            ApiToken = a.ApiToken,
                            ApiLoginUrl = a.ApiLoginUrl,
                            ApiLoginBody = a.ApiLoginBody,
                            Active = a.Active,
                            CreatedBy = a.CreatedBy,
                            CreatedDate = a.CreatedDate,
                            UpdatedBy = a.UpdatedBy,
                            UpdatedDate = a.UpdatedDate
                        };

            ApiSetupDTO? data = await query.FirstOrDefaultAsync();

            return data;
        });
    }
}
