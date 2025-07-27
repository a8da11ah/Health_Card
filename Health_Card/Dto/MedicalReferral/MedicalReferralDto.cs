using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class MedicalReferralDto
    {
        public int ReferralID { get; set; }
        public int ServantID { get; set; }
        public DateTime ReferralDate { get; set; }
        public string? MedicalDiagnosis { get; set; }
        public string? LeaveType { get; set; }
        public int? LeaveDays { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMedicalReferralDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        public DateTime ReferralDate { get; set; }

        [Required]
        public string MedicalDiagnosis { get; set; }

        [StringLength(255)]
        public string LeaveType { get; set; }

        [Range(0, 365)]
        public int? LeaveDays { get; set; }
    }

    public class UpdateMedicalReferralDto
    {
        [Required]
        public DateTime ReferralDate { get; set; }

        [Required]
        public string MedicalDiagnosis { get; set; }

        [StringLength(255)]
        public string LeaveType { get; set; }

        [Range(0, 365)]
        public int? LeaveDays { get; set; }
    }
}