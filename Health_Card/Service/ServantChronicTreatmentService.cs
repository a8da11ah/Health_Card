using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;
using Health_Card.Dto;
using Health_Card.Interface;

namespace Health_Card.Service
{
    public class ServantChronicTreatmentService :  IServiceBase<ServantChronicTreatment,ServantChronicTreatmentFilter>
    {
        private readonly  IRepositoryBase<ServantChronicTreatment,ServantChronicTreatmentFilter> _servantChronicTreatmentRepository;

        public ServantChronicTreatmentService( IRepositoryBase<ServantChronicTreatment,ServantChronicTreatmentFilter> servantChronicTreatmentRepository)
        {
            _servantChronicTreatmentRepository = servantChronicTreatmentRepository;
        }


        
        public async Task<IEnumerable<ServantChronicTreatment>> GetAllAsync(ServantChronicTreatmentFilter filter)
        {
            return await _servantChronicTreatmentRepository.GetAllAsync(filter);
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
