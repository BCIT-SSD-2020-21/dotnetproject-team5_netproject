using DotNetProject_Team5_Armoire.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Data
{
    public class ClothDbContext : DbContext
    { 

        public ClothDbContext(DbContextOptions<ClothDbContext> options) : base(options)
        {

        }

        public DbSet<Cloth> Clothes { get; set; }

        public DbSet<Category> Categories { get; set; } 
    }
}
