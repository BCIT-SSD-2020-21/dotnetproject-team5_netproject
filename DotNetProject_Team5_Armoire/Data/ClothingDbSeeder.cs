using DotNetProject_Team5_Armoire.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DotNetProject_Team5_Armoire.Data
{
    public class ClothingDbSeeder
    {
        public static async Task SeedAsync(ClothDbContext db)
        {
            if (!await db.Categories.AnyAsync())
            {
                await db.Categories.AddRangeAsync(
                    GetPreconfiguredCategories());

                await db.SaveChangesAsync();
            }
            if (!await db.Clothes.AnyAsync())
            {
                await db.Clothes.AddRangeAsync(
                    GetPreconfiguredItems());

                await db.SaveChangesAsync();
            }

            static IEnumerable<Clothing> GetPreconfiguredItems()
            {
                return new List<Clothing>()
                {   //in sequence of userid, name, isclean, picture, categoryid
                    new Clothing("1","Crop Top",false,"/images/tops/croptop.jpg",1),
                    new Clothing("1","T-shirt",true,"/images/tops/tshirt.jpg",1),
                    new Clothing("2","Shirt",true,"/images/tops/shirt.jpg",1),
                    new Clothing("2","Straight Pants",false,"/images/bottoms/pant.jpg",2),
                    new Clothing("2","Jeans",true,"/images/bottoms/jeans.jpg",2),
                    new Clothing("1","Sweatpants",false,"/images/bottoms/sweatpants.jpg",2),
                };
            }

            static IEnumerable<Category> GetPreconfiguredCategories()
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
