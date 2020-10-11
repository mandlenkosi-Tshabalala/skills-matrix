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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository skillsRepository;

        public SkillsController(ISkillsRepository skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSkillsCategories()
        {
            try
            {
                return Ok(await skillsRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Skills>> GetSkills(int id)
        {
            try
            {
                var result = await skillsRepository.GetById(id);

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
        public async Task<ActionResult<Skills>> CreateSkills(Skills skills)
        {
            try
            {
                if (skills == null)
                {
                    return BadRequest();
                }

                var result = await skillsRepository.Add(skills);

                return CreatedAtAction(nameof(GetSkills), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Skills>> UpdateSkills(int id, Skills skills)
        {
            try
            {
                if (id != skills.Id)
                {
                    return BadRequest("Skills ID mismatch");
                }

                var result = await skillsRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Skills with Id = {id} not found.");
                }

                return await skillsRepository.Update(skills);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Skills>> DeleteSkills(int id)
        {
            try
            {
                var result = await skillsRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Skills with Id = {id} not found.");
                }

                return await skillsRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
