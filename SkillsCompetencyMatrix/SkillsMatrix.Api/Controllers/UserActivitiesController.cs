using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;

        public UserActivitiesController(IActivityRepository ActivityRepository)
        {
            this.activityRepository = ActivityRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetActivity()
        {
            try
            {
                return Ok(await activityRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserActivities>> GetActivity(int id)
        {
            try
            {
                var result = await activityRepository.GetById(id);

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
        public async Task<ActionResult<IEnumerable<UserActivities>>> GetUserActivities(int UserID)
        {
            try
            {
                var result = await activityRepository.GetUserActivity(UserID);

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
        public async Task<ActionResult<UserActivities>> CreateActivity(UserActivities Activity)
        {
            try
            {
                if (Activity == null)
                {
                    return BadRequest();
                }

                var result = await activityRepository.Add(Activity);

                return CreatedAtAction(nameof(GetActivity), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserActivities>> UpdateActivity(UserActivities Activity)
        {
            try
            {
                //if (id != Skill.Id)
                //{
                //    return BadRequest("Skill ID mismatch");
                //}

                //var result =  SkillRepository.GetById(Skill.Id);

                if (Activity == null)
                {
                    return new UserActivities();
                }

                return await activityRepository.Update(Activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserActivities>> DeleteActivity(int id)
        {
            try
            {
                if (id > 0)
                {
                    return await activityRepository.Delete(id);
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
