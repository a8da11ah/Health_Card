using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class ServantChronicTreatment
    {
        [Key]
        public int TreatmentID { get; set; }

        public int ServantID { get; set; }

        [Required]
        [StringLength(255)]
        public string TreatmentName { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}