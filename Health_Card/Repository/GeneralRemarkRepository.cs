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
    public class GeneralRemarkRepository : BaseRepository, IRepositoryBase<GeneralRemark,GeneralRemarkFilter>
    {
        public GeneralRemarkRepository(IDbConnection connection) : base(connection)
        {
        }
        
        public async Task<IEnumerable<GeneralRemark>> GetAllAsync(GeneralRemarkFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM GeneralRemarks WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.CreatedBy))
            {
                sql.Append(" AND CreatedBy LIKE @CreatedBy");
                filter.CreatedBy = $"%{filter.CreatedBy}%";
            }

            sql.Append(" ORDER BY GeneralRemarkID"); // Replace with your actual PK or suitable column

            sql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new
            {
                filter.CreatedBy,
                Offset = (filter.Page - 1) * filter.PageSize,
                PageSize = filter.PageSize
            };

            return await QueryAsync<GeneralRemark>(sql.ToString(), parameters);
        }


        public async Task<GeneralRemark> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync<GeneralRemark>(
                "spGetGeneralRemarkById", 
                new { RemarkID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(GeneralRemark entity)
        {
            var id = await ExecuteScalarAsync(
                "spCreateGeneralRemark",
                new
                {
                    entity.ServantID,
                    entity.Remarks,
                    entity.OtherNotes,
                    entity.CreatedBy
                },
                commandType: CommandType.StoredProcedure);

            entity.RemarkID = id;
        }

        public async Task UpdateAsync(GeneralRemark entity)
        {
            await ExecuteAsync(
                "spUpdateGeneralRemark",
                new
                {
                    entity.RemarkID,
                    entity.ServantID,
                    entity.Remarks,
                    entity.OtherNotes,
                    entity.CreatedBy
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteAsync(
                "spDeleteGeneralRemark",
                new { RemarkID = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
