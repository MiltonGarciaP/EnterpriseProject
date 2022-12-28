using Application.ViewModels.Position;
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
    public class PositionRepository : IHelpersRepository<Position>
    {
        private readonly ApplicationContext _dbContext;

        public PositionRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Position> AddAsync(Position position)
        {
            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync();
            return position;
        }

        public async Task DeleteAsync(Position position)
        {
            _dbContext.Set<Position>().Remove(position);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _dbContext.Set<Position>().ToListAsync();
        }

        public async Task<List<Position>> GetAllWithIncludeAsync(List<string> property)
        {
            var query = _dbContext.Set<Position>().AsQueryable();

            foreach(var properties in property)
            {
                query = query.Include(properties);
            }

            return await query.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Position>().FindAsync(id);
        }

        public async Task UpdateAsync(Position position)
        {
            _dbContext.Entry(position).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public string GetPositionName(int id)
        {
            return _dbContext.Set<Position>().FindAsync(id).Result.PositionName;
        }
    }
}
