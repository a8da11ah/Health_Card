
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Dto;
using Health_Card.Model;
using Dapper;
using Health_Card.Interface;

namespace Health_Card.Repository
{
    public class ChronicDiseaseRepository : BaseRepository, IRepositoryBase<ChronicDisease,ChronicDiseaseFilter>
    {
        public ChronicDiseaseRepository(IDbConnection connection) : base(connection)
        {
        }

 

        public async Task<IEnumerable<ChronicDisease>> GetAllAsync(ChronicDiseaseFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM ChronicDiseases WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.DiseaseName))
            {
                sql.Append(" AND DiseaseName LIKE @DiseaseName");
                filter.DiseaseName = $"%{filter.DiseaseName}%";
            }

            if (filter.DiseaseType.HasValue)
            {
                sql.Append(" AND DiseaseType = @DiseaseType");
            }

            sql.Append(" ORDER BY ChronicDiseaseID"); // assuming you have a PK or column to order by

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.DiseaseName,
                filter.DiseaseType,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<ChronicDisease>(sql.ToString(), parameters);
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
