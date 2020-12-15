using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using a00893112s3clothesimages_S3_bucket.Services;
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
        private readonly IAWSS3Service _awsS3Service;

        public Clothing Clothing { get; set; }
        public IQueryable<Clothing> Clothes { get; set; }
        public List<Clothing> isDirty = new List<Clothing>();


        public string msg = "";
        public string popoverclass = "";

        [BindProperty]
        public IFormFile Upload { get; set; }

        private IHostingEnvironment _environment;

        public IndexModel(ClothDbContext db, IHostingEnvironment environment, IAWSS3Service awsS3Service)
        {
            this.db = db;
            _environment = environment;
            _awsS3Service = awsS3Service;
        }

        public void OnGet(Clothing clothing)
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
                    popoverclass = "fas fa-bell text-danger";
                }
                else
                {
                    msg = "No new notifications at this time";
                    popoverclass = "fas fa-bell";
                }
            }
        }

        public async Task<IActionResult> OnPostUploadAsync(string clothingName, string category, bool isClean = false)
        {
            if (ModelState.IsValid)
            {

                string imageUri = null;
                if (Upload != null)
                {
                    try
                    {
                        imageUri = await _awsS3Service.UploadFile(Upload);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("S3 Upload", e.Message);
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
        }

    }
}
