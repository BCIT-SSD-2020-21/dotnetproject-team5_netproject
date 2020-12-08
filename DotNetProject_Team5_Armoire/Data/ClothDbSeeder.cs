using DotNetProject_Team5_Armoire.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DotNetProject_Team5_Armoire.Data
{
    public class ClothDbSeeder
    {
        public static async Task SeedAsync(ClothDbContext clothcontext)
        {
            if (!await clothcontext.Clothes.AnyAsync())
            {
                await clothcontext.Clothes.AddRangeAsync(
                    GetPreconfiguredItems());

                await clothcontext.SaveChangesAsync();
            }

            static IEnumerable<Clothing> GetPreconfiguredItems()
            {
                return new List<Clothing>()
                {   //in sequence of userid, name, isclean, picture, categoryid
                    new Clothing("user1","Crop Top",false,"/images/tops/croptop.jpg",1),
                    new Clothing("user2","T-shirt",true,"/images/tops/tshirt.jpg",1),
                    new Clothing("user3","Shirt",true,"/images/tops/shirt.jpg",1),
                    new Clothing("user2","Straight Pants",false,"/images/bottoms/pant.jpg",2),
                    new Clothing("user2","Jeans",true,"/images/bottoms/jeans.jpg",2),
                    new Clothing("user2","Sweatpants",false,"/images/bottoms/sweatpants.jpg",2),
                };
            }
        }
    }
}
