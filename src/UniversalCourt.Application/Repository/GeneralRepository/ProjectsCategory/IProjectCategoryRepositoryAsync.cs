using UniversalCourt.Application.Dtos.Project;
using UniversalCourt.Application.Repository.Base;
using UniversalCourt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Repository.GeneralRepository.ProjectsCategory
{
    public interface IProjectCategoryRepositoryAsync : IGenericRepositoryAsync<ProjectCategory>
    {
        Task<IReadOnlyList<ProjectCategoryListDto>> GetAllProjectCategoryWithProjectCountAsync();
    }
}
