using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class WorkInjuryDto
    {
        public int InjuryID { get; set; }
        public int ServantID { get; set; }
        public DateTime InjuryDate { get; set; }
        public string InjuryType { get; set; }
        public string DepartmentOfInjury { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateWorkInjuryDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        public DateTime InjuryDate { get; set; }

        [Required]
        [StringLength(255)]
        public string InjuryType { get; set; }

        [StringLength(255)]
        public string DepartmentOfInjury { get; set; }

        public string Description { get; set; }
    }

    public class UpdateWorkInjuryDto
    {
        [Required]
        public DateTime InjuryDate { get; set; }

        [Required]
        [StringLength(255)]
        public string InjuryType { get; set; }

        [StringLength(255)]
        public string DepartmentOfInjury { get; set; }

        public string Description { get; set; }
    }
}