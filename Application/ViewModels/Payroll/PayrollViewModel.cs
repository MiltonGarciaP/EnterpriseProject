using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public int Id { get; set; }
        public double Earnings { get; set; }

        public double afp { get; set; }

        public double sfs { get; set; }

        public double isr { get; set; }

        public double Discount { get; set; }

        public double Earning { get; set; }

        public double Total { get; set; }
    }
}
