using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebConsoleAppPersonnel
{
     public class Employee
     {
          public string Name { get; set; }
		public int Salary { get; set; }
		public string Department { get; set; }
		public Address Address { get; set; }

     }
}