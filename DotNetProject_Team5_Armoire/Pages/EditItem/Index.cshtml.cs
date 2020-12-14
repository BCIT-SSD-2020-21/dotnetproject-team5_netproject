using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages.EditItem
{
    public class IndexModel : PageModel
    {
        private readonly ClothDbContext _db;
        public IQueryable<Clothing> Clothes { get; set; }
        public List<Clothing> ClothingList = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();
        public string msg = "";

        public IndexModel(ClothDbContext db)
        {
            _db = db;
        }

        public Clothing ClothItem { get; set; }

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
            }
            else
            {
                msg = "No new notifications at this time";
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

