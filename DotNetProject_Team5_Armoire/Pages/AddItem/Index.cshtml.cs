using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Pages.AddItem
{


    public class IndexModel : PageModel
    {
        // connect to database
        protected readonly ClothDbContext db;

        public Clothing Clothing { get; set; }
        public IQueryable<Clothing> Clothes { get; set; }
        public List<Clothing> isDirty = new List<Clothing>();

        public string msg = "";
        [BindProperty]
        public IFormFile Upload { get; set; }

        private IHostingEnvironment _environment;

        public IndexModel(ClothDbContext db, IHostingEnvironment environment)
        {
            this.db = db;
            _environment = environment;
        }

        public void OnGet()
        {
            string userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = db.Clothes
                    .Where(c => c.OwnerId == userId);
                // filter
                foreach (var item in Clothes)
                {
                    if (!item.IsClean)
                    {
                        isDirty.Add(item);

                    }
                }
                if (isDirty.Count > 3)
                {
                    msg = $"You have {isDirty.Count} items in your dirty pile. Time to do laundry!";
                }
                else
                {
                    msg = "No new notifications at this time";
                }
            }
        }

        //public async Task<IActionResult> OnPostUploadAsync(string clothingName, string category, bool isClean = false)
        //{
        //    if(ModelState.IsValid)
        //    {

        //        string imageUri = null;
        //        if(Upload != null)
        //        {
        //            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images", Upload.FileName);
        //            imageUri = Path.Combine("/images", Upload.FileName);
        //            using (var fileStream = new FileStream(file, FileMode.Create))
        //            {
        //                await Upload.CopyToAsync(fileStream);
        //            }
        //        }

        //        //get UserId
        //        string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //        var categoryId = category switch
        //        {
        //            "Top" => 1,
        //            "Bottom" => 2,
        //            _ => 1,
        //        };

        //        // Create new clothing object and save it to database
        //        Clothing = new Clothing(userId, clothingName, isClean, imageUri, categoryId);
        //        db.Clothes.Add(Clothing);
        //        db.SaveChanges();

        //        return RedirectToPage("/ItemAddedNotification/Index");
        //    }

        //    return Page();
        //}

    }
}
