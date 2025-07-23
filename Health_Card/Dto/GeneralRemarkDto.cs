using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class GeneralRemarkDto
    {
        public int RemarkID { get; set; }
        public int ServantID { get; set; }
        public string? Remarks { get; set; }
        public string? OtherNotes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateGeneralRemarkDto
    {
        [Required]
        public int ServantID { get; set; }

        public string? Remarks { get; set; }

        public string? OtherNotes { get; set; }

        [StringLength(255)]
        public string? CreatedBy { get; set; }
    }

    public class UpdateGeneralRemarkDto
    {
        public string? Remarks { get; set; }

        public string? OtherNotes { get; set; }

        [StringLength(255)]
        public string? CreatedBy { get; set; }
    }
}