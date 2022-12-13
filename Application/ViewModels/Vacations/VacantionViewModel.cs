using Application.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Vacations
{
    public class VacantionViewModel
    {
        public int Id { get; set; }
        public string VacantionName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public List<EmployeeViewModel> EmployeeViewModels { get; set; }
    }
}
