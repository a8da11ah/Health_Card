using System;
using Health_Card.Base;
namespace Health_Card.Dto
{

    public class ServantChronicTreatmentFilter : BaseFilter
    {
        public int? ServantID { get; set; }

        // Use this for partial search on TreatmentName
        public string? TreatmentName { get; set; }
    }
}