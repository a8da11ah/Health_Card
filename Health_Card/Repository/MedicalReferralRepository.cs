using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.MedicalReferral;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class MedicalReferralRepository : BaseRepository, IMedicalReferralRepository
    {
        public MedicalReferralRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<MedicalReferral>> GetAllAsync()
        {
            const string sql = "SELECT * FROM MedicalReferrals";
            return await QueryAsync<MedicalReferral>(sql);
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
