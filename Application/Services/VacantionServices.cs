using Application.Repository;
using Application.ViewModels.Vacations;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VacantionServices : IHelpersServices<VacantionViewModel>
    {
        private readonly VacantionRepository _vacantionRepository;
        private readonly EmployeeRepository _employeeRepository;

        public VacantionServices(ApplicationContext dbContext)
        {
            _vacantionRepository = new(dbContext);
            _employeeRepository = new(dbContext);
        }

        public async Task<VacantionViewModel> Add(VacantionViewModel vm)
        {
            Vacantion vacantion = new();
            vacantion.StartingDate = vm.StartingDate;
            vacantion.EndingDate = vm.EndingDate;

            vacantion = await _vacantionRepository.AddAsync(vacantion);

            VacantionViewModel ovm = new();
            ovm.Id = vacantion.Id;
            ovm.StartingDate = vacantion.StartingDate;
            ovm.EndingDate = vacantion.EndingDate;

            return ovm;
        }

        public async Task Delete(int id)
        {
            var vacantion = await _vacantionRepository.GetByIdAsync(id);
            await _vacantionRepository.DeleteAsync(vacantion);
        }

        public async Task<List<VacantionViewModel>> GetAllViewModel()
        {
            var vacantionList = await _vacantionRepository.GetAllWithIncludeAsync(new List<string> { "Employees" });

            return vacantionList.Select(vacantion => new VacantionViewModel
            {
                Id = vacantion.Id,
                StartingDate = vacantion.StartingDate,
                EndingDate = vacantion.EndingDate,
                EmployeeName = _employeeRepository.GetEmployeeName(vacantion.Employees.First().Id)
            }).ToList();
        }

        public async Task<VacantionViewModel> GetByIdViewModel(int id)
        {
            var order = await _vacantionRepository.GetByIdAsync(id);

            VacantionViewModel vm = new();
            vm.Id = order.Id;
            vm.StartingDate = order.StartingDate;
            vm.EndingDate = order.EndingDate;

            return vm;
        }

        public async Task Update(VacantionViewModel vm)
        {
            var vacantion = await _vacantionRepository.GetByIdAsync(vm.Id);

            vacantion.Id = vm.Id;
            vacantion.StartingDate = vm.StartingDate;
            vacantion.EndingDate = vm.EndingDate;

            await _vacantionRepository.UpdateAsync(vacantion);
        }
    }
}
