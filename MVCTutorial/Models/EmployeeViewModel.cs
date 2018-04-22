using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class EmployeeViewModel
    {
   
        public int EmployeeID { get; set; }
        //[Required(ErrorMessage ="Enter Name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DepartmentID { get; set; }
        //[Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        public bool Isbool { get; set; }
        public string DepartmentName { get; set; }
     
    }
}