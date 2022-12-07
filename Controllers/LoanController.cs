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
    public class LoanController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService,
                  IMapper mapper)
        {
            _mapper = mapper;
            _loanService = loanService;
        }

        #region Public Methods
        [HttpPost("add-loan")]
        public IActionResult AddLoan(LoanBasicVM model)
        {
            var _result = _loanService.AddLoan(model);
            return Ok(_result);
        }

        [HttpPost("update-loan")]
        public IActionResult UpdateLoan(LoanBasicVM model)
        {
            var _result = _loanService.UpdateLoan(model);
            return Ok(_result);
        }

        [HttpPost("get-all-loan")]
        public IActionResult GetAllLoan(int Id)
        {
            var _result = _loanService.GetAllLoan(Id);
            return Ok(_result);
        }

        [HttpPost("get-all-loan-by-folder")]
        public IActionResult GetAllLoanByFolder(int TenantId,int FolderId)
        {
            var _result = _loanService.GetAllLoanByFolder(TenantId, FolderId);
            return Ok(_result);
        }

        [HttpPost("get-loan-byId")]
        public IActionResult GetAllLoanById(int Id)
        {
            var _result = _loanService.EditLoan(Id);
            return Ok(_result);
        }

        [HttpPost("add-coborrower")]
        public IActionResult AddCoBorrower(CoBorrowerVM model)
        {
            var _result = _loanService.ManageCoBorrower(model);
            return Ok(_result);
        }

        [HttpPost("get-coborrower")]
        public IActionResult GetCoBorrower(int loanId)
        {
            var _result = _loanService.GetCoBorrower(loanId);
            return Ok(_result);
        }

        [HttpPost("add-loan-property")]
        public IActionResult AddLoanProperty(LoanPropertyVM model)
        {
            var _result = _loanService.AddProperty(model);
            return Ok(_result);
        }

        [HttpPost("update-loan-property")]
        public IActionResult UpdateLoanProperty(LoanPropertyVM model)
        {
            var _result = _loanService.UpdateProperty(model);
            return Ok(_result);
        }

        [HttpPost("get-loan-property")]
        public IActionResult getLoanProperty(int loanId)
        {
            var _result = _loanService.GetProperty(loanId);
            return Ok(_result);
        }

        [HttpPost("manage-loan-finance")]
        public IActionResult AddLoanFianace(LoanFinanceVM model)
        {
            var _result = _loanService.AddFinance(model);
            return Ok(_result);
        }

        
        [HttpPost("get-loan-finance")]
        public IActionResult getLoanFinance(int loanId)
        {
            var _result = _loanService.GetFinance(loanId);
            return Ok(_result);
        }

        [HttpPost("manage-loan-team")]
        public IActionResult AddLoanTeam(LoanTeamVM model)
        {
            var _result = _loanService.AddTeam(model);
            return Ok(_result);
        }

        
        [HttpPost("get-loan-team")]
        public IActionResult getLoanTeam(int loanId)
        {
            var _result = _loanService.GetTeam(loanId);
            return Ok(_result);
        }

        [HttpPost("get-loan-task")]
        public IActionResult getLoanTask(int loanId)
        {
            var _result = _loanService.GetAllTask(loanId);
            return Ok(_result);
        }

        [HttpPost("get-loan-need")]
        public IActionResult getLoanNeed(int loanId)
        {
            var _result = _loanService.GetAllNeeds(loanId);
            return Ok(_result);
        }
        #endregion
    }
}
