using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class VaccinationDto
    {
        public int VaccinationID { get; set; }
        public int ServantID { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string VaccinationType { get; set; }
        public string? VaccinationLocation { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateVaccinationDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        public DateTime VaccinationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string VaccinationType { get; set; }

        [StringLength(255)]
        public string? VaccinationLocation { get; set; }

        public string? Notes { get; set; }
    }

    public class UpdateVaccinationDto
    {
        [Required]
        public DateTime VaccinationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string VaccinationType { get; set; }

        [StringLength(255)]
        public string? VaccinationLocation { get; set; }

        public string? Notes { get; set; }
    }
}