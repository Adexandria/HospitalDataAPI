﻿using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.LabModel
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Code { get; set; }
    }
}