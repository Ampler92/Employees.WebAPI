using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;

namespace EmployeeManager.WebAPI.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeGetDto>();

            CreateMap<Employee, EmployeePutPostDto>();

            CreateMap<EmployeePutPostDto, Employee>();
        }
    }
}