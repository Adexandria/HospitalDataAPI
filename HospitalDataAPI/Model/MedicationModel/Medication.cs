using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.MedicationModel
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }
}