
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillsMatrix.Api.Models;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using SelectPdf;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetEmployees(string EmployeeName, int expertiseID, int competencyCategoryID, string Skills, string QualificationLevel, string Country, int competencyID)
        {
            try
            {
                return Ok(await personRepository.GetAllEmployees(EmployeeName, expertiseID, competencyCategoryID, Skills, QualificationLevel, Country, competencyID));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetByUserId/{UserId}")]
        public async Task<ActionResult<PersonalInfo>> GetByUserId(int UserId)
        {
            try
            {
                var result = await personRepository.GetByUserId(UserId);

                if (result == null)
                {
                    return new PersonalInfo();
                }

                return result;
            }
            catch (Exception ex)
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

        [HttpGet("downloadCV/{id:int}")]
        public async Task<ActionResult<PersonalInfo>> GetCV(int id)
        {
            try
            {
                string url = $"https://localhost:44349/viewPDF/{id}";

                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                    pdf_page_size, true);

                string pdf_orientation = "Portrait";
                //PdfPageOrientation pdfOrientation =
                //    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                //    pdf_orientation, true);

                int webPageWidth = 1024;
                try
                {
                    webPageWidth = Convert.ToInt32("1024");
                }
                catch { }

                int webPageHeight = 0;
                try
                {
                    webPageHeight = Convert.ToInt32("0");
                }
                catch { }

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // set converter options
                converter.Options.PdfPageSize = pageSize;
                //  converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.MarginBottom = 15;
                converter.Options.MarginTop = 15;
                converter.Options.MarginLeft = 10;
                converter.Options.MarginRight = 10;
                // create a new pdf document converting an url
                PdfDocument doc = converter.ConvertUrl(url);

                MemoryStream ms = new MemoryStream();
                doc.Save(ms);
                doc.Save();

                ms.Position = 0;

                FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
                fileStreamResult.FileDownloadName = "mycv.pdf";

                return fileStreamResult;

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

        [HttpPut("UpdateCompletion/{userID:int}/{Percentage:int}")]
        public async Task<ActionResult<PersonalInfo>> UpdateCompletion(int userID,int Percentage)
        {
            try
            {
                return await personRepository.UpdatePercentage(userID,Percentage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  $"Error updating data{ex.Message}");
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
