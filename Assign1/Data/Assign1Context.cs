using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assign1.Models;

namespace Assign1.Data
{
    public class Assign1Context : DbContext
    {
        public Assign1Context (DbContextOptions<Assign1Context> options)
            : base(options)
        {
        }

        public DbSet<Assign1.Models.Product> Product { get; set; } = default!;
    }
}
