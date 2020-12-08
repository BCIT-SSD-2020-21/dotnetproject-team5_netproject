//using DotNetProject_Team5_Armoire.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Linq;

//namespace DotNetProject_Team5_Armoire.Data
//{
//    public class ClothDbSeeder
//    {
//        public static async Task SeedAsync(ClothDbContext clothcontext)
//        {
//            if (!await clothcontext.Clothes.AnyAsync())
//            {
//                await clothcontext.Clothes.AddRangeAsync(
//                    GetPreconfiguredItems());

//                await clothcontext.SaveChangesAsync();
//            }

//            static IEnumerable<Cloth> GetPreconfiguredItems()
//            {
//                return new List<Cloth>()
//                {   //in sequence of userid, name, isclean, picture, categoryid
//                    new Cloth(1,"Crop Top",false,"/images/tops/croptop.jpg",1),
//                    new Cloth(1,"T-shirt",true,"/images/tops/tshirt.jpg",1),
//                    new Cloth(2,"Shirt",true,"/images/tops/shirt.jpg",1),
//                    new Cloth(2,"Straight Pants",false,"/images/bottoms/pant.jpg",2),
//                    new Cloth(2,"Jeans",true,"/images/bottoms/jeans.jpg",2),
//                    new Cloth(1,"Sweatpants",false,"/images/bottoms/sweatpants.jpg",2),
//                };
//            }
//        }
//    }
//}
