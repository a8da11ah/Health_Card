using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Model
{
    public class GeneralRemark
    {
        [Key]
        public int RemarkID { get; set; }

        public int ServantID { get; set; }

        public string? Remarks { get; set; }

        public string? OtherNotes { get; set; }

        [StringLength(255)]
        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}