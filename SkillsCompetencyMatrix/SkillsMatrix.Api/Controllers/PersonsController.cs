
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
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPersonCategories()
        {
            try
            {
                return Ok(await personRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await personRepository.GetAllEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonalInfo>> GetPerson(int id)
        {
            try
            {
                var result = await personRepository.GetById(id);

                if (result == null)
                {
                    return new PersonalInfo();
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
        public async Task<ActionResult<PersonalInfo>> CreatePerson(PersonalInfo person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest();
                }

                var result = await personRepository.Add(person);

                return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error saving data {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<PersonalInfo>> UpdatePerson(PersonalInfo person)
        {
            try
            {

                //var result = await personRepository.GetById(person.Id);

                //if (result == null)
                //{
                //    return NotFound($"Person with Id = {person.Id} not found.");
                //}

                return await personRepository.Update(person);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  $"Error updating data{ex.Message}" );
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PersonalInfo>> DeletePerson(int id)
        {
            try
            {
                var result = await personRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Person with Id = {id} not found.");
                }

                return await personRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
