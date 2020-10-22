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
    public class PersonCompetenciesController : ControllerBase
    {
        private readonly IPersonCompetenciesRepository competencyRepository;

        public PersonCompetenciesController(IPersonCompetenciesRepository competencyRepository)
        {
            this.competencyRepository = competencyRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEducations()
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
        public async Task<ActionResult<UserCompetency>> GetEducation(int id)
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

        [HttpGet("List/{UserID:int}")]
        public async Task<ActionResult<IEnumerable<UserCompetency>>> GetEducations(int UserID)
        {
            try
            {
                var result = await competencyRepository.GetUserCompetences(UserID);

                if (result == null)
                {
                    return NotFound();
                }

                return result.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserCompetency>> CreateEducation(UserCompetency UserCompetency)
        {
            try
            {
                if (UserCompetency == null)
                {
                    return BadRequest();
                }

                var result = await competencyRepository.Add(UserCompetency);

                return CreatedAtAction(nameof(GetEducation), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserCompetency>> UpdateEducation(UserCompetency UserCompetency)
        {
            try
            {
                //if (id != education.Id)
                //{
                //    return BadRequest("Education ID mismatch");
                //}

                //var result =  competencyRepository.GetById(education.Id);

                if (UserCompetency == null)
                {
                    return new UserCompetency();
                }

                return await competencyRepository.Update(UserCompetency);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserCompetency>> DeleteEducation(int id)
        {
            try
            {
                if (id > 0)
                {
                    return await competencyRepository.Delete(id);
                }

                return null;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data " + ex.Message);
            }
        }
    }
}
