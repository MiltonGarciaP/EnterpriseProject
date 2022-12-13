﻿using Application.Repository;
using Application.ViewModels.Employee;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeServices : IHelpersServices<EmployeeViewModel>
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly PayrollRepository _payrollRepository;
        private readonly VacantionRepository _vacantionRepository;

        public EmployeeServices(ApplicationContext dbContext)
        {
            _employeeRepository = new(dbContext);
            _payrollRepository = new(dbContext);
            _vacantionRepository = new(dbContext);
        }

        public async Task<EmployeeViewModel> Add(EmployeeViewModel vm)
        {
            Employee employee = new();
            employee.EmployeeName = vm.EmployeeName;
            employee.Email = vm.Email;
            employee.IdCard = vm.IdCard;
            employee.PhoneNumber = vm.PhoneNumber;
            employee.DOB = vm.DOB;
            employee.DOA = vm.DOA;
            employee.Position = vm.Position;
            employee.PayrollId = vm.PayrollId;
            employee.VacantionId = vm.VacantionId;

            employee = await _employeeRepository.AddAsync(employee);

            EmployeeViewModel pvm = new();
            pvm.Id = employee.Id;
            pvm.EmployeeName = employee.EmployeeName;
            pvm.Email = employee.Email;
            pvm.IdCard = employee.IdCard;
            pvm.PhoneNumber = employee.PhoneNumber;
            pvm.DOB = employee.DOB;
            pvm.DOA = employee.DOA;
            pvm.Position = employee.Position;
            pvm.PayrollId = employee.PayrollId;
            pvm.VacantionId = employee.VacantionId;

            return pvm;
        }

        public async Task Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task<List<EmployeeViewModel>> GetAllViewModel()
        {
            var productList = await _employeeRepository.GetAllWithIncludeAsync(new List<string> { "Payroll" });

            return productList.Select(product => new EmployeeViewModel
            {
                Id = product.Id,
                EmployeeName = product.EmployeeName,
                PhoneNumber = product.PhoneNumber,
                Email = product.Email,
                IdCard = product.IdCard,
                DOA = product.DOA,
                DOB = product.DOB,
                Position = product.Position,
                Wage = _payrollRepository.GetAmount(product.PayrollId),
                //PayrollId = product.PayrollId,
                //VacantionId = product.VacantionId,
                VacantionName = _vacantionRepository.GetVacation(product.VacantionId),
                StartingDate = _vacantionRepository.GetVacationStartingDate(product.VacantionId),
                EndingDate = _vacantionRepository.GetVacantionEndingDate(product.VacantionId)
            }).ToList();
        }

        public async Task<EmployeeViewModel> GetByIdViewModel(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            EmployeeViewModel vm = new();
            vm.Id = employee.Id;
            vm.EmployeeName = employee.EmployeeName;
            vm.PhoneNumber = employee.PhoneNumber;
            vm.Email = employee.Email;
            vm.IdCard = employee.IdCard;
            vm.DOA = employee.DOA;
            vm.DOB = employee.DOB;
            vm.Position = employee.Position;
            vm.PayrollId = employee.PayrollId;
            vm.VacantionId = employee.VacantionId;

            return vm;
        }

        public async Task Update(EmployeeViewModel vm)
        {
            var employee = await _employeeRepository.GetByIdAsync(vm.Id);

            employee.Id = vm.Id;
            employee.EmployeeName = vm.EmployeeName;
            employee.Email = vm.Email;
            employee.IdCard = vm.IdCard;
            employee.PhoneNumber = vm.PhoneNumber;
            employee.DOB = vm.DOB;
            employee.DOA = vm.DOA;
            employee.Position = vm.Position;
            employee.PayrollId = vm.PayrollId;
            employee.VacantionId = vm.VacantionId;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
