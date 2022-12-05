using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Helpers;

namespace WebApi.Services.Interface
{
    public interface ICommonService
    {
        void SaveError(Exception model, string function);
        ServiceResponse<List<TblMilestone>> GetMilestones();
        ServiceResponse<List<TblSection>> GetSections();
        ServiceResponse<List<TblStatus>> GetStatus();
        ServiceResponse<List<TblTaskGroup>> GetTaskGroup();
        ServiceResponse<List<TblUserPersona>> GetUserPersona();
        ServiceResponse<List<TblLoanFolder>> GetLoanFolder();
        ServiceResponse<List<TblLoanStage>> GetLoanStage();
        ServiceResponse<List<TblLoanSubFolder>> GetLoanSubFolder(int pos = 0);
    }
}
