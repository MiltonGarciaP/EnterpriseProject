using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class VacantionRepository : IHelpersRepository<Vacantion>
    {
        private readonly ApplicationContext _dbContext;

        public VacantionRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vacantion> AddAsync(Vacantion vacantion)
        {
            await _dbContext.Vacantions.AddAsync(vacantion);
            await _dbContext.SaveChangesAsync();
            return vacantion;
        }

        public async Task DeleteAsync(Vacantion vacantion)
        {
            _dbContext.Set<Vacantion>().Remove(vacantion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vacantion>> GetAllAsync()
        {
            return await _dbContext.Set<Vacantion>().ToListAsync();
        }

        public async Task<List<Vacantion>> GetAllWithIncludeAsync(List<string> property)
        {
            var query = _dbContext.Set<Vacantion>().AsQueryable();

            foreach (var properties in property)
            {
                query = query.Include(properties);
            }

            return await query.ToListAsync();
        }

        public async Task<Vacantion> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Vacantion>().FindAsync(id);
        }

        public async Task UpdateAsync(Vacantion vacantion)
        {
            _dbContext.Entry(vacantion).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public DateTime GetVacationStartingDate(int id)
        {
            return _dbContext.Set<Vacantion>().FindAsync(id).Result.StartingDate;
        }

        public DateTime GetVacantionEndingDate(int id)
        {
            return _dbContext.Set<Vacantion>().FindAsync(id).Result.EndingDate;
        }
    }
}
