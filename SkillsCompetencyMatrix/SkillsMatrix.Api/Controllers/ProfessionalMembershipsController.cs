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
    public class ProfessionalMembershipsController : ControllerBase
    {
        private readonly IProfessionalMembershipRepository professionalMembershipRepository;

        public ProfessionalMembershipsController(IProfessionalMembershipRepository professionalMembershipRepository)
        {
            this.professionalMembershipRepository = professionalMembershipRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProfessionalMembershipCategories()
        {
            try
            {
                return Ok(await professionalMembershipRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfessionalMembership>> GetProfessionalMembership(int id)
        {
            try
            {
                var result = await professionalMembershipRepository.GetById(id);

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
        public async Task<ActionResult<ProfessionalMembership>> CreateProfessionalMembership(ProfessionalMembership professionalMembership)
        {
            try
            {
                if (professionalMembership == null)
                {
                    return BadRequest();
                }

                var result = await professionalMembershipRepository.Add(professionalMembership);

                return CreatedAtAction(nameof(GetProfessionalMembership), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProfessionalMembership>> UpdateProfessionalMembership(ProfessionalMembership professionalMembership)
        {
            try
            {
                //if (id != professionalMembership.Id)
                //{
                //    return BadRequest("ProfessionalMembership ID mismatch");
                //}

                var result = await professionalMembershipRepository.GetById(professionalMembership.Id);

                if (result == null)
                {
                    return new ProfessionalMembership();
                }

                return await professionalMembershipRepository.Update(professionalMembership);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProfessionalMembership>> DeleteProfessionalMembership(int id)
        {
            try
            {
                var result = await professionalMembershipRepository.GetById(id);

                if (result == null)
                {
                    return NotFound($"ProfessionalMembership with Id = {id} not found.");
                }

                return await professionalMembershipRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
