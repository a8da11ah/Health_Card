using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.Vaccination;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class VaccinationRepository : BaseRepository, IVaccinationRepository
    {
        public VaccinationRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Vaccination>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Vaccinations";
            return await QueryAsync<Vaccination>(sql);
        }

        public async Task<Vaccination> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<Vaccination>(
                "spGetVaccinationById", 
                new { VaccinationID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(Vaccination entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateVaccination",
                new
                {
                    entity.ServantID,
                    entity.VaccinationDate,
                    entity.VaccinationType,
                    entity.VaccinationLocation,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);

            entity.VaccinationID = id;
        }

        public async Task UpdateAsync(Vaccination entity)
        {
            await ExecuteAsync(
                "spUpdateVaccination",
                new
                {
                    entity.VaccinationID,
                    entity.ServantID,
                    entity.VaccinationDate,
                    entity.VaccinationType,
                    entity.VaccinationLocation,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteVaccination",
                new { VaccinationID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
