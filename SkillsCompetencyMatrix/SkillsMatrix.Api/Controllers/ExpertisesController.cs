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
    public class ExpertisesController : ControllerBase
    {
        private readonly IExpertiseRepository expertiseRepository;

        public ExpertisesController(IExpertiseRepository expertiseRepository)
        {
            this.expertiseRepository = expertiseRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetExpertises()
        {
            try
            {
                return Ok(await expertiseRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Expertise>> GetExpertise(int id)
        {
            try
            {
                var result = await expertiseRepository.GetById(id);

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
        public async Task<ActionResult<Expertise>> CreateExpertise(Expertise expertise)
        {
            try
            {
                if (expertise == null)
                {
                    return BadRequest();
                }

                var result = await expertiseRepository.Add(expertise);

                return CreatedAtAction(nameof(GetExpertise), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Expertise>> UpdateExpertise(Expertise expertise)
        {
            try
            {
                //if (id != expertise.Id)
                //{
                //    return BadRequest("Expertise ID mismatch");
                //}

               // var result = await expertiseRepository.GetById(expertise.Id);

                //if (result == null)
                //{
                //    return new Expertise();
                //}

                return await expertiseRepository.Update(expertise);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Expertise>> DeleteExpertise(int id)
        {
            try
            {
                var result = await expertiseRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Expertise with Id = {id} not found.");
                }

                return await expertiseRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
