using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.LabModel
{
    public class Coding
    {
        [Key]
        public int TestCodeId { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
}