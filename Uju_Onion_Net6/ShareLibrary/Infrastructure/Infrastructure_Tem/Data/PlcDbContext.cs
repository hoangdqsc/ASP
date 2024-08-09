using Core_Temp.Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Temp.Data
{

    public class plcDbContext : DbContext
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<MachineData> MachineData { get; set; }
        }

    }
}
