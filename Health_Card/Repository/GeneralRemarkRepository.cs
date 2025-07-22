using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Health_Card.Base;
using Health_Card.Interface.GeneralRemark;
using Health_Card.Model;
using Dapper;

namespace Health_Card.Repository
{
    public class GeneralRemarkRepository : BaseRepository, IGeneralRemarkRepository
    {
        public GeneralRemarkRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<GeneralRemark>> GetAllAsync()
        {
            const string sql = "SELECT * FROM GeneralRemarks";
            return await QueryAsync<GeneralRemark>(sql);
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
