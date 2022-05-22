using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionerDetailsService.Models;
using PensionerDetailsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailsService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        private readonly IPensionerDataservice _applicationDataSetup;
        private readonly ILogger<PensionerDetailController> _logger;

        public PensionerDetailController(IPensionerDataservice applicationDataSetup, ILogger<PensionerDetailController> logger)
        {
            _logger = logger;
            _applicationDataSetup = applicationDataSetup;
        }

        /// <summary>
        /// Get List of Pensioner
        /// </summary>
        /// <returns></returns>
        [Route("GetPensionerList")]
        [HttpGet]
        public List<PensionerDetail> GetPensionerList()
        {
            _logger.LogInformation("GetPensionerList method started");
            return _applicationDataSetup.GetPensionerDetails();
        }

        /// <summary>
        /// Get Pensioner Detail by aadhaarNumber
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns>Pensioner detail associated to the aadhaarNumber</returns>
        [Route("PensionerDetailByAadhaar")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PensionerDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PensionerDetail> PensionerDetailByAadhaar(long aadhaarNumber)
        {
            _logger.LogInformation("PensionerDetailByAadhaar method started");
            List<PensionerDetail> pensionerList = _applicationDataSetup.GetPensionerDetails();
            PensionerDetail pensioner = pensionerList.FirstOrDefault(i => i.AadharNumber == aadhaarNumber);
            if (pensioner == null)
                return BadRequest(new { message = "Please enter valid aadhar details" });

            return Ok(pensioner);

        }
    }
}
