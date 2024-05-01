// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Drawing;
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
            Input = new InputModel();
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
            [BindProperty]
            public IFormFile ProfilePicture { get; set; }
            [Display(Name = "Profile Picture")]
            public string ProfilePictureSize { get; set; }
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
            //if (!string.IsNullOrEmpty(user.ProfilePictureFilePath))
            //{
            //    var fileInfo = new FileInfo(user.ProfilePictureFilePath);
            //    Input.ProfilePictureSize = $"Image size: {fileInfo.Length / 1024} KB";
            //}
            //else
            //{
            //    Input.ProfilePictureSize = string.Empty;
            //}
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
            var userName = await _userManager.GetUserNameAsync(user);
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
            }

            //if (Input.ProfilePicture != null && Input.ProfilePicture is { Length: > 0 })
            //{
            //    // Check file size
            //    if (Input.ProfilePicture.Length > 3 * 1024 * 1024)
            //    {
            //        ModelState.AddModelError(string.Empty, "Image size cannot exceed 3MB.");
            //        return Page();
            //    }

            //    // resize image
            //    // Resize the image to a maximum size of 3MB
            //    using var originalImage = Image.FromStream(Input.ProfilePicture.OpenReadStream());
            //    int maxWidth = 2048;
            //    int maxHeight = 2048;

            //    int width = originalImage.Width;
            //    int height = originalImage.Height;

            //    if (width > maxWidth || height > maxHeight)
            //    {
            //        double ratioX = (double)maxWidth / width;
            //        double ratioY = (double)maxHeight / height;
            //        double ratio = Math.Min(ratioX, ratioY);

            //        width = (int)(width * ratio);
            //        height = (int)(height * ratio);
            //    }

            //    using var resizedImage = new Bitmap(width, height);
            //    using var graphics = Graphics.FromImage(resizedImage);
            //    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //    graphics.DrawImage(originalImage, 0, 0, width, height);

            //    // Save the resized image to a memory stream
            //    using var memoryStream = new MemoryStream();
            //    resizedImage.Save(memoryStream, originalImage.RawFormat);
            //    var resizedImageFile = new FormFile(memoryStream, 0, memoryStream.Length, Input.ProfilePicture.Name, Input.ProfilePicture.FileName);

            //    var fileName = $"{user.Id}_{Guid.NewGuid()}{Path.GetExtension(Input.ProfilePicture.FileName)}";
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            //    await using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await resizedImageFile.CopyToAsync(stream);
            //    }
            //    user.ProfilePictureFilePath = filePath;
            //    user.ProfilePicture = fileName;
            //}

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
