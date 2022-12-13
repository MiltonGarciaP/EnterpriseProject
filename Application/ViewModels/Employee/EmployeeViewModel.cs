using Application.ViewModels.Payroll;
using Application.ViewModels.Vacations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DOB { get; set; }
        public int IdCard { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOA { get; set; }
        public string Position { get; set; }

        public double Wage { get; set; }

        public string VacantionName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public int PayrollId { get; set; }
        public List<PayrollViewModel> Payrolls { get; set; }

        public int VacantionId { get; set; }
        public List<VacantionViewModel> Vacantions { get; set; }
    }
}
