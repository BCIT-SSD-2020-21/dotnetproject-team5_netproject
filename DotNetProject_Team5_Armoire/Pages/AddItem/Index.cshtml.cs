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
    
    [Authorize]
    public class IndexModel : PageModel
    {
        // connect to database
        protected readonly ClothDbContext db;

        public IndexModel(ClothDbContext db)
        {
            this.db = db;
        }

        public Clothing clothing { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(string clothingName, string category, bool isClean = false)
        {
            // When the post request is made we want to create a new Cloth

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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

            Clothing clothing = new Clothing(userId, clothingName, isClean, "newImages", categoryId);
            db.Clothes.Add(clothing);
            db.SaveChanges();


            // SaveChanges to Database
            return RedirectToPage();
        }
    }
}
