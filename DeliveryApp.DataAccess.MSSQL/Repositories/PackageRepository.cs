using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.DataAccess.MSSQL.Repositories
{
    public class PackageRepository
    {
        private readonly DeliveryDbContext _dbContext;

        public PackageRepository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PackageEntity>> Get()
        {
            return await _dbContext.Packages
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<PackageEntity?> GetById(int id)
        {
            return await _dbContext.Packages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PackageEntity>> GetFromDistrict(int districtId)
        {
            var district = await _dbContext.Districts.FirstOrDefaultAsync(d => d.Id == districtId)
                ?? throw new Exception();

            return await _dbContext.Packages
                .AsNoTracking()
                .Where(p => p.District.Id == districtId)
                .OrderBy(p => p.OrderTime)
                .ToListAsync();
        }

        public async Task<List<PackageEntity>> GetOrderedByOrderTime()
        {
            return await _dbContext.Packages
                .AsNoTracking()
                .OrderBy(p => p.OrderTime)
                .ToListAsync();
        }

        public async Task<List<PackageEntity>> GetOrderedByDescendingOrderTime()
        {
            return await _dbContext.Packages
                .AsNoTracking()
                .OrderByDescending(p => p.OrderTime)
                .ToListAsync();
        }

        public async Task Add(float weight, DateTime ordertime, int districtId)
        {
            var district = await _dbContext.Districts.FirstOrDefaultAsync(d => d.Id == districtId)
                ?? throw new ArgumentException("Provided district is not exists");

            var package = new PackageEntity()
            {
                Weight = weight,
                OrderTime = ordertime,
                DistrictId = districtId,
                District = district                
            };

            package.District.Packages ??= new List<PackageEntity>();
            package.District.Packages.Add(package);

            await Console.Out.WriteLineAsync($"package {package.OrderTime} was added");

            await _dbContext.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, float weight, DateTime ordertime, int districtId)
        {
            var district = await _dbContext.Districts.FirstOrDefaultAsync(d => d.Id == districtId)
                ?? throw new Exception();

            await _dbContext.Packages
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Weight, weight)
                    .SetProperty(p => p.OrderTime, ordertime)
                    .SetProperty(p => p.DistrictId, districtId)
                    .SetProperty(p => p.District, district));
            await Console.Out.WriteLineAsync($"Package {id} has been updated");
        }

        public async Task Delete(int id)
        {
            var package = await GetById(id);

            _dbContext.Districts
                .FirstOrDefault(d => d.Id == package.DistrictId)
                .Packages
                .Remove(package);

            await _dbContext.Packages
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
            await Console.Out.WriteLineAsync($"Package {id} has been deleted");
        }
    }
}
