
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            try
            {
                var result = await personRepository.GetById(id);

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
        public async Task<ActionResult<Person>> CreatePerson(Person person)
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, Person person)
        {
            try
            {
                if (id != person.Id)
                {
                    return BadRequest("Person ID mismatch");
                }

                var result = await personRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Person with Id = {id} not found.");
                }

                return await personRepository.Update(person);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
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
