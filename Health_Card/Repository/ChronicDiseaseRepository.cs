
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class ChronicDiseaseRepository : BaseRepository, IChronicDiseaseRepository
    {
        public ChronicDiseaseRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<ChronicDisease>> GetAllAsync()
        {
            const string sql = "SELECT * FROM ChronicDiseases";
            return await QueryAsync<ChronicDisease>(sql);
        }

        public async Task<ChronicDisease> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<ChronicDisease>(
                "spGetChronicDiseaseById", 
                new { ChronicDiseaseID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(ChronicDisease entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateChronicDisease",
                new
                {
                    entity.ServantID,
                    entity.DiseaseName,
                    entity.Notes,
                    entity.DiseaseType,
                    entity.FamilyMemberRelation
                },
                commandType: CommandType.StoredProcedure);

            entity.ChronicDiseaseID = id;
        }

        public async Task UpdateAsync(ChronicDisease entity)
        {
            await ExecuteAsync(
                "spUpdateChronicDisease",
                new
                {
                    entity.ChronicDiseaseID,
                    entity.ServantID,
                    entity.DiseaseName,
                    entity.Notes,
                    entity.DiseaseType,
                    entity.FamilyMemberRelation
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteChronicDisease",
                new { ChronicDiseaseID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
