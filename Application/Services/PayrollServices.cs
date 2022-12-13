using Application.Repository;
using Application.ViewModels.Payroll;
using Database;
using Database.Entities;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.JSInterop;
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
        private readonly ApplicationContext _dbContext;

        public PayrollServices(ApplicationContext dbContext)
        {
            _payrollRepository = new(dbContext);
            _dbContext = dbContext;
        }
        public async Task<PayrollViewModel> Add(PayrollViewModel vm)
        {
            Payroll payroll = new();
            payroll.Earnings = vm.Earnings;

            payroll = await _payrollRepository.AddAsync(payroll);

            PayrollViewModel cvm = new();
            cvm.Id = payroll.Id;
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
                Earnings = client.Earnings,
                afp = client.Earnings * 0.0287,
                sfs = client.Earnings * 0.0304,
                Discount = (client.Earnings * 0.0287) + (client.Earnings * 0.0304),
                Earning = client.Earnings- ((client.Earnings * 0.0287) + (client.Earnings * 0.0304))
                
                

            }).ToList();
        }

        public async Task<PayrollViewModel> GetByIdViewModel(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);

            PayrollViewModel vm = new();
            vm.Id = payroll.Id;
            vm.Earnings = payroll.Earnings;

            return vm;
        }

        public async Task Update(PayrollViewModel vm)
        {
            var payroll = await _payrollRepository.GetByIdAsync(vm.Id);

            payroll.Id = vm.Id;
            payroll.Earnings = vm.Earnings;

            await _payrollRepository.UpdateAsync(payroll);
        }
    }
}
