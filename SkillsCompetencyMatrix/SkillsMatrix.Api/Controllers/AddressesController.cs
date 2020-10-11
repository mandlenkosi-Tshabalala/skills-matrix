using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Address>>> Search(string code, string addressline)
        {
            try
            {
                var result = await addressRepository.Search(code, addressline);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            try
            {
                return Ok(await addressRepository.GetAddresses());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var result = await addressRepository.GetAddress(id);

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
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest();
                }

                var createdAddress = await addressRepository.AddAddress(address);

                return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.Id }, createdAddress);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Address>> UpdateAddress(int id, Address address)
        {
            try
            {
                if (id != address.Id)
                {
                    return BadRequest("Address ID mismatch");
                }

                var result = await addressRepository.GetAddress(id);

                if (result == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return await addressRepository.UpdateAddress(address);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            try
            {
                var addressToDelete = await addressRepository.GetAddress(id);

                if (addressToDelete == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return await addressRepository.DeleteAddress(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
