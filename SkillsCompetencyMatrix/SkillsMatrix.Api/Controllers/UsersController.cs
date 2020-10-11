using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using SkillsMatrix.Api.Models;

namespace UsersMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersCategories()
        {
            try
            {
                return Ok(await usersRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            try
            {
                var result = await usersRepository.GetById(id);

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
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var result = await usersRepository.Add(user);

                return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Users>> UpdateUsers(int id, Users user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("Users ID mismatch");
                }

                var result = await usersRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Users with Id = {id} not found.");
                }

                return await usersRepository.Update(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            try
            {
                var result = await usersRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Users with Id = {id} not found.");
                }

                return await usersRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
