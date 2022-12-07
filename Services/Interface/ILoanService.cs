using System.Collections.Generic;
using WebApi.Helpers;
using WebApi.ViewModel;

namespace WebApi.Services.Interface
{
    public interface ILoanService
    {
        #region Tasks
        ServiceResponse<LoanBasicVM> AddLoan(LoanBasicVM model);

        ServiceResponse<LoanBasicVM> UpdateLoan(LoanBasicVM model);

        ServiceResponse<List<LoanBasicVM>> GetAllLoan(int tenantId);

        ServiceResponse<List<LoanBasicVM>> GetAllLoanByFolder(int TenantId, int FolderId);

        ServiceResponse<LoanBasicVM> EditLoan(int loadId);

        ServiceResponse<CoBorrowerVM> ManageCoBorrower(CoBorrowerVM model);

        ServiceResponse<CoBorrowerVM> GetCoBorrower(int loadId);

        ServiceResponse<LoanPropertyVM> AddProperty(LoanPropertyVM model);

        ServiceResponse<LoanPropertyVM> UpdateProperty(LoanPropertyVM model);

        ServiceResponse<LoanPropertyVM> GetProperty(int loadId);

        ServiceResponse<LoanFinanceVM> AddFinance(LoanFinanceVM model);

        ServiceResponse<LoanFinanceVM> GetFinance(int loanId);

        ServiceResponse<LoanTeamVM> AddTeam(LoanTeamVM model);

        ServiceResponse<LoanTeamVM> GetTeam(int loanId);

        ServiceResponse<List<LoanTasksVM>> GetAllTask(int loanId);

        ServiceResponse<List<LoanNeedVM>> GetAllNeeds(int loanId);

        #endregion
    }
}
