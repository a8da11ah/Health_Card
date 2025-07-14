using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class Vaccination
    {
        [Key]
        public int VaccinationID { get; set; }

        public int ServantID { get; set; }

        [Required]
        public DateTime VaccinationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string VaccinationType { get; set; }

        [StringLength(255)]
        public string VaccinationLocation { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}