using Application.Repository;
using Application.ViewModels.Position;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PositionServices : IHelpersServices<PositionViewModel>
    {
        private readonly PositionRepository _positionRepository;
        public PositionServices(ApplicationContext dbContext)
        {
            _positionRepository = new(dbContext);
        }

        public async Task<PositionViewModel> Add(PositionViewModel vm)
        {
            Position position = new();
            position.PositionName = vm.PositionName;

            position = await _positionRepository.AddAsync(position);

            PositionViewModel pvm = new();
            pvm.Id = position.Id;
            pvm.PositionName = position.PositionName;

            return pvm;
        }

        public async Task Delete(int id)
        {
            var position = await _positionRepository.GetByIdAsync(id);

            await _positionRepository.DeleteAsync(position);
        }

        public async Task<List<PositionViewModel>> GetAllViewModel()
        {
            var positionList = await _positionRepository.GetAllWithIncludeAsync(new List<string> { "Employees" });

            return positionList.Select(position => new PositionViewModel
            {
                Id = position.Id,
                PositionName = position.PositionName
            }).ToList();
        }

        public async Task<PositionViewModel> GetByIdViewModel(int id)
        {
            var position = await _positionRepository.GetByIdAsync(id);

            PositionViewModel pvm = new();
            pvm.Id = position.Id;
            pvm.PositionName = position.PositionName;

            return pvm;
        }

        public async Task Update(PositionViewModel vm)
        {
            var position = await _positionRepository.GetByIdAsync(vm.Id);

            position.Id = vm.Id;
            position.PositionName = vm.PositionName;

            await _positionRepository.UpdateAsync(position);
        }
    }
}
