using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class SurgicalOperation
    {
        [Key]
        public int OperationID { get; set; }

        public int ServantID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string OperationType { get; set; }

        [StringLength(255)]
        public string HospitalName { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}