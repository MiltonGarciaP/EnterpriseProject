using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public DateTime DOB { get; set; }
        public int IdCard { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOA { get; set; }

        public int VacantionId { get; set; }
        public Vacantion Vacantion { get; set; }


        public int PayrollId { get; set; }
        public Payroll Payroll { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
