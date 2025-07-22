using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.WorkInjury;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class WorkInjuryRepository : BaseRepository, IWorkInjuryRepository
    {
        public WorkInjuryRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<WorkInjury>> GetAllAsync()
        {
            const string sql = "SELECT * FROM WorkInjuries";
            return await QueryAsync<WorkInjury>(sql);
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
