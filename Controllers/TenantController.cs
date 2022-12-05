using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services.Interface;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenantController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService,
                     IMapper mapper)
        {
            _mapper = mapper;
            _tenantService = tenantService;
        }


        #region Task
        [HttpPost("add-task")]
        public IActionResult AddTask(TenantTasksVM model)
        {
            var _result = _tenantService.AddTask(model);
            return Ok(_result);
        }

        [HttpPost("update-task")]
        public IActionResult UpdateTask(TenantTasksVM model)
        {
            var _result = _tenantService.EditTask(model);
            return Ok(_result);
        }


        [HttpPost("remove-task")]
        public IActionResult RemoveTask(int Id)
        {
            var _result = _tenantService.RemoveTask(Id);
            return Ok(_result);
        }

        [HttpPost("get-task-byId")]
        public IActionResult GetById(int Id)
        {
            var _result = _tenantService.GetTaskById(Id);
            return Ok(_result);
        }

        [HttpPost("get-all-task")]
        public IActionResult GetAll(int Id)
        {
            var _result = _tenantService.GetAllTask(Id);
            return Ok(_result);
        }

        #endregion

        #region Need
        [HttpPost("add-need")]
        public IActionResult AddNeed(TenantNeedVM model)
        {
            var _result = _tenantService.AddNeed(model);
            return Ok(_result);
        }

        [HttpPost("update-need")]
        public IActionResult UpdateNeed(TenantNeedVM model)
        {
            var _result = _tenantService.EditNeed(model);
            return Ok(_result);
        }

        [HttpPost("remove-need")]
        public IActionResult RemoveNeed(int Id)
        {
            var _result = _tenantService.RemoveNeed(Id);
            return Ok(_result);
        }

        [HttpPost("get-need-byId")]
        public IActionResult GetNeedById(int Id)
        {
            var _result = _tenantService.GetNeedById(Id);
            return Ok(_result);
        }

        [HttpPost("get-all-need")]
        public IActionResult GetAllNeed(int Id)
        {
            var _result = _tenantService.GetAllNeed(Id);
            return Ok(_result);
        }
        #endregion

        #region MileStone
        [HttpPost("update-milestone")]
        public IActionResult UpdateMileStone(TenantMilestoneVM model)
        {
            var _result = _tenantService.EditMileStone(model);
            return Ok(_result);
        }

        [HttpPost("get-all-milestone")]
        public IActionResult GetAllMileStone(int Id)
        {
            var _result = _tenantService.GetAllMileStone(Id);
            return Ok(_result);
        }

        [HttpPost("get-milestone-byId")]
        public IActionResult GetMileStoneById(int Id)
        {
            var _result = _tenantService.GetMileStoneById(Id);
            return Ok(_result);
        }
        #endregion

        #region Branch

        [HttpPost("add-branch")]
        public IActionResult AddBranch(BranchMasterVM model)
        {
            var _result = _tenantService.AddBranch(model);
            return Ok(_result);
        }

        [HttpPost("update-branch")]
        public IActionResult UpdateBranch(BranchMasterVM model)
        {
            var _result = _tenantService.EditBranch(model);
            return Ok(_result);
        }

        [HttpPost("get-all-branch")]
        public IActionResult GetAllBranch(int Id)
        {
            var _result = _tenantService.GetAllBranch(Id);
            return Ok(_result);
        }

        [HttpPost("get-branch-byId")]
        public IActionResult GetBranchById(int Id)
        {
            var _result = _tenantService.GetBranchById(Id);
            return Ok(_result);
        }

        [HttpPost("remove-branch")]
        public IActionResult RemoveBranch(int Id)
        {
            var _result = _tenantService.RemoveBranch(Id);
            return Ok(_result);
        }
        #endregion

        #region User

        [HttpPost("add-user")]
        public IActionResult AddUser(UserMasterVM model)
        {
            var _result = _tenantService.AddUser(model);
            return Ok(_result);
        }

        [HttpPost("update-user")]
        public IActionResult UpdateBranch(UserMasterVM model)
        {
            var _result = _tenantService.EditUser(model);
            return Ok(_result);
        }

        [HttpPost("get-all-user")]
        public IActionResult GetAllUsers(int Id)
        {
            var _result = _tenantService.GetAllUser(Id);
            return Ok(_result);
        }

        [HttpPost("get-user-byId")]
        public IActionResult GetUserById(int Id)
        {
            var _result = _tenantService.GetUserById(Id);
            return Ok(_result);
        }
        #endregion

        #region Company

        [HttpPost("get-company-details")]
        public IActionResult GetCompanyDetails(int Id)
        {
            var _result = _tenantService.GetCompanyById(Id);
            return Ok(_result);
        }

        [HttpPost("update-company")]
        public IActionResult UpdateCompany(CompanyMasterVM model)
        {
            var _result = _tenantService.EditCompany(model);
            return Ok(_result);
        }

       
        #endregion

        #region Persona

        [HttpPost("update-persona")]
        public IActionResult UpdatePersona(TenantUserPersonaVM model)
        {
            var _result = _tenantService.EditPersona(model);
            return Ok(_result);
        }

        [HttpPost("get-all-persona")]
        public IActionResult GetAllPersona(int Id)
        {
            var _result = _tenantService.GetAllPersona(Id);
            return Ok(_result);
        }

        [HttpPost("get-persona-byId")]
        public IActionResult GetPersonaById(int Id)
        {
            var _result = _tenantService.GetPersonaById(Id);
            return Ok(_result);
        }
        #endregion

    }
}
