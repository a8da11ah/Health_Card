using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class ServantChronicTreatmentDto
    {
        public int TreatmentID { get; set; }
        public int ServantID { get; set; }
        public string TreatmentName { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateServantChronicTreatmentDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        [StringLength(255)]
        public string TreatmentName { get; set; }

        public string? Notes { get; set; }
    }

    public class UpdateServantChronicTreatmentDto
    {
        [Required]
        [StringLength(255)]
        public string TreatmentName { get; set; }

        public string? Notes { get; set; }
    }
}