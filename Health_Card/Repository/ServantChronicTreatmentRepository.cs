using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.ServantChronicTreatment;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class ServantChronicTreatmentRepository : BaseRepository, IServantChronicTreatmentRepository
    {
        public ServantChronicTreatmentRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<ServantChronicTreatment>> GetAllAsync()
        {
            const string sql = "SELECT * FROM ServantChronicTreatments";
            return await QueryAsync<ServantChronicTreatment>(sql);
        }

        public async Task<ServantChronicTreatment> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<ServantChronicTreatment>(
                "spGetServantChronicTreatmentById", 
                new { TreatmentID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(ServantChronicTreatment entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateServantChronicTreatment",
                new
                {
                    entity.ServantID,
                    entity.TreatmentName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);

            entity.TreatmentID = id;
        }

        public async Task UpdateAsync(ServantChronicTreatment entity)
        {
            await ExecuteAsync(
                "spUpdateServantChronicTreatment",
                new
                {
                    entity.TreatmentID,
                    entity.ServantID,
                    entity.TreatmentName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteServantChronicTreatment",
                new { TreatmentID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
