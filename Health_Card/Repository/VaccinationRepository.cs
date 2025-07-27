using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class VaccinationRepository : BaseRepository, IRepositoryBase<Vaccination,VaccinationFilter>
    {
        public VaccinationRepository(IDbConnection connection) : base(connection)
        {
        }



        public async Task<IEnumerable<Vaccination>> GetAllAsync(VaccinationFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM Vaccinations WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.VaccinationType))
            {
                sql.Append(" AND VaccinationType LIKE @VaccinationType");
                filter.VaccinationType = $"%{filter.VaccinationType}%";
            }

            if (!string.IsNullOrEmpty(filter.Dose))
            {
                sql.Append(" AND Dose LIKE @Dose");
                filter.Dose = $"%{filter.Dose}%";
            }

            sql.Append(" ORDER BY VaccinationID"); // Replace VaccinationID with your actual PK or suitable column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.VaccinationType,
                filter.Dose,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<Vaccination>(sql.ToString(), parameters);
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
