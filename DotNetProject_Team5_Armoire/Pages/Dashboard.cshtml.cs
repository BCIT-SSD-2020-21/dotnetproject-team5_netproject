using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class DashboardModel : PageModel
    {

        //Access database
        protected readonly ClothDbContext db;
        public IQueryable<Clothing> Clothes { get; set; }
        //public IQueryable<Category> Category { get; set; }

        [BindProperty]
        public string Message { get; set; }
        public Clothing Clothing { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        // public IQueryable<Category> Categories { get; set; }
        public List<Clothing> isDirty = new List<Clothing>();

        public string msg = "";

        private IHostingEnvironment _environment;

        public DashboardModel(ClothDbContext db, IHostingEnvironment environment)
        public DashboardModel(ClothDbContext db)
        {
            this.db = db;
            this._environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(Clothing clothing)
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

            if(clothing.ClothName != null)
            {
                Clothing = clothing;
            }

            return Page();

            //Category = db.Categories.Where(c => c.Id == 1 || c.Id == 2);
        }
        
        public void OnPost(int? id)
        {
            string userId;
            userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (id > 0)
                Clothes = db.Clothes.Where(c => c.CategoryId == id && c.OwnerId == userId );
            else
                Clothes = db.Clothes.Where(c => c.CategoryId > 0);

        }

        public async Task<IActionResult> OnPostUploadAsync(string clothingName, string category, bool isClean = false)
        {
            if (ModelState.IsValid)
            {

                string imageUri = null;
                if (Upload != null)
                {
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images", Upload.FileName);
                    imageUri = Path.Combine("/images", Upload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                }

                //get UserId
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var categoryId = category switch
                {
                    "Top" => 1,
                    "Bottom" => 2,
                    _ => 1,
                };

                // Create new clothing object and save it to database
                Clothing = new Clothing(userId, clothingName, isClean, imageUri, categoryId);
                db.Clothes.Add(Clothing);
                db.SaveChanges();

                return RedirectToPage("/Dashboard", Clothing);
            }

            return Page();
                Clothes = db.Clothes.Where(c => c.CategoryId > 0 && c.OwnerId == userId);
        }

    }
}
