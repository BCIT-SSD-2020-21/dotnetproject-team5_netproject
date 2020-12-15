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
using Microsoft.Extensions.Logging;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ClothDbContext _db;
        public string msg = "";

        public IndexModel(ClothDbContext db)
        {
            _db = db ;
        }

        public List<Category> Categories = new List<Category>();
        public List<Clothing> Clothes = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();
        public async Task OnGet()
        {
            string userId;
            Categories = await _db.Categories.ToListAsync();
            Clothes = await _db.Clothes.ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = await _db.Clothes.Where(c => c.OwnerId == userId).ToListAsync();

                Categories = await _db.Categories.ToListAsync();

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
    }
}
