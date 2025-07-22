using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.ServantMedicalReview;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class ServantMedicalReviewRepository : BaseRepository, IServantMedicalReviewRepository
    {
        public ServantMedicalReviewRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<ServantMedicalReview>> GetAllAsync()
        {
            const string sql = "SELECT * FROM ServantMedicalReviews";
            return await QueryAsync<ServantMedicalReview>(sql);
        }

        public async Task<ServantMedicalReview> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<ServantMedicalReview>(
                "spGetServantMedicalReviewById", 
                new { ReviewID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(ServantMedicalReview entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateServantMedicalReview",
                new
                {
                    entity.ServantID,
                    entity.ReviewDate,
                    entity.ReviewType,
                    entity.MedicalDiagnosis,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);

            entity.ReviewID = id;
        }

        public async Task UpdateAsync(ServantMedicalReview entity)
        {
            await ExecuteAsync(
                "spUpdateServantMedicalReview",
                new
                {
                    entity.ReviewID,
                    entity.ServantID,
                    entity.ReviewDate,
                    entity.ReviewType,
                    entity.MedicalDiagnosis,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteServantMedicalReview",
                new { ReviewID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
