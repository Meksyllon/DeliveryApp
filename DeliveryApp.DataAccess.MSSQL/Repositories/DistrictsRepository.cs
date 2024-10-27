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

        public async Task Add(DistrictEntity district)
        {
            await Console.Out.WriteLineAsync($"district {district.Title} was added");
            await _dbContext.AddAsync(district);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(string title, List<PackageEntity> packages)
        {
            var district = new DistrictEntity()
            {
                Title = title,
                Packages = packages
            };
            Add(district);            
        }

        public async Task Update(int id, string title, List<PackageEntity> packages)
        {
            await _dbContext.Districts
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(d => d.Title, title)
                    .SetProperty(d => d.Packages, packages));
        }

        public async Task Delete(int id)
        {
            await _dbContext.Districts
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
