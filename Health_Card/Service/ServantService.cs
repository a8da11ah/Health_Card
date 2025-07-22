using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.servant;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ServantService : IServantService
    {
        private readonly IServantRepository _servantRepository;

        public ServantService(IServantRepository servantRepository)
        {
            _servantRepository = servantRepository;
        }

        public async Task<IEnumerable<Servant>> GetAllAsync()
        {
            return await _servantRepository.GetAllAsync();
        }

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