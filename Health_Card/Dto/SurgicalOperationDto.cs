using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class SurgicalOperationDto
    {
        public int OperationID { get; set; }
        public int ServantID { get; set; }
        public DateTime OperationDate { get; set; }
        public string OperationType { get; set; }
        public string? HospitalName { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateSurgicalOperationDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string OperationType { get; set; }

        [StringLength(255)]
        public string? HospitalName { get; set; }

        public string? Notes { get; set; }
    }

    public class UpdateSurgicalOperationDto
    {
        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string OperationType { get; set; }

        [StringLength(255)]
        public string? HospitalName { get; set; }

        public string? Notes { get; set; }
    }
}