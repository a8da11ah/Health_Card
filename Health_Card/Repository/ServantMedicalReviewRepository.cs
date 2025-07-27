using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Dto;
using Health_Card.Model;
using Health_Card.Interface;
using Dapper;

namespace Health_Card.Repository
{
    public class ServantMedicalReviewRepository : BaseRepository, IRepositoryBase<ServantMedicalReview,ServantMedicalReviewFilter>
    {
        public ServantMedicalReviewRepository(IDbConnection connection) : base(connection)
        {
        }



        public async Task<IEnumerable<ServantMedicalReview>> GetAllAsync(ServantMedicalReviewFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM ServantMedicalReviews WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.ReviewType))
            {
                sql.Append(" AND ReviewType LIKE @ReviewType");
                filter.ReviewType = $"%{filter.ReviewType}%";
            }

            if (!string.IsNullOrEmpty(filter.DoctorName))
            {
                sql.Append(" AND DoctorName LIKE @DoctorName");
                filter.DoctorName = $"%{filter.DoctorName}%";
            }

            sql.Append(" ORDER BY ReviewID"); // Replace ReviewID with your actual primary key or a suitable column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.ReviewType,
                filter.DoctorName,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<ServantMedicalReview>(sql.ToString(), parameters);
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
