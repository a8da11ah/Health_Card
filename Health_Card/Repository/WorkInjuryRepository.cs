using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class WorkInjuryRepository : BaseRepository, IRepositoryBase<WorkInjury,WorkInjuryFilter>
    {
        public WorkInjuryRepository(IDbConnection connection) : base(connection)
        {
        }


        public async Task<IEnumerable<WorkInjury>> GetAllAsync(WorkInjuryFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM WorkInjuries WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.InjuryType))
            {
                sql.Append(" AND InjuryType LIKE @InjuryType");
                filter.InjuryType = $"%{filter.InjuryType}%";
            }

            if (!string.IsNullOrEmpty(filter.DoctorName))
            {
                sql.Append(" AND DoctorName LIKE @DoctorName");
                filter.DoctorName = $"%{filter.DoctorName}%";
            }

            sql.Append(" ORDER BY WorkInjuryID"); // Replace with the actual PK or relevant column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.InjuryType,
                filter.DoctorName,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<WorkInjury>(sql.ToString(), parameters);
        }


        public async Task<WorkInjury> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<WorkInjury>(
                "spGetWorkInjuryById", 
                new { InjuryID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(WorkInjury entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateWorkInjury",
                new
                {
                    entity.ServantID,
                    entity.InjuryDate,
                    entity.InjuryType,
                    entity.DepartmentOfInjury,
                    entity.Description
                },
                commandType: CommandType.StoredProcedure);

            entity.InjuryID = id;
        }

        public async Task UpdateAsync(WorkInjury entity)
        {
            await ExecuteAsync(
                "spUpdateWorkInjury",
                new
                {
                    entity.InjuryID,
                    entity.ServantID,
                    entity.InjuryDate,
                    entity.InjuryType,
                    entity.DepartmentOfInjury,
                    entity.Description
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteWorkInjury",
                new { InjuryID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
