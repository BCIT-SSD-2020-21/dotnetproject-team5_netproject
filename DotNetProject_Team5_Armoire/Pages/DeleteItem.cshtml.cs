using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class DeleteItemModel : PageModel
    {
        private readonly DotNetProject_Team5_Armoire.Data.ClothDbContext _db;

        public DeleteItemModel(DotNetProject_Team5_Armoire.Data.ClothDbContext db)
        {
            _db = db;
        }

        public Clothing Cloth { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cloth = await _db.Clothes.FirstOrDefaultAsync(c => c.Id == id);

            if (Cloth == null)
            {
                return NotFound();
            }
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
                _db.Clothes.Remove(Cloth);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Dashboard");
        }
    }
}

