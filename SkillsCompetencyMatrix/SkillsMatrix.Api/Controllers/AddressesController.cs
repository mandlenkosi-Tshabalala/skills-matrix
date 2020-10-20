using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IAddressRepository addressRepository, ILogger<AddressesController> logger)
        {
            this.addressRepository = addressRepository;
            this._logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            try
            {
                return Ok(await addressRepository.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "addressRepository.GetAddresses()");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var result = await addressRepository.GetById(id);

                if (result == null)
                {
                    return new Address();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "addressRepository.GetAddress(id)");

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        [HttpGet("GetByUserId/{UserId}")]
        public async Task<ActionResult<Address>> GetByUserId(int UserId)
        {
            try
            {
                var result = await addressRepository.GetByUserId(UserId);

                if (result == null)
                {
                    return new Address();
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

                var createdAddress = await addressRepository.Add(address);

                return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.Id }, createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "addressRepository.AddAddress(address)");

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error saving data");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Address>> UpdateAddress(Address address)
        {
            try
            {
                return await addressRepository.Update(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "addressRepository.UpdateAddress(address)");

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            try
            {
                var addressToDelete = await addressRepository.GetById(id);

                if (addressToDelete == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return await addressRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "addressRepository.DeleteAddress(id)");

                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error deleting data");
            }
        }
    }
}
