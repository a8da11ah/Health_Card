using Health_Card.Base;

namespace Health_Card.Dto
{
    public class ServantFilter: BaseFilter
    {
        public string? BloodType { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }

    }
}
