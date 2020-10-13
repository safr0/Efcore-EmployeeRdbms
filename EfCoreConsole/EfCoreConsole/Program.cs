using EfCoreConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;            
            //TestDataInjection();         //uncomment for data entry
            DisplayOrganisationHierarchy();            
        }

        /// <summary>
        /// Print CEO and Manager names
        /// </summary>
        private static void DisplayOrganisationHierarchy()
        {
            using (var db = new EmployeeDbContext())
            {                
                var ceo = db.EmployeeManagerList.Include(x => x.Manager).Where(x => x.ManagerID == null).Include(x => x.Employee).FirstOrDefault();
                //print managers
                Console.WriteLine($"{ceo.Employee.EmployeeName}");

                var managers = db.EmployeeManagerList.Include(x => x.Employee).Where(x => x.ManagerID== ceo.EmployeeID);

                foreach (var manager in managers)
                {
                    //print managers
                    Console.WriteLine($"\t{manager.Employee.EmployeeName}");                    
                    DisplayOrganisationHierarchy(manager.EmployeeID);
                }
            }
        }

        /// <summary>
        /// Print employees names for all managers
        /// </summary>
        /// <param name="managerId"></param>
        private static void DisplayOrganisationHierarchy(int managerId) 
        {
            using (var db = new EmployeeDbContext())
            {                
                var employeeList = db.EmployeeManagerList.Include(x => x.Employee).Where(x => x.ManagerID == managerId);
                foreach (var employee in employeeList)
                {                    
                    Console.WriteLine($"\t\t{employee.Employee.EmployeeName}");
                }                
            }
        }


        /// <summary>
        /// Test data
        /// </summary>
        private static void TestDataInjection()
        {
            using (var db = new EmployeeDbContext())
            {
                var employee1 = new Employee() { EmployeeID = 100, EmployeeName = "Alan" };
                var employee2 = new Employee() { EmployeeID = 220, EmployeeName = "Martin" };
                var employee3 = new Employee() { EmployeeID = 150, EmployeeName = "Jamie" };
                var employee4 = new Employee() { EmployeeID = 275, EmployeeName = "Alex" };
                var employee5 = new Employee() { EmployeeID = 400, EmployeeName = "Steve" };
                var employee6 = new Employee() { EmployeeID = 190, EmployeeName = "David" };

                db.EmployeeList.Add(employee1);
                db.EmployeeList.Add(employee2);
                db.EmployeeList.Add(employee3);
                db.EmployeeList.Add(employee4);
                db.EmployeeList.Add(employee5);
                db.EmployeeList.Add(employee6);

                db.SaveChanges();

                var employeManager1 = new EmployeeManagerRelation() { EmployeeID = 100, ManagerID = 150 };
                var employeManager2 = new EmployeeManagerRelation() { EmployeeID = 220, ManagerID = 100 };
                var employeManager3 = new EmployeeManagerRelation() { EmployeeID = 150 };
                var employeManager4 = new EmployeeManagerRelation() { EmployeeID = 275, ManagerID = 100 };
                var employeManager5 = new EmployeeManagerRelation() { EmployeeID = 400, ManagerID = 150 };
                var employeManager6 = new EmployeeManagerRelation() { EmployeeID = 190, ManagerID = 400 };

                db.EmployeeManagerList.Add(employeManager1);
                db.EmployeeManagerList.Add(employeManager2);
                db.EmployeeManagerList.Add(employeManager3);
                db.EmployeeManagerList.Add(employeManager4);
                db.EmployeeManagerList.Add(employeManager5);
                db.EmployeeManagerList.Add(employeManager6);

                db.SaveChanges();
            }
        }
    }
}
