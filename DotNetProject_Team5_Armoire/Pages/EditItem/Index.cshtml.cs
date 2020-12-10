using System;
using System.Collections.Generic;
using System.Linq;
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

        public IndexModel(ClothDbContext db)
        {
            _db = db;
        }

        public Clothing ClothItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //ClothItem = await _db.Clothes
            //    //.Include(c => c.Category)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.Id == id);
            ClothItem = await _db.Clothes.FindAsync(id);

            if (ClothItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ClothToUpdate = await _db.Clothes.FindAsync(id);

            if (ClothToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Clothing>(
                ClothToUpdate,
                "Clothes",
                s => s.ClothName, s => s.PictureUri, s => s.IsClean, s => s.CategoryId))
            {
                await _db.SaveChangesAsync();
                return RedirectToPage("../Dashboard");
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _db.Attach(ClothItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClothingItemExists(ClothItem.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("../Dashboard");
        //}

//        private bool ClothingItemExists(int id)
//        {
//            return _db.Clothes.Any(e => e.Id == id);
//        }
  } 
}

