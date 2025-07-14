using System;
using System.ComponentModel.DataAnnotations;
using Health_Card.Enums;

namespace Health_Card.Model
{
    public class ChronicDisease
    {
        [Key]
        public int ChronicDiseaseID { get; set; }

        public int ServantID { get; set; }

        [Required]
        [StringLength(255)]
        public string DiseaseName { get; set; }

        public string Notes { get; set; }

        public DiseaseType DiseaseType { get; set; } = DiseaseType.PERSONAL;

        [StringLength(100)]
        public string FamilyMemberRelation { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}