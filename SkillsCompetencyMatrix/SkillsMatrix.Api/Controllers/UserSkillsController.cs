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
    public class UserSkillsController : ControllerBase
    {
        private readonly IUserSkillsRepository skillsRepository;

        public UserSkillsController(IUserSkillsRepository SkillRepository)
        {
            this.skillsRepository = SkillRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSkills()
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
        public async Task<ActionResult<Skill>> GetSkill(int id)
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

        [HttpGet("List/{UserID:int}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills(int UserID)
          {
            try
            {
                var result = await skillsRepository.GetUserSkills(UserID);

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
        public async Task<ActionResult<Skill>> CreateSkill(Skill Skill)
        {
            try
            {
                if (Skill == null)
                {
                    return BadRequest();
                }

                var result = await skillsRepository.Add(Skill);

                return CreatedAtAction(nameof(GetSkill), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Skill>> UpdateSkill( Skill Skill)
        {
            try
            {
                //if (id != Skill.Id)
                //{
                //    return BadRequest("Skill ID mismatch");
                //}

                //var result =  SkillRepository.GetById(Skill.Id);

                if (Skill == null)
                {
                    return new Skill();
                }

                return await skillsRepository.Update(Skill);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data"  + ex.Message );
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Skill>> DeleteSkill(int id)
        {
            try
            {
                if( id >  0)
                {
                    return await skillsRepository.Delete(id);
                }

                return null;

            }
            catch (Exception ex )
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data " + ex.Message);
            }
        }
    }
}
