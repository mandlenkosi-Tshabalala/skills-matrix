using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        private readonly IIndustryRepository industryRepository;

        public IndustriesController(IIndustryRepository industryRepository)
        {
            this.industryRepository = industryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetIndustryCategories()
        {
            try
            {
                return Ok(await industryRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Industry>> GetIndustry(int id)
        {
            try
            {
                var result = await industryRepository.GetById(id);

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
        public async Task<ActionResult<Industry>> CreateIndustry(Industry industry)
        {
            try
            {
                if (industry == null)
                {
                    return BadRequest();
                }

                var result = await industryRepository.Add(industry);

                return CreatedAtAction(nameof(GetIndustry), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Industry>> UpdateIndustry(Industry industry)
        {
            try
            {
                //if (id != industry.Id)
                //{
                //    return BadRequest("Industry ID mismatch");
                //}

                var result = await industryRepository.GetById(industry.Id);

                if (result == null)
                {
                    return new Industry();
                }

                return await industryRepository.Update(industry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Industry>> DeleteIndustry(int id)
        {
            try
            {
                var result = await industryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Industry with Id = {id} not found.");
                }

                return await industryRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
