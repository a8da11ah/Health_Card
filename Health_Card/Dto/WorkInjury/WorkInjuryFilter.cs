using Health_Card.Base;

namespace Health_Card.Dto
{
    public class WorkInjuryFilter:BaseFilter
    {
        public string? InjuryType { get; set; }
        public string? DoctorName { get; set; }
    }
}
