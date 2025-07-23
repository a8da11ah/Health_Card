using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class WorkInjury
    {
        [Key]
        public int InjuryID { get; set; }

        public int ServantID { get; set; }

        [Required]
        public DateTime InjuryDate { get; set; }

        [Required]
        [StringLength(255)]
        public string InjuryType { get; set; }

        [StringLength(255)]
        public string? DepartmentOfInjury { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}