using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using a00893112s3clothesimages_S3_bucket.Services;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class DeleteItemModel : PageModel
    {
        private readonly ClothDbContext _db;
        private readonly IAWSS3Service _awsS3Service;

        public List<Clothing> Clothes = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();
        public string msg = "";
        public string popoverclass = "";

        public DeleteItemModel(ClothDbContext db, IAWSS3Service awsS3Service)
        {
            _db = db;
            _awsS3Service = awsS3Service;
        }

        public Clothing Cloth { get; set; }
        public Category Category { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            Cloth = await _db.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            Category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == Cloth.CategoryId);

            if (Cloth == null)
            {
                return NotFound();
            }
            
            // --------------- NOTIFICATION ---------------
            string userId;
            userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Clothes = await _db.Clothes.Where(c => c.OwnerId == userId).ToListAsync();
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
            // ---------------------------------------------
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cloth = await _db.Clothes.FindAsync(id);

            if (Cloth != null)
            {
                // Remove image from AWS
                try
                {
                    _awsS3Service.DeleteFile(Cloth.PictureUri);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("S3 Delete", e.Message);
                }
                _db.Clothes.Remove(Cloth);
                await _db.SaveChangesAsync();


            }

            return RedirectToPage("./Dashboard");
        }
    }
}

