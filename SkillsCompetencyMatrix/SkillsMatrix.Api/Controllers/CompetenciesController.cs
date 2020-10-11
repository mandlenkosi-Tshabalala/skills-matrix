﻿using Microsoft.AspNetCore.Http;
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
    public class CompetenciesController : ControllerBase
    {
        private readonly ICompetencyRepository competencyRepository;

        public CompetenciesController(ICompetencyRepository competencyRepository)
        {
            this.competencyRepository = competencyRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompetencies()
        {
            try
            {
                return Ok(await competencyRepository.GetAll());
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Competency>> UpdateCompetency(int id, Competency competency)
        {
            try
            {
                if (id != competency.Id)
                {
                    return BadRequest("Competency ID mismatch");
                }

                var result = await competencyRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Competency with Id = {id} not found.");
                }

                return await competencyRepository.Update(competency);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
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
