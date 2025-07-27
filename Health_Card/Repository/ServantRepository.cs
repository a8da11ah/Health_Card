using System.Data;
using System.Text;
using Health_Card.Base;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;


namespace Health_Card.Repository
{
    public class ServantRepository : BaseRepository, IRepositoryBase<Servant,ServantFilter>
    {
        public ServantRepository(IDbConnection connection) : base(connection)
        {
        }

        // public async Task<IEnumerable<Servant>> GetAllAsync(ServantFilter filter)
        // {
        //     var sql = new StringBuilder("SELECT * FROM vwServants ORDER BY ServantID OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
        //     return await QueryAsync<Servant>(sql.ToString(), new { Offset = (filter.Page - 1) * filter.PageSize, PageSize = filter.PageSize });
        // }
        //
        // public async Task<IEnumerable<Servant>> GetByFilterAsync(ServantFilter filter)
        // {
        //     var sql = new StringBuilder("SELECT * FROM vwServants WHERE 1=1");
        //
        //     if (!string.IsNullOrEmpty(filter.BloodType))
        //     {
        //         sql.Append(" AND BloodType = @BloodType");
        //     }
        //
        //     if (!string.IsNullOrEmpty(filter.MaritalStatus))
        //     {
        //         sql.Append(" AND MaritalStatus = @MaritalStatus");
        //     }
        //
        //     if (!string.IsNullOrEmpty(filter.Gender))
        //     {
        //         sql.Append(" AND Gender = @Gender");
        //     }
        //
        //     sql.Append(" ORDER BY ServantID OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
        //
        //     return await QueryAsync<Servant>(sql.ToString(), new { filter.BloodType, filter.MaritalStatus, filter.Gender, Offset = (filter.Page - 1) * filter.PageSize, PageSize = filter.PageSize });
        // }
        
        public async Task<IEnumerable<Servant>> GetAllAsync(ServantFilter filter)
        {
            var sql = new StringBuilder("SELECT * FROM vwServants WHERE 1=1");

            if (!string.IsNullOrEmpty(filter.BloodType))
            {
                sql.Append(" AND BloodType = @BloodType");
            }

            if (!string.IsNullOrEmpty(filter.MaritalStatus))
            {
                sql.Append(" AND MaritalStatus = @MaritalStatus");
            }

            if (!string.IsNullOrEmpty(filter.Gender))
            {
                sql.Append(" AND Gender = @Gender");
            }
    
            sql.Append(" ORDER BY ServantID OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var parameters = new 
            { 
                filter.BloodType, 
                filter.MaritalStatus, 
                filter.Gender, 
                Offset = (filter.Page - 1) * filter.PageSize, 
                PageSize = filter.PageSize 
            };

            return await QueryAsync<Servant>(sql.ToString(), parameters);
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

        public Task<IEnumerable<Servant>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}


