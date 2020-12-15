using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ClothDbContext _db;

        public List<Clothing> ClothingList = new List<Clothing>();
        public List<Clothing> isDirty = new List<Clothing>();
        public string msg = "";
        public string popoverclass = "";

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ClothDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId;
            userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            ClothingList = _db.Clothes.Where(c => c.OwnerId == userId).ToList(); 
            // filter
            foreach (var item in ClothingList)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
