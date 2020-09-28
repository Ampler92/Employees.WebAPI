using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;

namespace EmployeeManager.WebAPI.Profiles
{
    public class CountriesProfile : Profile
    {
        public CountriesProfile()
        {
            CreateMap<Country, CountryGetDto>();

            CreateMap<Country, CountryPutPostDto>();

            CreateMap<CountryPutPostDto, Country>();
        }
    }
}
