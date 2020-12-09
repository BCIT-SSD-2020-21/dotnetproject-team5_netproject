using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class DashboardModel : PageModel
    {
        // access database
        protected readonly ClothDbContext db;

        public IQueryable<Clothing> Clothes { get; set; }

        public DashboardModel(ClothDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            // get user clothes
            // find the clothes that have the current users id
            
            // Get User id 
            string userId;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = db.Clothes
                    .Where(c => c.OwnerId == userId);
            }
        }


    }
}
