using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class Servant
    {
        [Key]
        public int ServantID { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(10)]
        public string BloodType { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(255)]
        public string EducationalQualification { get; set; }

        public bool SmokingStatus { get; set; } = false;

        public string DrugAllergies { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}