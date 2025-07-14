using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Card.Dto
{
    public class ServantMedicalReviewDto
    {
        public int ReviewID { get; set; }
        public int ServantID { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewType { get; set; }
        public string MedicalDiagnosis { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateServantMedicalReviewDto
    {
        [Required]
        public int ServantID { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [StringLength(100)]
        public string ReviewType { get; set; }

        public string MedicalDiagnosis { get; set; }

        public string Notes { get; set; }
    }

    public class UpdateServantMedicalReviewDto
    {
        [Required]
        public DateTime ReviewDate { get; set; }

        [StringLength(100)]
        public string ReviewType { get; set; }

        public string MedicalDiagnosis { get; set; }

        public string Notes { get; set; }
    }
}