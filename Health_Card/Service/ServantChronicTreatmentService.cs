using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.ServantChronicTreatment;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ServantChronicTreatmentService : IServantChronicTreatmentService
    {
        private readonly IServantChronicTreatmentRepository _servantChronicTreatmentRepository;

        public ServantChronicTreatmentService(IServantChronicTreatmentRepository servantChronicTreatmentRepository)
        {
            _servantChronicTreatmentRepository = servantChronicTreatmentRepository;
        }

        public async Task<IEnumerable<ServantChronicTreatment>> GetAllAsync()
        {
            return await _servantChronicTreatmentRepository.GetAllAsync();
        }

        public async Task<ServantChronicTreatment> GetByIdAsync(int id)
        {
            return await _servantChronicTreatmentRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(ServantChronicTreatment servantChronicTreatment)
        {
            await _servantChronicTreatmentRepository.CreateAsync(servantChronicTreatment);
        }

        public async Task UpdateAsync(ServantChronicTreatment servantChronicTreatment)
        {
            await _servantChronicTreatmentRepository.UpdateAsync(servantChronicTreatment);
        }

        public async Task DeleteAsync(int id)
        {
            await _servantChronicTreatmentRepository.DeleteAsync(id);
        }
    }
}
