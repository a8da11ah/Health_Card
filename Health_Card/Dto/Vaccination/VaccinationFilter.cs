using Health_Card.Base;

namespace Health_Card.Dto
{
    public class VaccinationFilter:BaseFilter
    {
        public string? VaccinationType { get; set; }
        public string? Dose { get; set; }
    }
}
