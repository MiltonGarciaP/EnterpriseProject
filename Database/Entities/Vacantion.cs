using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Vacantion
    {
        public int Id { get; set; }
        public string VacationName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
