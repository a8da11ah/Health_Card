using Health_Card.Model;

namespace Health_Card.Interface;

public interface IChronicDiseaseService
{
    Task<IEnumerable<ChronicDisease>> GetAllAsync();
    Task<ChronicDisease> GetByIdAsync(int id);
    Task CreateAsync(ChronicDisease ChronicDisease);
    Task UpdateAsync(ChronicDisease ChronicDisease);
    Task DeleteAsync(int id);
}
