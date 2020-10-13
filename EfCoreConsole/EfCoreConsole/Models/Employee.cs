using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfCoreConsole.Models
{
    public class Employee
    {
        [Key()]
        public virtual int EmployeeID { get; set; }
        public virtual string EmployeeName { get; set; }

        public virtual ICollection<EmployeeManagerRelation> EmployeeList { get; set; }
        public virtual ICollection<EmployeeManagerRelation> Managers { get; set; }

        public Employee()
        {

        }
    }
}
