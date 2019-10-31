using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadDataAPI.DataAccess;
using LeadDataAPI.Entities;
using LeadDataAPI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadDataAPI.Controllers
{
    /// <summary>
    /// Lead controller CRUD operations
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private IGenericRepository<Lead> _leadRepository = null;

        public LeadController(IGenericRepository<Lead> leadRepository)
        {
            _leadRepository = leadRepository;
        }
        // GET: api/Lead
        /// <summary>
        /// Returns all Leads Information
        /// </summary>
        /// <returns>Ok result containing all elements</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_leadRepository.GetAll());
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Lead/5
        /// <summary>
        /// Returns Lead details based on id
        /// </summary>
        /// <param name="id">Lead Id</param>
        /// <returns>Ok result containing all matching elements</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_leadRepository.GetById(id));
            }
            catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Lead/company/c1
        /// <summary>
        /// Returns lead details based on company
        /// </summary>
        /// <param name="company">Company name</param>
        /// <returns>Ok result containing all matching elements</returns>
        [HttpGet("company/{company}")]
        public IActionResult SearchByCompany(string company)
        {
            try
            {
                return Ok(_leadRepository.Search(lead => lead.Company == company));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Lead
        /// <summary>
        /// Creates lead data post validation
        /// </summary>
        /// <param name="lead">Lead data object</param>
        /// <returns>Created(201)/Bad Request result</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Lead lead)
        {
            try
            {
                //validate
                var validator = new LeadValidator();
                var results = validator.Validate(lead);

                if (results.IsValid)
                {
                    lead.DateCreated = DateTime.Today;
                    _leadRepository.Insert(lead);
                    return Created(string.Empty, lead); //Intentionally not writing url in response
                }
                else
                {
                    return BadRequest("Invalid input");
                }
            }
            catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Lead/5
        /// <summary>
        /// Updates the exisitng lead data based on given id
        /// </summary>
        /// <param name="id">Lead Id to be updated</param>
        /// <param name="leadParam">Updated Lead object</param>
        /// <returns>Ok/Bad Request result</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Lead leadParam)
        {
            try
            {
                //validate
                var validator = new LeadValidator();
                var results = validator.Validate(leadParam);
                if (results.IsValid)
                {
                    _leadRepository.Update(leadParam);
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid input");
                }
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Lead/5
        /// <summary>
        /// Removes lead information based on given id
        /// </summary>
        /// <param name="id">Lead Id</param>
        /// <returns>Ok/Bad Request result</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _leadRepository.Delete(id);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
