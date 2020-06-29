using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeApi.Models
{
    public class EmployeeDto
    {
        [Required(ErrorMessage ="Id is required")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Employeee Name Required")]
        [MaxLength(100)]
        [MinLength(20)]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [DataType(DataType.Date)]
        public DateTime EmployeeBirthdate { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string EmployeeGender { get; set; }


        [Required(ErrorMessage = "Salary is Required")]
        public double EmployeeSalary { get; set; }

        [Required(ErrorMessage = "EmployeeDepartment is Required")]
        public string EmployeeDepartment { get; set; }

        [MaxLength(1000, ErrorMessage = "length should less 1000")]
        public string BriefOfExperience { get; set; }

        [DataType(DataType.Url)]
        public string ProfileURL { get; set; }

        public string Description { get; set; }
    }
}