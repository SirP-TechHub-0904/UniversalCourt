﻿using UniversalCourt.Application.Dtos.UsersDto;
using UniversalCourt.Application.Repository.Base;
using UniversalCourt.Application.Repository.GeneralRepository.Projects;
using UniversalCourt.Application.Repository.GeneralRepository.Users;
using UniversalCourt.Application.Services.AWS;
using UniversalCourt.Domain.Models;
using UniversalCourt.Website.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace UniversalCourt.Website.Areas.MIS.Pages.IProject
{
    public class DeletedModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly IRazorRenderService _renderService;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly UserManager<Profile> _userManager;
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        public DeletedModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context,
             ILogger<IndexModel> logger, UserManager<Profile>
            userManager,
            IUserRepositoryAsync userRepositoryAsync, IRazorRenderService renderService)
        {
            _logger = logger;
            _userManager = userManager;
            _userRepositoryAsync = userRepositoryAsync;
            _renderService = renderService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var setting = await _context.SuperSettings.FirstOrDefaultAsync();
            if (setting == null)
            {
                return NotFound();
            }
            if (setting.ActivateProject == false)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            var data = await _userRepositoryAsync.GetDashboardAllDeletedUsersAsync();

            return new PartialViewResult
            {
                ViewName = "_ViewDelete",
                ViewData = new ViewDataDictionary<IEnumerable<ListUsersDto>>(ViewData, data)
            };
        }

        public async Task<JsonResult> OnGetDetailsAsync(string? id)
        {
            if (id == null)
            {
                var datalist = await _userRepositoryAsync.GetDashboardAllUsersAsync();
                var html = await _renderService.ToStringAsync("_ViewDelete", datalist);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var thisdata = await _userRepositoryAsync.GetUserDetailsByIdAsync(id);

                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_Details", thisdata) });
            }
        }

        public async Task<JsonResult> OnPostRestoreAsync(string id)
        {
            var getuser = await _userManager.FindByIdAsync(id);
            if (getuser != null)
            {
                getuser.UserStatus = Domain.Enum.Enum.UserStatus.Active;
                await _userManager.UpdateAsync(getuser);
            }

            var datalist = await _userRepositoryAsync.GetDashboardAllDeletedUsersAsync();
            var html = await _renderService.ToStringAsync("_ViewDelete", datalist);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
