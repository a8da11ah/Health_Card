using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class ServantDto
    {
        public int ServantID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodType { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string EducationalQualification { get; set; }
        public bool SmokingStatus { get; set; }
        public string DrugAllergies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateServantDto
    {
        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(10)]
        public string BloodType { get; set; }

        [Range(0, 300)]
        public decimal? Height { get; set; }

        [Range(0, 500)]
        public decimal? Weight { get; set; }

        [StringLength(255)]
        public string EducationalQualification { get; set; }

        public bool SmokingStatus { get; set; } = false;

        public string DrugAllergies { get; set; }
    }

    public class UpdateServantDto
    {
        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(10)]
        public string BloodType { get; set; }

        [Range(0, 300)]
        public decimal? Height { get; set; }

        [Range(0, 500)]
        public decimal? Weight { get; set; }

        [StringLength(255)]
        public string EducationalQualification { get; set; }

        public bool SmokingStatus { get; set; }

        public string DrugAllergies { get; set; }
    }
}