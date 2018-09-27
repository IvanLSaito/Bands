using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bands.Models
{
    public class BandContext : DbContext
    {
        public BandContext(DbContextOptions<BandContext> options) : base(options)
        {

        }
        public DbSet<BandsItem> BandItems { get; set; }
    }
}
