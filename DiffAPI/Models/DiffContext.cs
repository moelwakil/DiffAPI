using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Models
{
    public class DiffContext : DbContext
    {
        public DiffContext(DbContextOptions<DiffContext> options)
            : base(options)
        {
        }
        public DbSet<DiffObject> DiffObjects { get; set; } = null!;
    }
}
