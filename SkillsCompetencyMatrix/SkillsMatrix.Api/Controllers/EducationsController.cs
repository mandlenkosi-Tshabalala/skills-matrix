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
    public class EducationsController : ControllerBase
    {
        private readonly IEducationRepository educationRepository;

        public EducationsController(IEducationRepository educationRepository)
        {
            this.educationRepository = educationRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEducations()
        {
            try
            {
                return Ok(await educationRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Education>> GetEducation(int id)
        {
            try
            {
                var result = await educationRepository.GetById(id);

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
        public async Task<ActionResult<Education>> CreateEducation(Education education)
        {
            try
            {
                if (education == null)
                {
                    return BadRequest();
                }

                var result = await educationRepository.Add(education);

                return CreatedAtAction(nameof(GetEducation), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Education>> UpdateEducation( Education education)
        {
            try
            {
                //if (id != education.Id)
                //{
                //    return BadRequest("Education ID mismatch");
                //}

                var result = await educationRepository.GetById(education.Id);

                if (result == null)
                {
                    return new Education();
                }

                return await educationRepository.Update(education);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Education>> DeleteEducation(int id)
        {
            try
            {
                var result = await educationRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Education with Id = {id} not found.");
                }

                return await educationRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
