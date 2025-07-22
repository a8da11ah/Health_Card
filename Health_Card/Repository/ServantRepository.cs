using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface;
using Health_Card.Model;
using Dapper;
using Health_Card.Interface.servant;

namespace Health_Card.Repository
{
    public class ServantRepository : BaseRepository,IServantRepository
    {
        public ServantRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Servant>> GetAllAsync()
        {
            const string sql = "SELECT * FROM vwServants";
            return await QueryAsync<Servant>(sql);
        }

        public async Task<Servant> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<Servant>(
                "spGetServantById",
                new { ServantID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(Servant entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateServant",
                new
                {
                    entity.BirthDate,
                    entity.Gender,
                    entity.MaritalStatus,
                    entity.BloodType,
                    entity.Height,
                    entity.Weight,
                    entity.EducationalQualification,
                    entity.SmokingStatus,
                    entity.DrugAllergies
                },
                commandType: CommandType.StoredProcedure);

            entity.ServantID = id;
        }

        public async Task UpdateAsync(Servant entity)
        {
            await ExecuteAsync(
                "spUpdateServant",
                new
                {
                    entity.ServantID,
                    entity.BirthDate,
                    entity.Gender,
                    entity.MaritalStatus,
                    entity.BloodType,
                    entity.Height,
                    entity.Weight,
                    entity.EducationalQualification,
                    entity.SmokingStatus,
                    entity.DrugAllergies
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteServant",
                new { ServantID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
