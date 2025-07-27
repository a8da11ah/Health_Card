using Health_Card.Base;

namespace Health_Card.Dto
{
    public class SurgicalOperationFilter:BaseFilter
    {
        public string? OperationType { get; set; }
        public string? HospitalName { get; set; }
    }
}
