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
    public class EmployeeRepository : IHelpersRepository<Employee>
    {

        private readonly ApplicationContext _dbContext;

        public EmployeeRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Set<Employee>().Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Set<Employee>().ToListAsync();
        }

        public async Task<List<Employee>> GetAllWithIncludeAsync(List<string> property)
        {
            var query = _dbContext.Set<Employee>().AsQueryable();

            foreach(var properties in property)
            {
                query = query.Include(properties);
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Employee>().FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public string GetEmployeeName(int id)
        {
            return _dbContext.Set<Employee>().FindAsync(id).Result.EmployeeName;
        }
    }
}
