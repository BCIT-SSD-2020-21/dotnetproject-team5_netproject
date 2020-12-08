using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            static Enumerable<Cloth> GetPreconfiguredItems()
            {
                return new List<Cloth>()
                {   //in sequence of userid, name, isclean, picture, categoryid
                    new Cloth(1,"Crop Top",1,"/images/tops/croptop.jpg",1),
                    new Cloth(1,"T-shirt",1,"/images/tops/tshirt.jpg",1),
                    new Cloth(2,"Shirt",1,"/images/tops/shirt.jpg",1),
                    new Cloth(2,"Straight Pants",1,"/images/bottoms/pant.jpg",2),
                    new Cloth(2,"Jeans",1,"/images/bottoms/jeans.jpg",2),
                    new Cloth(1,"Sweatpants",1,"/images/bottoms/sweatpants.jpg",2),
                };
            }
        }
    }
}
