using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using a00893112s3clothesimages_S3_bucket.Services;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages.EditItem
{
    public class IndexModel : PageModel
    {
        private readonly ClothDbContext _db;
        private readonly IAWSS3Service _awsS3Service;
        public IQueryable<Clothing> Clothes { get; set; }
        public List<Clothing> ClothingList = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();
        public string msg = "";
        public string popoverclass = "";

        [BindProperty]
        public IFormFile Upload { get; set; }

        public IndexModel(ClothDbContext db, IAWSS3Service awsS3Service)
        {
            _db = db;
            _awsS3Service = awsS3Service;
        }

        public Clothing ClothItem { get; set; } = new Clothing();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string userId;
            userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (id == null)
            {
                return NotFound();
            }

            ClothItem = await _db.Clothes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ClothItem == null)
            {
                return NotFound();
            }

            ClothingList = await _db.Clothes.Where(c => c.OwnerId == userId).ToListAsync();
            // filter
            foreach (var item in ClothingList)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var ClothToUpdate = await _db.Clothes.FindAsync(id);

            if (ClothToUpdate == null)
            {
                return NotFound();
            }

            if (Upload != null)
            {
                try
                {
                    ClothToUpdate.PictureUri = await _awsS3Service.UploadFile(Upload);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("S3 Upload", e.Message);
                }
            }

            if (await TryUpdateModelAsync<Clothing>(
                ClothToUpdate,
                "ClothItem",
                s => s.ClothName, s => s.PictureUri, s => s.IsClean, s => s.CategoryId))
            {
                await _db.SaveChangesAsync();
                return RedirectToPage("../Dashboard");
            }

            return Page();
        }

       
  } 
}

