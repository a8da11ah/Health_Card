using Health_Card.Base;

namespace Health_Card.Dto
{
    public class MedicalReferralFilter:BaseFilter
    {
        public string? HospitalName { get; set; }
        public string? DoctorName { get; set; }
    }
}