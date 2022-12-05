using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Services;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : BaseController
    {
        private readonly ICommonService _commonService;
        private readonly IEmailService _emailService;
        public CommonController(
                   ICommonService commonService,
                   IEmailService emailService
                  )
        {
            _commonService = commonService;
            _emailService = emailService;
        }

        //[Authorize]
        [HttpGet("GetMilestones")]
        public IActionResult GetMilestones()
        {
            var _data = _commonService.GetMilestones();
            return Ok(_data);
        }

        //[Authorize]
        [HttpGet("GetStatus")]
        public IActionResult GetStatus()
        {
            var _data = _commonService.GetStatus();
            return Ok(_data);
        }

        //[Authorize]
        [HttpGet("GetTaskGroup")]
        public IActionResult GetTaskGroup()
        {
            var _data = _commonService.GetTaskGroup();
            return Ok(_data);
        }

        //[Authorize]
        [HttpGet("GetUserPersona")]
        public IActionResult GetUserPersona()
        {
            var _data = _commonService.GetUserPersona();
            return Ok(_data);
        }

        //[Authorize]
        [HttpGet("GetSection")]
        public IActionResult GetSection()
        {
            var _data = _commonService.GetSections();
            return Ok(_data);
        }


        [HttpGet("GetLoanFolder")]
        public IActionResult GetLoanFolder()
        {
            var _data = _commonService.GetLoanFolder();
            return Ok(_data);
        }

        [HttpGet("GetLoanSubFolder")]
        public IActionResult GetLoanSubFolder()
        {
            var _data = _commonService.GetLoanSubFolder();
            return Ok(_data);
        }

        [HttpGet("GetLoanStage")]
        public IActionResult GetLoanStage()
        {
            var _data = _commonService.GetLoanStage();
            return Ok(_data);
        }
    }
}
