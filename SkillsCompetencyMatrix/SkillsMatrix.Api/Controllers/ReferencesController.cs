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
    public class ReferencesController : ControllerBase
    {
        private readonly IReferenceRepository referenceRepository;

        public ReferencesController(IReferenceRepository referenceRepository)
        {
            this.referenceRepository = referenceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetReferenceCategories()
        {
            try
            {
                return Ok(await referenceRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reference>> GetReference(int id)
        {
            try
            {
                var result = await referenceRepository.GetById(id);

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
        public async Task<ActionResult<Reference>> CreateReference(Reference reference)
        {
            try
            {
                if (reference == null)
                {
                    return BadRequest();
                }

                var result = await referenceRepository.Add(reference);

                return CreatedAtAction(nameof(GetReference), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Reference>> UpdateReference(int id, Reference reference)
        {
            try
            {
                if (id != reference.Id)
                {
                    return BadRequest("Reference ID mismatch");
                }

                var result = await referenceRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Reference with Id = {id} not found.");
                }

                return await referenceRepository.Update(reference);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reference>> DeleteReference(int id)
        {
            try
            {
                var result = await referenceRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"Reference with Id = {id} not found.");
                }

                return await referenceRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
