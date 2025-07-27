using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ServantService : IServiceBase<Servant,ServantFilter>
    {
        private readonly  IServiceBase<Servant,ServantFilter> _servantRepository;

        public ServantService( IServiceBase<Servant,ServantFilter> servantRepository)
        {
            _servantRepository = servantRepository;
        }

        public async Task<IEnumerable<Servant>> GetAllAsync(ServantFilter filter)
        {
            return await _servantRepository.GetAllAsync(filter);
        }

        // public async Task<IEnumerable<Servant>> GetByFilterAsync(ServantFilter filter)
        // {
        //     return await _servantRepository.GetByFilterAsync(filter);
        // }

        public async Task<Servant> GetByIdAsync(int id)
        {
            return await _servantRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Servant servant)
        {
            // You can add business rules/validation here before calling repository
            await _servantRepository.CreateAsync(servant);
        }

        public async Task UpdateAsync(Servant servant)
        {
            // Add any additional validation or business logic here
            await _servantRepository.UpdateAsync(servant);
        }

        public async Task DeleteAsync(int id)
        {
            // Add business validation if needed before deletion
            await _servantRepository.DeleteAsync(id);
        }
    }
}