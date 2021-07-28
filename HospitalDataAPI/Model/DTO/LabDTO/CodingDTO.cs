using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class CodingDTO
    {
        public int TestCodeId { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
}
