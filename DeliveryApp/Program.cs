using DeliveryApp.DataAccess.MSSQL;
using DeliveryApp.DataAccess.MSSQL.Repositories;
using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetDB();            
        }

        private async static void SetDB()
        {
            var dbContext = new DeliveryDbContext();
            var packageRep = new PackageRepository(dbContext);
            var districtRep = new DistrictsRepository(dbContext);

            packageRep.Add(1.5f, new DateTime(2024, 10, 26, 16, 25, 36), 2);
            packageRep.Add(2.5f, new DateTime(2024, 10, 26, 17, 33, 22), 2);
            packageRep.Add(1f, new DateTime(2024, 10, 26, 16, 51, 16), 1);
            packageRep.Add(3f, new DateTime(2024, 10, 26, 15, 22, 26), 3);
            packageRep.Add(2f, new DateTime(2024, 10, 27, 16, 6, 8), 1);
            packageRep.Add(0.5f, new DateTime(2024, 10, 27, 15, 37, 55), 4);
            packageRep.Add(3.5f, new DateTime(2024, 10, 27, 15, 52, 13), 1);
            packageRep.Add(2.5f, new DateTime(2024, 10, 27, 18, 12, 36), 5);

            Console.ReadLine();
            dbContext.SaveChanges();
        }
    }
}
