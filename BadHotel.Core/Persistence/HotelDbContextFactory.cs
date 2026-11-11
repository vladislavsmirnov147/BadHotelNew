using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BadHotel.Core.Persistence;

public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
{
    public HotelDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=BadHotelEfDb;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        return new HotelDbContext(options);
    }
}
