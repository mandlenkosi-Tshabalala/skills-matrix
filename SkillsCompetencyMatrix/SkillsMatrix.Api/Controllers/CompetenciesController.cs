using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SkillsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenciesController : ControllerBase
    {
        private readonly ICompetencyRepository competencyRepository;
        private readonly ILogger<CompetenciesController> _logger;

        public CompetenciesController(ICompetencyRepository competencyRepository, ILogger<CompetenciesController> logger)
        {
            this.competencyRepository = competencyRepository;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompetencies()
        {
            try
            {
                _logger.LogInformation("GetCompetencies Started");
                return Ok(await competencyRepository.GetAllCompetencies());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database", null);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("List/{id}")]
        public async Task<ActionResult> GetCompetencies(int id)
        {
            try
            {
                _logger.LogInformation("GetCompetencies Started");
                return Ok(await competencyRepository.GetAllByID(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database", null);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Competency>> GetCompetency(int id)
        {
            try
            {
                var result = await competencyRepository.GetById(id);

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
        public async Task<ActionResult<Competency>> CreateCompetency(Competency competency)
        {
            try
            {
                if (competency == null)
                {
                    return BadRequest();
                }

                var result = await competencyRepository.Add(competency);

                return CreatedAtAction(nameof(GetCompetency), new { id = result.Id }, result);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error saving data {Ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Competency>> UpdateCompetency( Competency competency)
        {
            try
            {


                return await competencyRepository.Update(competency);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  $"Error updating data{ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Competency>> DeleteCompetency(int id)
        {
            try
            {
                var result = await competencyRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Competency with Id = {id} not found.");
                }

                return await competencyRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
