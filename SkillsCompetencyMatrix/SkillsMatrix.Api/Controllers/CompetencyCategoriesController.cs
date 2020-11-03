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
    public class CompetencyCategoriesController : ControllerBase
    {
        private readonly ICompetencyCategoryRepository categoryRepository;

        public CompetencyCategoriesController(ICompetencyCategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompetencyCategories()
        {
            try
            {
                return Ok(await categoryRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompetencyCategory>> GetCompetencyCategory(int id)
        {
            try
            {
                var result = await categoryRepository.GetById(id);

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
        public async Task<ActionResult<CompetencyCategory>> CreateCompetencyCategory(CompetencyCategory CompetencyCategory)
        {
            try
            {
                if (CompetencyCategory == null)
                {
                    return BadRequest();
                }

                var result = await categoryRepository.Add(CompetencyCategory);

                return CreatedAtAction(nameof(GetCompetencyCategory), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CompetencyCategory>> UpdateCompetencyCategory(CompetencyCategory competencyCategory)
        {
            try
            {
                //if (id != competencyCategory.Id)
                //{
                //    return BadRequest("CompetencyCategory ID mismatch");
                //}

                //var result = await categoryRepository.GetById(id);

                //if (result == null)
                //{
                //    return NotFound($"CompetencyCategory with Id = {id} not found.");
                //}

                return await categoryRepository.Update(competencyCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CompetencyCategory>> DeleteCompetencyCategory(int id)
        {
            try
            {
                var result = await categoryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"CompetencyCategory with Id = {id} not found.");
                }

                return await categoryRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
