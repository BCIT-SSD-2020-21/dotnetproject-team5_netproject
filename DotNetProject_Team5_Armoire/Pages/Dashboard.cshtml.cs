using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using DotNetProject_Team5_Armoire.ViewModels;
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

        public List<Clothing> isDirty = new List<Clothing>();
        public List<Category> Categories = new List<Category>();
        public Clothing Clothing { get; set; }

        public string msg = "";
        public string popoverclass = "";

        // ---- Pagination ----
        const int ITEMS_PER_PAGE = 3;
        public PaginationInfoVM PaginationInfo { get; set; }

        public DashboardModel(ClothDbContext db)
        {
            this.db = db;
        }

        public void OnGet(Clothing clothing, int pageIndex)
        {
            string userId;

            Categories = db.Categories.ToList();

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = db.Clothes
                    .Where(c => c.OwnerId == userId);

                if (clothing.ClothName != null)
                {
                    Clothing = clothing;
                }

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
            }

            // --------- PAGINATION ---------
            int totalItems = Clothes.Count();
            Clothes = Clothes.Skip(pageIndex * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);
            PaginationInfo = new PaginationInfoVM()
            {
                PageIndex = pageIndex,
                ItemsPerPage = Clothes.Count(),
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / ITEMS_PER_PAGE)).ToString()),
            };

            PaginationInfo.Previous = PaginationInfo.PageIndex == 0 ? "is-disabled" : "";
            PaginationInfo.Next = PaginationInfo.PageIndex == PaginationInfo.TotalPages - 1 ? "is-disabled" : "";
        }

        public void OnPost(int? id)
        {
            string userId;
            userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id > 0)
                Clothes = db.Clothes.Where(c => c.CategoryId == id && c.OwnerId == userId);
            else
                Clothes = db.Clothes.Where(c => c.CategoryId > 0 && c.OwnerId == userId);
        }

    }
}