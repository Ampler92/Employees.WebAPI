using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.WebAPI.Data;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;
using EmployeeManager.WebAPI.Repositories;

namespace EmployeeManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(IRepository<Country> countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryGetDto>>> GetJobCategories()
        {
            var allCountries =  await _countryRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CountryGetDto>>(allCountries));
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public ActionResult<CountryGetDto> GetCountry(Guid id)
        {
            var country = _countryRepository.GetById(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryGetDto>(country));
        }

        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public IActionResult PutCountry(Guid id, CountryPutPostDto countryDto)
        {
            var country = _countryRepository.GetById(id);

            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(countryDto, country);
            
            try
            {
                _countryRepository.Update(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated");
        }

        // POST: api/Countries
        [HttpPost]
        public ActionResult<CountryPutPostDto> PostCountry(CountryPutPostDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            _countryRepository.Insert(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, _mapper.Map<CountryGetDto>(country));
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(Guid id)
        {
            var country = _countryRepository.GetById(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryRepository.Delete(id);

            return Ok("Deleted");
        }

        private bool CountryExists(Guid id)
        {
            return _countryRepository.Exists(id);
        }
    }
}
