using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Pages.AddItem
{
    public class IndexModel : PageModel
    {
        // connect to database
        protected readonly ClothDbContext db;

        public IndexModel(ClothDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            // When the post request is made we want to create a new Cloth



            // SaveChanges to Database
            return RedirectToPage();
        }
    }
}
