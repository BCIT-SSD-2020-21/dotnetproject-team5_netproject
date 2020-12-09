using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Pages.AddItem
{
    

    public class IndexModel : PageModel
    {
        // connect to database
        protected readonly ClothDbContext db;

        public IndexModel(ClothDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(string clothingName, string category, string imageUri, bool isClean = false)
        {
            // Set 2 seconds delay
            System.Threading.Thread.Sleep(2000);

            // get UserId
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // set CategoryId
            int categoryId;
            
            switch (category)
            {     
                case "Top":
                    categoryId = 1;
                    break;
                case "Bottom":
                    categoryId = 2;
                    break;
                default:
                    categoryId = 1;
                    break;
            }

            // Create new clothing object and save it to database
            Clothing clothing = new Clothing(userId, clothingName, isClean, imageUri, categoryId);
            db.Clothes.Add(clothing);
            db.SaveChanges();

            return Redirect("/ItemAddedNotification/Index");
        }
    }
}
