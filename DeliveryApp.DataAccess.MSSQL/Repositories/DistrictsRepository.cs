using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.DataAccess.MSSQL.Repositories
{
    public class DistrictsRepository
    {
        private readonly DeliveryDbContext _dbContext;

        public DistrictsRepository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DistrictEntity>> Get()
        {
            return await _dbContext.Districts
                .AsNoTracking()
                .OrderBy(d => d.Title)
                .ToListAsync();
        }

        public async Task<DistrictEntity?> GetById(int id)
        {
            return await _dbContext.Districts
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DistrictEntity?> GetByTitle(string title)
        {
            return await _dbContext.Districts
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Title == title);
        }

        public async Task Add(string title)
        {
            var district = new DistrictEntity()
            {
                Title = title,
                Packages = new List<PackageEntity>()
            };

            await Console.Out.WriteLineAsync($"district {district.Title} was added");
            await _dbContext.AddAsync(district);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, string title)
        {
            await _dbContext.Districts
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(d => d.Title, title));
            await Console.Out.WriteLineAsync($"District {id} has been updated");
        }

        public async Task Delete(int id)
        {
            await _dbContext.Packages
                .Where(p => p.DistrictId == id)
                .ExecuteDeleteAsync();

            await _dbContext.Districts
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();

            await Console.Out.WriteLineAsync($"District {id} has been deleted");
        }
    }
}
