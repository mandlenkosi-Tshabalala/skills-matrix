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
    public class UserExpertiseController : ControllerBase
    {
        private readonly IUserExpertiseRepository userExpertiseRepository;

        public UserExpertiseController(IUserExpertiseRepository userExpertiseRepository)
        {
            this.userExpertiseRepository = userExpertiseRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserExpertises()
        {
            try
            {
                return Ok(await userExpertiseRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<UserExpertise>> GetUserExpertise(int UserId, int ExpertiseId)
        {
            try
            {
                var result = await userExpertiseRepository.GetByUserAndExpertiseId(UserId, ExpertiseId);

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
        public async Task<ActionResult<IEnumerable<UserExpertise>>> GetUserExpertises(int UserID)
          {
            try
            {

                var result = await userExpertiseRepository.GetUserExpertise(UserID);

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
        public async Task<ActionResult<UserExpertise>> CreateUserExpertise(UserExpertise UserExpertise)
        {
            try
            {
                if (UserExpertise == null)
                {
                    return BadRequest();
                }

                var checkexist = await userExpertiseRepository.GetByUserAndExpertiseId(UserExpertise.UserId, UserExpertise.ExpertiseId);

                if (checkexist == null)
                {
                    var result = await userExpertiseRepository.Add(UserExpertise);

                    return CreatedAtAction(nameof(GetUserExpertise), new { id = result.Id }, result);
                }
                else
                {

                    var result = await userExpertiseRepository.UnDeleteByUserAndExpertiseId(UserExpertise.UserId, UserExpertise.ExpertiseId);
                    return UserExpertise;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserExpertise>> UpdateUserExpertise( UserExpertise UserExpertise)
        {
            try
            {
                //if (id != UserExpertise.Id)
                //{
                //    return BadRequest("UserExpertise ID mismatch");
                //}

                //var result =  UserExpertiseRepository.GetById(UserExpertise.Id);

                if (UserExpertise == null)
                {
                    return new UserExpertise();
                }

                return await userExpertiseRepository.Update(UserExpertise);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data"  + ex.Message );
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserExpertise>> DeleteUserExpertise(int id)
        {
            try
            {
                if( id >  0)
                {
                    return await userExpertiseRepository.Delete(id);
                }

                return null;

            }
            catch (Exception ex )
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data " + ex.Message);
            }
        }

        [HttpDelete("[action]/{UserId:int}/{ExpertiseId:int}")]
        public async Task<ActionResult<UserExpertise>> DeleteByUserExpertiseIds(int UserId, int ExpertiseId)
        {
            try
            {
                var result = await userExpertiseRepository.DeleteByUserAndExpertiseId(UserId, ExpertiseId);

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
    }
}
