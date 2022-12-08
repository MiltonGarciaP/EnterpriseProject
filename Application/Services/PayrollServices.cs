using Application.Repository;
using Application.ViewModels.Payroll;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PayrollServices : IHelpersServices<PayrollViewModel>
    {

        private readonly PayrollRepository _payrollRepository;

        public PayrollServices(ApplicationContext dbContext)
        {
            _payrollRepository = new(dbContext);
        }
        public async Task<PayrollViewModel> Add(PayrollViewModel vm)
        {
            Payroll payroll = new();
            payroll.ISS = vm.ISS;
            payroll.AFP = vm.AFP;
            payroll.Neto = vm.Neto;
            payroll.Earnings = vm.Earnings;

            payroll = await _payrollRepository.AddAsync(payroll);

            PayrollViewModel cvm = new();
            cvm.Id = payroll.Id;
            cvm.ISS = payroll.ISS;
            cvm.AFP = payroll.AFP;
            cvm.Neto = payroll.Neto;
            cvm.Earnings = payroll.Earnings;

            return cvm;
        }

        public async Task Delete(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);
            await _payrollRepository.DeleteAsync(payroll);
        }

        public async Task<List<PayrollViewModel>> GetAllViewModel()
        {
            var payrollList = await _payrollRepository.GetAllWithIncludeAsync(new List<string> { "Employees" });

            return payrollList.Select(client => new PayrollViewModel
            {
                Id = client.Id,
                AFP = client.AFP,
                ISS = client.ISS,
                Earnings = client.Earnings,
                Neto = client.Neto
            }).ToList();
        }

        public async Task<PayrollViewModel> GetByIdViewModel(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);

            PayrollViewModel vm = new();
            vm.Id = payroll.Id;
            vm.AFP = payroll.AFP;
            vm.ISS = payroll.ISS;
            vm.Earnings = payroll.Earnings;
            vm.Neto = payroll.Neto;

            return vm;
        }

        public async Task Update(PayrollViewModel vm)
        {
            var payroll = await _payrollRepository.GetByIdAsync(vm.Id);

            payroll.Id = vm.Id;
            payroll.AFP = vm.AFP;
            payroll.ISS = vm.ISS;
            payroll.Earnings = vm.Earnings;
            payroll.Neto = payroll.Neto;

            await _payrollRepository.UpdateAsync(payroll);
        }
    }
}
