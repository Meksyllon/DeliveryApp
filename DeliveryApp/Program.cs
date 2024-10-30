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
            Console.ReadLine();
        }

        private async static void SetDB()
        {
            var dbContext = new DeliveryDbContext();
            var packageRep = new PackageRepository(dbContext);
            var districtRep = new DistrictsRepository(dbContext);
                        

            await Console.Out.WriteLineAsync("start ");
            //await districtRep.Add("Куйбышева");
            await districtRep.Update(6, "Куйбашева");
            await districtRep.Delete(6);
            await Console.Out.WriteLineAsync("All methods are running");
        }
    }
}
