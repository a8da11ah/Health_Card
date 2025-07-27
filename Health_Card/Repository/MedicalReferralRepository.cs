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
    public class MedicalReferralRepository : BaseRepository, IRepositoryBase<MedicalReferral, MedicalReferralFilter>
    {
        public MedicalReferralRepository(IDbConnection connection) : base(connection)
        {
        }


        public async Task<IEnumerable<MedicalReferral>> GetAllAsync(MedicalReferralFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM MedicalReferrals WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.HospitalName))
            {
                sql.Append(" AND HospitalName LIKE @HospitalName");
                filter.HospitalName = $"%{filter.HospitalName}%";
            }

            if (!string.IsNullOrEmpty(filter.DoctorName))
            {
                sql.Append(" AND DoctorName LIKE @DoctorName");
                filter.DoctorName = $"%{filter.DoctorName}%";
            }

            sql.Append(" ORDER BY MedicalReferralID"); // Replace with your PK or appropriate column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.HospitalName,
                filter.DoctorName,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<MedicalReferral>(sql.ToString(), parameters);
        }


        public async Task<MedicalReferral> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<MedicalReferral>(
                "spGetMedicalReferralById", 
                new { ReferralID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(MedicalReferral entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateMedicalReferral",
                new
                {
                    entity.ServantID,
                    entity.ReferralDate,
                    entity.MedicalDiagnosis,
                    entity.LeaveType,
                    entity.LeaveDays
                },
                commandType: CommandType.StoredProcedure);

            entity.ReferralID = id;
        }

        public async Task UpdateAsync(MedicalReferral entity)
        {
            await ExecuteAsync(
                "spUpdateMedicalReferral",
                new
                {
                    entity.ReferralID,
                    entity.ServantID,
                    entity.ReferralDate,
                    entity.MedicalDiagnosis,
                    entity.LeaveType,
                    entity.LeaveDays
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteMedicalReferral",
                new { ReferralID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
