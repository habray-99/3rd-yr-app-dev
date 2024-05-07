﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public IndexModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Username")]
            public string Username { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [MaxLength(30)]
            [Display(Name = "Address")]
            public string Address { get; set; }
            //[BindProperty]
            //public IFormFile ProfilePicture { get; set; }
            [BindProperty]
            public IFormFile ProfilePictureUpload { get; set; }

        }

        private async Task LoadAsync(CustomUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var address = user.Address;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Username = userName,
                Address = user.Address
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
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
            //var userName = await _userManager.GetUserNameAsync(user);
            var userName = await _userManager.GetUserIdAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.Username != userName)
            {
                var setUsernameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUsernameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set username.";
                    return RedirectToPage();
                }
            }
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
                //await _userManager.UpdateAsync(user);
            }
            //if (Input.ProfilePicture != null && Input.ProfilePicture.Length > 0)
            //{
            //    // Check the file size (in bytes)
            //    if (Input.ProfilePicture.Length > 3 * 1024 * 1024) // 3 MB
            //    {
            //        ModelState.AddModelError("ProfilePicture", "The profile picture must be 3 MB or smaller.");
            //        return Page();
            //    }

            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await Input.ProfilePicture.CopyToAsync(memoryStream);
            //        user.ProfilePicture = memoryStream.ToArray();
            //    }
            //}
            //// If no new picture is uploaded, keep the existing picture
            //else
            //{
            //    user.ProfilePicture = user.ProfilePicture;
            //}
            if (Input.ProfilePictureUpload != null && Input.ProfilePictureUpload.Length > 0)
            {
                Debug.Write("Size of the image uploaded is: "+Input.ProfilePictureUpload.Length);
                // Check the file size (in bytes)
                if (Input.ProfilePictureUpload.Length > 3 * 1024 * 1024) // 3 MB
                {
                    ModelState.AddModelError("ProfilePicture", "The profile picture must be 3 MB or smaller.");
                    return Page();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await Input.ProfilePictureUpload.CopyToAsync(memoryStream);
                    user.ProfilePicture = memoryStream.ToArray();
                }
            }
            // If no new picture is uploaded, keep the existing picture
            else
            {
                user.ProfilePicture = user.ProfilePicture;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update user profile.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
