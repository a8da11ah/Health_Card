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
    public class SurgicalOperationRepository : BaseRepository, IRepositoryBase<SurgicalOperation,SurgicalOperationFilter>
    {
        public SurgicalOperationRepository(IDbConnection connection) : base(connection)
        {
        }



        public async Task<IEnumerable<SurgicalOperation>> GetAllAsync(SurgicalOperationFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM SurgicalOperations WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.OperationType))
            {
                sql.Append(" AND OperationType LIKE @OperationType");
                filter.OperationType = $"%{filter.OperationType}%";
            }

            if (!string.IsNullOrEmpty(filter.HospitalName))
            {
                sql.Append(" AND HospitalName LIKE @HospitalName");
                filter.HospitalName = $"%{filter.HospitalName}%";
            }

            sql.Append(" ORDER BY SurgicalOperationID"); // Replace with your actual PK or appropriate column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.OperationType,
                filter.HospitalName,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<SurgicalOperation>(sql.ToString(), parameters);
        }

        public async Task<SurgicalOperation> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<SurgicalOperation>(
                "spGetSurgicalOperationById", 
                new { OperationID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(SurgicalOperation entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateSurgicalOperation",
                new
                {
                    entity.ServantID,
                    entity.OperationDate,
                    entity.OperationType,
                    entity.HospitalName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);

            entity.OperationID = id;
        }

        public async Task UpdateAsync(SurgicalOperation entity)
        {
            await ExecuteAsync(
                "spUpdateSurgicalOperation",
                new
                {
                    entity.OperationID,
                    entity.ServantID,
                    entity.OperationDate,
                    entity.OperationType,
                    entity.HospitalName,
                    entity.Notes
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteSurgicalOperation",
                new { OperationID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
