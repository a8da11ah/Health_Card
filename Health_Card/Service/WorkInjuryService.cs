using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.WorkInjury;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class WorkInjuryService : IWorkInjuryService
    {
        private readonly IWorkInjuryRepository _workInjuryRepository;

        public WorkInjuryService(IWorkInjuryRepository workInjuryRepository)
        {
            _workInjuryRepository = workInjuryRepository;
        }

        public async Task<IEnumerable<WorkInjury>> GetAllAsync()
        {
            return await _workInjuryRepository.GetAllAsync();
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
