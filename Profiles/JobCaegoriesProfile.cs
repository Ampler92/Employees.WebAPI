using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;

namespace EmployeeManager.WebAPI.Profiles
{
    public class JobCategoriesProfile : Profile
    {
        public JobCategoriesProfile()
        {
            CreateMap<JobCategory, JobCategoryGetDto>();

            CreateMap<JobCategory, JobCategoryPutPostDto>();

            CreateMap<JobCategoryPutPostDto, JobCategory>();
        }
    }
}
