using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UD5_02_API_RESTFull_.NET.Models
{
    public class CombateContext : DbContext 
    {
        public CombateContext(DbContextOptions<CombateContext> options)
            : base(options)
        {
        }

        public DbSet<Combate> Combate { get; set; }
       
    }
}
