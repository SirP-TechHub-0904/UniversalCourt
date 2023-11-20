using UniversalCourt.Application.Dtos.AwsDtos;
using UniversalCourt.Application.Services.AWS;
using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static UniversalCourt.Domain.Enum.Enum;

namespace UniversalCourt.Website.Areas.Staff.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateProfileModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateProfileModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]
        public Profile UserDatas { get; set; }

        [BindProperty]
        public IFormFile? imagefile { get; set; }

        [BindProperty]
        public AdditionalProfile AdditionalProfileData { get; set; }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }


        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }


        [BindProperty]
        public Profile Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            [Required]
            public string? FirstName { get; set; }

            [Display(Name = "Title")]
            [Required]
            public string? Title { get; set; }

            [Display(Name = "Middle Name")]
            [Required]
            public string? MiddleName { get; set; }

            [Display(Name = "Last Name")]
            [Required]
            public string? LastName { get; set; }

            [Required]
            public string PhoneNumber { get; set; }

            [Required]
            public string AltPhoneNumber { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string OfficeEmail { get; set; }
            [Required]
            public string PermanentHomeAddress { get; set; }
            [Required]
            public string PermanentLga { get; set; }
            [Required]
            public string PermanentState { get; set; }
            [Required]
            public string Address { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string State { get; set; }
            [Required]
            public string Country { get; set; }
            [Required]
            public DateTime? DateOfBirth { get; set; }
            [Required]
            public GenderStatus Gender { get; set; }
            [Required]
            public string MaritalStatus { get; set; }
            [Required]
            public string Biography { get; set; }
        }

        [BindProperty]
        public string redirectttt { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserDatas = await _userManager.FindByIdAsync(user.Id);
            Input = UserDatas;
            if (UserDatas == null)
            {
                return NotFound();
            }
            AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            ProfileCategories = await _context.ProfileCategories
                .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userinfo = await _userManager.FindByIdAsync(user.Id);

                var checkemail = await _userManager.FindByEmailAsync(Input.Email);

                //image
                if (imagefile != null)
                {
                    try
                    {
                        // Process file
                        await using var memoryStream = new MemoryStream();
                        await imagefile.CopyToAsync(memoryStream);

                        var fileExt = Path.GetExtension(imagefile.FileName);
                        var docName = $"{Guid.NewGuid()}{fileExt}";
                        // call server

                        var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                        {
                            BucketName = "juray2023",
                            InputStream = memoryStream,
                            Name = docName
                        };

                        var cred = new AwsCredentials()
                        {
                            AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                            SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                        };

                        var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, userinfo.PassportFilePathKey);
                        // 
                        if (xresult.Message.Contains("200"))
                        {
                            userinfo.PassportFilePathUrl = xresult.Url;
                            userinfo.PassportFilePathKey = xresult.Key;
                        }
                        else
                        {
                            TempData["error"] = "unable to upload image";
                            //return Page();
                        }
                    }
                    catch (Exception c)
                    {

                    }
                }



                userinfo.Title = Input.Title;
                userinfo.FirstName = Input.FirstName;
                userinfo.LastName = Input.LastName;
                userinfo.MiddleName = Input.MiddleName;
                userinfo.PhoneNumber = Input.PhoneNumber;
                userinfo.AltPhoneNumber = Input.AltPhoneNumber;
                if (checkemail == null)
                {
                    var emx = await _userManager.GenerateChangeEmailTokenAsync(user, Input.Email);
                    var upd = await _userManager.ChangeEmailAsync(user, Input.Email, emx);
                    if (!upd.Succeeded)
                    {
                        TempData["error"] = "Unable to change email";
                    }
                    else
                    {
                        await _userManager.UpdateNormalizedEmailAsync(user);

                        var ccemx = await _userManager.SetUserNameAsync(user, Input.Email);
                        if (ccemx.Succeeded)
                        {
                            await _userManager.UpdateNormalizedUserNameAsync(user);
                        }

                        TempData["success"] = "Email Changed Successfully";
                    }
                }

                userinfo.OfficeEmail = Input.OfficeEmail;
                userinfo.PermanentHomeAddress = Input.PermanentHomeAddress;
                userinfo.PermanentLga = Input.PermanentLga;
                userinfo.PermanentState = Input.PermanentState;
                userinfo.Address = Input.Address;
                userinfo.City = Input.City;
                userinfo.State = Input.State;
                userinfo.Country = Input.Country;
                if(Input.DateOfBirth != null) { 
                userinfo.DateOfBirth = Input.DateOfBirth;
                }
                userinfo.Gender = Input.Gender;
                userinfo.MaritalStatus = Input.MaritalStatus;
                userinfo.Biography = Input.Biography;

                await _userManager.UpdateAsync(userinfo);
                TempData["success"] = "Successfull";
                return RedirectToPage("./Profile");
            }
            catch (Exception c)
            {
                var user = await _userManager.GetUserAsync(User);
                UserDatas = await _userManager.FindByIdAsync(user.Id);

                if (UserDatas == null)
                {
                    return NotFound();
                }
                AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
                ProfileCategories = await _context.ProfileCategories
                    .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAdditionalAsync()
        {
            _context.AdditionalProfiles.Add(AdditionalProfileData);
            await _context.SaveChangesAsync();
            TempData["success"] = "Successfull";
            return RedirectToPage("./UpdateProfile");
        }

        public async Task<IActionResult> OnPostCategoriesAsync()
        {
            _context.ProfileCategoryLists.Add(ProfileCategoryList);
            await _context.SaveChangesAsync();
            TempData["success"] = "Successfull";
            return RedirectToPage("./UpdateProfile" + redirectttt);
        }


    }
}
