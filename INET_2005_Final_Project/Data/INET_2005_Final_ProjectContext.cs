using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INET_2005_Final_Project.Models;

namespace INET_2005_Final_Project.Data
{
    public class INET_2005_Final_ProjectContext : DbContext
    {
        public INET_2005_Final_ProjectContext (DbContextOptions<INET_2005_Final_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<INET_2005_Final_Project.Models.Product> Product { get; set; } = default!;
    }
}
