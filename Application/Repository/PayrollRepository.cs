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
    public class PayrollRepository : IHelpersRepository<Payroll>
    {
        private readonly ApplicationContext _dbContext;

        public PayrollRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payroll> AddAsync(Payroll payroll)
        {
            await _dbContext.Payrolls.AddAsync(payroll);
            await _dbContext.SaveChangesAsync();
            return payroll;
        }

        public async Task DeleteAsync(Payroll payroll)
        {
            _dbContext.Set<Payroll>().Remove(payroll);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Payroll>> GetAllAsync()
        {
            return await _dbContext.Set<Payroll>().ToListAsync();
        }

        public async Task<List<Payroll>> GetAllWithIncludeAsync(List<string> property)
        {
            var query = _dbContext.Set<Payroll>().AsQueryable();

            foreach (var properties in property)
            {
                query = query.Include(properties);
            }

            return await query.ToListAsync();
        }

        public async Task<Payroll> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Payroll>().FindAsync(id);
        }

        public async Task UpdateAsync(Payroll payroll)
        {
            _dbContext.Entry(payroll).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
