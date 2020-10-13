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
    public class EmploymentHistoriesController : ControllerBase
    {
        private readonly IEmploymentHistoryRepository employmentHistoryRepository;

        public EmploymentHistoriesController(IEmploymentHistoryRepository employmentHistoryRepository)
        {
            this.employmentHistoryRepository = employmentHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmploymentHistories()
        {
            try
            {
                return Ok(await employmentHistoryRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmploymentHistory>> GetEmploymentHistory(int id)
        {
            try
            {
                var result = await employmentHistoryRepository.GetById(id);

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
        public async Task<ActionResult<EmploymentHistory>> CreateEmploymentHistory(EmploymentHistory employmentHistory)
        {
            try
            {
                if (employmentHistory == null)
                {
                    return BadRequest();
                }

                var result = await employmentHistoryRepository.Add(employmentHistory);

                return CreatedAtAction(nameof(GetEmploymentHistory), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<EmploymentHistory>> UpdateEmploymentHistory(EmploymentHistory employmentHistory)
        {
            try
            {
                //if (id != employmentHistory.Id)
                //{
                //    return BadRequest("EmploymentHistory ID mismatch");
                //}

                var result = await employmentHistoryRepository.GetById(employmentHistory.Id);

                if (result == null)
                {
                    return new EmploymentHistory();
                }

                return await employmentHistoryRepository.Update(employmentHistory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmploymentHistory>> DeleteEmploymentHistory(int id)
        {
            try
            {
                var result = await employmentHistoryRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"EmploymentHistory with Id = {id} not found.");
                }

                return await employmentHistoryRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
