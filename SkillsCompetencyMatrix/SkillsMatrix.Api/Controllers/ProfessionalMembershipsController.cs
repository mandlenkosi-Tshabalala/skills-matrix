using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfessionalMembershipsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalMembershipsController : ControllerBase
    {
        private readonly IProfessionalMembershipRepository ProfessionalMembershipRepository;

        public ProfessionalMembershipsController(IProfessionalMembershipRepository professionalMembershipRepository)
        {
            this.ProfessionalMembershipRepository = professionalMembershipRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProfessionalMemberships()
        {
            try
            {
                return Ok(await ProfessionalMembershipRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Membership>> GetProfessionalMembership(int id)
        {
            try
            {
                var result = await ProfessionalMembershipRepository.GetById(id);

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
        public async Task<ActionResult<IEnumerable<Membership>>> GetProfessionalMemberships(int UserID)
        {
            try
            {
                var result = await ProfessionalMembershipRepository.GetMemberships(UserID);

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
        public async Task<ActionResult<Membership>> CreateProfessionalMembership(Membership ProfessionalMembership)
        {
            try
            {
                if (ProfessionalMembership == null)
                {
                    return BadRequest();
                }

                var result = await ProfessionalMembershipRepository.Add(ProfessionalMembership);

                return CreatedAtAction(nameof(GetProfessionalMembership), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Membership>> UpdateProfessionalMembership(Membership ProfessionalMembership)
        {
            try
            {
                //if (id != ProfessionalMembership.Id)
                //{
                //    return BadRequest("ProfessionalMembership ID mismatch");
                //}

                //var result =  ProfessionalMembershipRepository.GetById(ProfessionalMembership.Id);

                if (ProfessionalMembership == null)
                {
                    return new Membership();
                }

                return await ProfessionalMembershipRepository.Update(ProfessionalMembership);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Membership>> DeleteProfessionalMembership(int id)
        {
            try
            {
                if (id > 0)
                {
                    return await ProfessionalMembershipRepository.Delete(id);
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
