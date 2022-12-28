using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
