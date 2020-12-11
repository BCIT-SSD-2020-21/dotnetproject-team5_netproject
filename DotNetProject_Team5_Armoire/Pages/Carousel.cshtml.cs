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
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages
{
    [Authorize]
    public class CarouselModel : PageModel
    {
        private readonly ClothDbContext _db;

        public CarouselModel(ClothDbContext db)
        {
            _db = db;
        }

        public List<Category> Categories = new List<Category>();
        public List<Clothing> Clothes = new List<Clothing>();
        public List<Clothing> Tops = new List<Clothing>();
        public List<Clothing> Bottoms = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();


        public async Task OnGet()
        {
            string userId;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = await _db.Clothes.Where(c => c.OwnerId == userId).ToListAsync();

                Categories = await _db.Categories.ToListAsync();

                // filter
                foreach (var item in Clothes)
                {
                    if (item.CategoryId == 1)
                    {
                        Tops.Add(item);
                    }
                    else
                    {
                        Bottoms.Add(item);
                    }

                    if (!item.IsClean)
                    {
                        isDirty.Add(item);

                    }
                }
            }

            
        }
    }
}
