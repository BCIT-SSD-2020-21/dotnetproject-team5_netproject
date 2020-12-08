using DotNetProject_Team5_Armoire.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Data
{
    public class CategoryDbSeeder
    {
        public static async Task SeedAsync(ClothDbContext clothcontext)
        {
            if(!await clothcontext.Categories.AnyAsync())
            {
                await clothcontext.Categories.AddRangeAsync(
                    GetPreconfiguredItems());

                await clothcontext.SaveChangesAsync();
            }

            static Enumerable<Category> GetPreconfiguredItems()
            {
                return new List<Category>()
                {
                    new Category("Tops"),
                    new Category("Bottoms")
                };
            }
        }
    }
}
