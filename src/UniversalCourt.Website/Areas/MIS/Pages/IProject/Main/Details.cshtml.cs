﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using UniversalCourt.Website.Middlewares;
using UniversalCourt.Application.Repository.Base;
using UniversalCourt.Application.Dtos.Project;
using UniversalCourt.Application.Repository.GeneralRepository.Projects;
using Microsoft.CodeAnalysis;
using static UniversalCourt.Domain.Enum.Enum;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Identity;
using UniversalCourt.Application.Repository.GeneralRepository.Users;
using UniversalCourt.Application.Repository.GeneralRepository.ProjectsCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversalCourt.Website.Areas.MIS.Pages.Main
{
    public class DetailsModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IRazorRenderService _renderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepositoryAsync _project;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly UserManager<Profile> _userManager;
        private readonly IProjectCategoryRepositoryAsync _projectCategory;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, IRazorRenderService renderService, IUnitOfWork unitOfWork, IProjectRepositoryAsync project, UserManager<Profile> userManager, IUserRepositoryAsync userRepositoryAsync, IProjectCategoryRepositoryAsync projectCategory, ILogger<DetailsModel> logger)
        {
            _context = context;
            _renderService = renderService;
            _unitOfWork = unitOfWork;
            _project = project;
            _userManager = userManager;
            _userRepositoryAsync = userRepositoryAsync;
            _projectCategory = projectCategory;
            _logger = logger;
        }

        public IEnumerable<ProjectMain> Project { get; set; }
        public IEnumerable<ProjectFeatureListDto> ProjectFeatureListDto { get; set; }
        public ProjectDto Data { get;set;}
        public async Task<IActionResult> OnGetAsync(long id)
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
            Data = await _project.GetProjectIdAsync(id);
            ProjectFeatureListDto = await _project.GetAllProjectFeatureSpecificListAsync(id);
            return Page();
        }
        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            var data = await _project.GetAllProjectByQuery();

            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<ProjectListDto>>(ViewData, data)
            };
        }
        public async Task<JsonResult> OnGetCreateAsync()
        {
            var referralOptions = (await _userRepositoryAsync.GetDashboardAllUsersAsync())
    .Select(u => new System.Web.Mvc.SelectListItem
    {
        Value = u.Id.ToString(),
        Text = u.Username
    })
    .ToList();

            var categoryOptions = (await _projectCategory.GetAllAsync())
                .Select(c => new System.Web.Mvc.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                })
                .ToList();
            // Create a new instance of ProjectCreateDto and set the dropdown options
            var model = new ProjectCreateDto
            {
                ReferralOptions = referralOptions,
                CategoryOptions = categoryOptions
            };

            return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_Create", model) });

        }
        public async Task<JsonResult> OnPostCreateAsync(long id, ProjectCreateDto data, IFormFile? file)
        {
             ModelState.Remove("ReferralOptions");
            ModelState.Remove("CategoryOptions");
            if (ModelState.IsValid)
            {

                var create = new ProjectMain
                {
                    Title = data.Title,
                    Description = data.Description,
                    ProfileReferralId = data.ProfileReferralId,
                    ProjectCategoryId = data.ProjectCategoryId,
                    Urls = data.Urls,
                    DateStarted = data.DateStarted,
                    DateFinised = data.DateFinised

                };
                await _project.AddProjectAsync(create, file);



                var datalist = await _project.GetAllProjectByQuery();
                var html = await _renderService.ToStringAsync("_ViewAll", datalist);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var referralOptions = (await _userRepositoryAsync.GetDashboardAllUsersAsync())
    .Select(u => new System.Web.Mvc.SelectListItem
    {
        Value = u.Id.ToString(),
        Text = u.Username
    })
    .ToList();

            var categoryOptions = (await _projectCategory.GetAllAsync())
                .Select(c => new System.Web.Mvc.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                })
                .ToList();

                data.ReferralOptions = referralOptions;
                data.CategoryOptions = categoryOptions;
                var html = await _renderService.ToStringAsync("_Create", data);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        //edit
        public async Task<JsonResult> OnGetEditAsync(long id = 0)
        {
            if (id == 0)
            {
                var datalist = await _project.GetAllProjectByQuery();
                var html = await _renderService.ToStringAsync("_ViewAll", datalist);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var thisdata = await _project.GetByIdAsync(id);


                var referralOptions = (await _userRepositoryAsync.GetDashboardAllUsersAsync())
        .Select(u => new System.Web.Mvc.SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Username
        })
        .ToList();

                var categoryOptions = (await _projectCategory.GetAllAsync())
                    .Select(c => new System.Web.Mvc.SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Title
                    })
                    .ToList();
                // Create a new instance of ProjectCreateDto and set the dropdown options
                var model = new ProjectEditDto
                {
                    Id = thisdata.Id,
                    Title = thisdata.Title,
                    Description = thisdata.Description,
                    Materials = thisdata.Materials,
                    Note = thisdata.Note,
                    Authentications = thisdata.Authentications,
                    ProfileReferralId = thisdata.ProfileReferralId,
                    ProjectCategoryId = thisdata.ProjectCategoryId,
                    LogoPicture = thisdata.LogoUrl,
                    Urls = thisdata.Urls,
                    LastUpdateDate = thisdata.LastDateUpdated,
                    ProjectStatus = thisdata.ProjectStatus,

                    ReferralOptions = referralOptions,
                    CategoryOptions = categoryOptions
                };

                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_Edit", model) });
            }
        }
        public async Task<JsonResult> OnPostEditAsync(long id, ProjectEditDto thisdata, IFormFile? file)
        {
               ModelState.Remove("ReferralOptions");
            ModelState.Remove("CategoryOptions");
            ModelState.Remove("LogoPicture");
            if (ModelState.IsValid)
            {

                
                await _project.UpdateProjectAsync(thisdata, file);



                var datalist = await _project.GetAllProjectByQuery();
                var html = await _renderService.ToStringAsync("_ViewAll", datalist);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_Edit", thisdata);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> OnGetDetailsAsync(long id = 0)
        {
            if (id == 0)
            {
                var datalist = await _project.GetAllProjectByQuery();
                var html = await _renderService.ToStringAsync("_ViewAll", datalist);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var thisdata = await _project.GetByIdAsync(id);

                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_Details", thisdata) });
            }
        }

        //public async Task<JsonResult> OnPostDeleteAsync(long id)
        //{
        //    var data = await _project.GetByIdAsync(id);
        //    await _project.DeleteAsync(data);
        //    await _unitOfWork.Commit();
        //    var datalist = await _project.GetAllProjectByQuery();
        //    var html = await _renderService.ToStringAsync("_ViewAll", datalist);
        //    return new JsonResult(new { isValid = true, html = html });
        //}
    }
}
