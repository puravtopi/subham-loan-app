using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Helpers;
using WebApi.ViewModel;

namespace WebApi.Services.Interface
{
    public interface ITenantService
    {
        #region Tasks
        ServiceResponse<TenantTasksVM> AddTask(TenantTasksVM model);

        ServiceResponse<TenantTasksVM> EditTask(TenantTasksVM model);

        ServiceResponse<bool> RemoveTask(int Id);

        ServiceResponse<TenantTasksVM> GetTaskById(int Id);

        ServiceResponse<List<TenantTasksVM>> GetAllTask(int Id);
        #endregion

        #region Needs
        ServiceResponse<TenantNeedVM> AddNeed(TenantNeedVM model);

        ServiceResponse<TenantNeedVM> EditNeed(TenantNeedVM model);

        ServiceResponse<bool> RemoveNeed(int Id);

        ServiceResponse<TenantNeedVM> GetNeedById(int Id);

        ServiceResponse<List<TenantNeedVM>> GetAllNeed(int Id);
        #endregion

        #region MileStone

        ServiceResponse<TenantMilestoneVM> EditMileStone(TenantMilestoneVM model);

        ServiceResponse<TenantMilestoneVM> GetMileStoneById(int Id);

        ServiceResponse<List<TenantMilestoneVM>> GetAllMileStone(int Id);
        #endregion

        #region Persona

        ServiceResponse<TenantUserPersonaVM> EditPersona(TenantUserPersonaVM model);

        ServiceResponse<TenantUserPersonaVM> GetPersonaById(int Id);

        ServiceResponse<List<TenantUserPersonaVM>> GetAllPersona(int Id);
        #endregion

        #region Company

        ServiceResponse<CompanyMasterVM> GetCompanyById(int Id);
        ServiceResponse<CompanyMasterVM> EditCompany(CompanyMasterVM model);

        #endregion

        #region Branch
        ServiceResponse<BranchMasterVM> AddBranch(BranchMasterVM model);

        ServiceResponse<BranchMasterVM> EditBranch(BranchMasterVM model);

        ServiceResponse<bool> RemoveBranch(int Id);

        ServiceResponse<BranchMasterVM> GetBranchById(int Id);

        ServiceResponse<List<BranchMasterVM>> GetAllBranch(int Id);

        #endregion

        #region Users
        ServiceResponse<UserMasterVM> AddUser(UserMasterVM model);

        ServiceResponse<UserMasterVM> EditUser(UserMasterVM model);

        ServiceResponse<bool> RemoveUser(int Id);

        ServiceResponse<UserMasterVM> GetUserById(int Id);

        ServiceResponse<List<UserMasterVM>> GetAllUser(int Id);
        #endregion
    }
}
