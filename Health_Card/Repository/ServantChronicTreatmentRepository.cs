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
    public class ServantChronicTreatmentRepository : BaseRepository, IRepositoryBase<ServantChronicTreatment,ServantChronicTreatmentFilter>
    {
        public ServantChronicTreatmentRepository(IDbConnection connection) : base(connection)
        {
        }
        
        public async Task<IEnumerable<ServantChronicTreatment>> GetAllAsync(ServantChronicTreatmentFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM ServantChronicTreatments WHERE 1=1");

            if (filter.ServantID.HasValue)
            {
                sql.Append(" AND ServantID = @ServantID");
            }

            if (!string.IsNullOrEmpty(filter.TreatmentName))
            {
                sql.Append(" AND TreatmentName LIKE @TreatmentName");
                filter.TreatmentName = $"%{filter.TreatmentName}%";
            }
            
            // Add ORDER BY and pagination
            sql.Append(" ORDER BY TreatmentID");  // assuming TreatmentID is PK or sortable column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new 
            {
                filter.ServantID,
                filter.TreatmentName,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<ServantChronicTreatment>(sql.ToString(), parameters);
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
