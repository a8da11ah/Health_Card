using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class MedicalReferral
    {
        [Key]
        public int ReferralID { get; set; }

        public int ServantID { get; set; }

        [Required]
        public DateTime ReferralDate { get; set; }

        [Required]
        public string MedicalDiagnosis { get; set; }

        [StringLength(255)]
        public string? LeaveType { get; set; }

        public int? LeaveDays { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}