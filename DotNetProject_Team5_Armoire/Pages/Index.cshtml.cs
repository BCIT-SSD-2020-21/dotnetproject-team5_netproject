using System;
using System.Collections.Generic;
using System.Linq;
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

        public IndexModel(ClothDbContext db)
        {
            _db = db ;
        }

        public List<Category> Categories = new List<Category>();
        //public List<Clothing> Clothes = new List<Clothing>();
        public async Task OnGet()
        {
            Categories = await _db.Categories.ToListAsync();
            //Clothes = await _db.Clothes.ToListAsync();
        }
    }
}
