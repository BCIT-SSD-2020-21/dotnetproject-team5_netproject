using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject_Team5_Armoire.Pages
{
    public class DashboardModel : PageModel
    {

        //Access database
        protected readonly ClothDbContext db;
        public IQueryable<Clothing> Clothes { get; set; }
        //public IQueryable<Category> Category { get; set; }
        

        
        
        public DashboardModel(ClothDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            string userId;
            

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = db.Clothes
                    .Where(c => c.OwnerId == userId);
                
            }

            //Category = db.Categories.Where(c => c.Id == 1 || c.Id == 2);
        }


        public void OnPost(int? id)
        {
            if (id > 0)
                Clothes = db.Clothes.Where(c => c.CategoryId == id);
            else
                Clothes = db.Clothes.Where(c => c.CategoryId > 0);
        }
        
    }
}
