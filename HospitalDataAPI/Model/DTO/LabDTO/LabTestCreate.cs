using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestCreate
    {

        ///<example>pending</example>
        [Required(ErrorMessage ="Enter Final/Pending")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter Code Id")]
        public int CodeId { get; set; }
        [Required(ErrorMessage = "Enter Category Id")]
        public int CategoryId { get; set; }


    }
}
