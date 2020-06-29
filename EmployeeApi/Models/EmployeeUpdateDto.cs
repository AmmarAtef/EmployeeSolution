using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeApi.Models
{
    public class EmployeeUpdateDto
    {
        [Required(ErrorMessage ="Id is required")]
        public int ID { get; set; }

        [MaxLength(100)]
        [MinLength(20)]
        public string EmployeeName { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmployeeBirthdate { get; set; }

        public string EmployeeGender { get; set; }

        public double EmployeeSalary { get; set; }

        public string EmployeeDepartment { get; set; }

        [MaxLength(1000, ErrorMessage = "length should less 1000")]
        public string BriefOfExperience { get; set; }

        [DataType(DataType.Url)]
        public string ProfileURL { get; set; }

        public string Description { get; set; }
    }
}