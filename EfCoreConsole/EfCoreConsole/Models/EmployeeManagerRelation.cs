namespace EfCoreConsole.Models
{
    public class EmployeeManagerRelation
    {
        public virtual int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public virtual int? ManagerID { get; set; }
        public Employee Manager { get; set; }
    }
}
