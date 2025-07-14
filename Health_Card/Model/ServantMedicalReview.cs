using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class ServantMedicalReview
    {
        [Key]
        public int ReviewID { get; set; }

        public int ServantID { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [StringLength(100)]
        public string ReviewType { get; set; }

        public string MedicalDiagnosis { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}