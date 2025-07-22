using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.GeneralRemark
{
    public interface IGeneralRemarkService
    {
        Task<IEnumerable<Model.GeneralRemark>> GetAllAsync();
        Task<Model.GeneralRemark> GetByIdAsync(int id);
        Task CreateAsync(Model.GeneralRemark generalRemark);
        Task UpdateAsync(Model.GeneralRemark generalRemark);
        Task DeleteAsync(int id);
    }
}
