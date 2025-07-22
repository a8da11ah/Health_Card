using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.SurgicalOperation;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class SurgicalOperationRepository : BaseRepository, ISurgicalOperationRepository
    {
        public SurgicalOperationRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<SurgicalOperation>> GetAllAsync()
        {
            const string sql = "SELECT * FROM SurgicalOperations";
            return await QueryAsync<SurgicalOperation>(sql);
        }

        public async Task<SurgicalOperation> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<SurgicalOperation>(
                "spGetSurgicalOperationById", 
                new { OperationID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(SurgicalOperation entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateSurgicalOperation",
                new
                {
                    entity.ServantID,
                    entity.OperationDate,
                    entity.OperationType,
                    entity.HospitalName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);

            entity.OperationID = id;
        }

        public async Task UpdateAsync(SurgicalOperation entity)
        {
            await ExecuteAsync(
                "spUpdateSurgicalOperation",
                new
                {
                    entity.OperationID,
                    entity.ServantID,
                    entity.OperationDate,
                    entity.OperationType,
                    entity.HospitalName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteSurgicalOperation",
                new { OperationID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
