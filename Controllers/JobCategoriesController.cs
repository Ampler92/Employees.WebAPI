using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.WebAPI.Data;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;
using EmployeeManager.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly IRepository<JobCategory> _jobCategoryRepository;
        private readonly IMapper _mapper;

        public JobCategoriesController(IRepository<JobCategory> jobCategoryRepository, IMapper mapper)
        {
            _jobCategoryRepository = jobCategoryRepository;
            _mapper = mapper;
        }

        // GET: api/JobCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryGetDto>>> GetJobCategories()
        {
            var allJobCategories = await _jobCategoryRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<JobCategoryGetDto>>(allJobCategories));
        }

        // GET: api/JobCategories/5
        [HttpGet("{id}")]
        public ActionResult<JobCategoryGetDto> GetJobCategory(Guid id)
        {
            var jobCategory = _jobCategoryRepository.GetById(id);

            if (jobCategory == null) return NotFound();

            return Ok(_mapper.Map<JobCategoryGetDto>(jobCategory));
        }

        // PUT: api/JobCategories/5
        [HttpPut("{id}")]
        public IActionResult PutJobCategory(Guid id, JobCategoryPutPostDto jobCategoryDto)
        {
            var jobCategory = _jobCategoryRepository.GetById(id);

            if (jobCategory == null) return NotFound();

            _mapper.Map(jobCategoryDto, jobCategory);

            try
            {
                _jobCategoryRepository.Update(jobCategory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCategoryExists(id))
                    return NotFound();
                throw;
            }

            return Ok("Updated");
        }

        // POST: api/JobCategories
        [HttpPost]
        public ActionResult<JobCategoryPutPostDto> PostJobCategory(JobCategoryPutPostDto jobCategoryDto)
        {
            var jobCategory = _mapper.Map<JobCategory>(jobCategoryDto);
            _jobCategoryRepository.Insert(jobCategory);

            return CreatedAtAction("GetJobCategory", new {id = jobCategory.Id}, _mapper.Map<JobCategoryGetDto>(jobCategory));
        }

        // DELETE: api/JobCategories/5
        [HttpDelete("{id}")]
        public IActionResult DeleteJobCategory(Guid id)
        {
            var jobCategory = _jobCategoryRepository.GetById(id);
            if (jobCategory == null) return NotFound();

            _jobCategoryRepository.Delete(id);

            return Ok("Deleted");
        }

        private bool JobCategoryExists(Guid id)
        {
            return _jobCategoryRepository.Exists(id);
        }
    }
}