using Health_Card.Base;
using Health_Card.Enums;

namespace Health_Card.Dto
{
    public class ChronicDiseaseFilter:BaseFilter
    {
        public string? DiseaseName { get; set; }
        public DiseaseType? DiseaseType { get; set; }
    }
}
