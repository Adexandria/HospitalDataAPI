using System.ComponentModel.DataAnnotations;


namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsCreate
    {
        ///<example>active</example>
        [Required(ErrorMessage = "Enter Active/InActive")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter Presciber")]
        public string Prescriber { get; set; }
        [Required(ErrorMessage = "Enter MedicationId")]
        public int MedicationId { get; set; }

    }
}
