using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class WorkInjuryService : IServiceBase<WorkInjury,WorkInjuryFilter>
    {
        private readonly  IRepositoryBase<WorkInjury,WorkInjuryFilter> _workInjuryRepository;

        public WorkInjuryService( IRepositoryBase<WorkInjury,WorkInjuryFilter> workInjuryRepository)
        {
            _workInjuryRepository = workInjuryRepository;
        }

        public async Task<IEnumerable<WorkInjury>> GetAllAsync(WorkInjuryFilter filter )
        {
            return await _workInjuryRepository.GetAllAsync(filter);
        }

        public async Task<WorkInjury> GetByIdAsync(int id)
        {
            return await _workInjuryRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(WorkInjury workInjury)
        {
            await _workInjuryRepository.CreateAsync(workInjury);
        }

        public async Task UpdateAsync(WorkInjury workInjury)
        {
            await _workInjuryRepository.UpdateAsync(workInjury);
        }

        public async Task DeleteAsync(int id)
        {
            await _workInjuryRepository.DeleteAsync(id);
        }
    }
}
