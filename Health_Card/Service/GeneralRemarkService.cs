
using Health_Card.Dto;
using Health_Card.Model;
using Health_Card.Interface;

namespace Health_Card.Service
{
    public class GeneralRemarkService : IServiceBase<GeneralRemark, GeneralRemarkFilter>
    {
        private readonly IRepositoryBase<GeneralRemark, GeneralRemarkFilter> _generalRemarkRepository;

        public GeneralRemarkService(IRepositoryBase<GeneralRemark, GeneralRemarkFilter> generalRemarkRepository)
        {
            _generalRemarkRepository = generalRemarkRepository;
        }
        
        public async Task<IEnumerable<GeneralRemark>> GetAllAsync(GeneralRemarkFilter filter)
        {
            return await _generalRemarkRepository.GetAllAsync(filter);
        }

        public async Task<GeneralRemark> GetByIdAsync(int id)
        {
            return await _generalRemarkRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(GeneralRemark generalRemark)
        {
            await _generalRemarkRepository.CreateAsync(generalRemark);
        }

        public async Task UpdateAsync(GeneralRemark generalRemark)
        {
            await _generalRemarkRepository.UpdateAsync(generalRemark);
        }

        public async Task DeleteAsync(int id)
        {
            await _generalRemarkRepository.DeleteAsync(id);
        }
    }
}
