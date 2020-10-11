using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertiseCategoriesController : ControllerBase
    {
        private readonly IExpertiseCategoryRepository expertiseCategoryRepository;

        public ExpertiseCategoriesController(IExpertiseCategoryRepository expertiseCategoryRepository)
        {
            this.expertiseCategoryRepository = expertiseCategoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetExpertiseCategories()
        {
            try
            {
                return Ok(await expertiseCategoryRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpertiseCategory>> GetExpertiseCategory(int id)
        {
            try
            {
                var result = await expertiseCategoryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpertiseCategory>> CreateExpertiseCategory(ExpertiseCategory expertiseCategory)
        {
            try
            {
                if (expertiseCategory == null)
                {
                    return BadRequest();
                }

                var result = await expertiseCategoryRepository.Add(expertiseCategory);

                return CreatedAtAction(nameof(GetExpertiseCategory), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExpertiseCategory>> UpdateExpertiseCategory(int id, ExpertiseCategory expertiseCategory)
        {
            try
            {
                if (id != expertiseCategory.Id)
                {
                    return BadRequest("ExpertiseCategory ID mismatch");
                }

                var result = await expertiseCategoryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"ExpertiseCategory with Id = {id} not found.");
                }

                return await expertiseCategoryRepository.Update(expertiseCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExpertiseCategory>> DeleteExpertiseCategory(int id)
        {
            try
            {
                var result = await expertiseCategoryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"ExpertiseCategory with Id = {id} not found.");
                }

                return await expertiseCategoryRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
