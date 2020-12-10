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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TestimonialsContext testContext = new TestimonialsContext();
            //Testimonials testimonial = testContext.testimonialContext.Find(id);
            //testimonial.Testimonial = Testimonial;
            //testContext.Entry(testimonial).State = EntityState.Modified;
            //testContext.SaveChanges();
            //return RedirectToAction("Index");


            _db.Entry(Cloth).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClothExists(Cloth.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Dashboard");
        }

        private bool ClothExists(int id)
        {
            return _db.Clothes.Any(e => e.Id == id);
        }
    } 
}

