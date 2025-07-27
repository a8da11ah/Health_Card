using Health_Card.Base;

namespace Health_Card.Dto
{
    public class ServantMedicalReviewFilter:BaseFilter
    {
        public string? ReviewType { get; set; }
        public string? DoctorName { get; set; }
    }
}
