using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gar3._0.Models;

namespace gar3._0.Data
{
    public class gar3_0Context : DbContext
    {
        public gar3_0Context (DbContextOptions<gar3_0Context> options)
            : base(options)
        {
        }

        public DbSet<gar3._0.Models.ParkedVehicle> ParkedVehicle { get; set; } = default!;
    }
}
