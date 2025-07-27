using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class SurgicalOperationService : IServiceBase<SurgicalOperation, SurgicalOperationFilter>
    {
        private readonly  IRepositoryBase<SurgicalOperation,SurgicalOperationFilter> _surgicalOperationRepository;

        public SurgicalOperationService( IRepositoryBase<SurgicalOperation,SurgicalOperationFilter> surgicalOperationRepository)
        {
            _surgicalOperationRepository = surgicalOperationRepository;
        }

        public async Task<IEnumerable<SurgicalOperation>> GetAllAsync(SurgicalOperationFilter filter)
        {
            return await _surgicalOperationRepository.GetAllAsync(filter);
        }

        public async Task<SurgicalOperation> GetByIdAsync(int id)
        {
            return await _surgicalOperationRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(SurgicalOperation surgicalOperation)
        {
            await _surgicalOperationRepository.CreateAsync(surgicalOperation);
        }

        public async Task UpdateAsync(SurgicalOperation surgicalOperation)
        {
            await _surgicalOperationRepository.UpdateAsync(surgicalOperation);
        }

        public async Task DeleteAsync(int id)
        {
            await _surgicalOperationRepository.DeleteAsync(id);
        }
    }
}
