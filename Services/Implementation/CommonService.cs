using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Helpers;
using WebApi.Services.Interface;

namespace WebApi.Services.Implementation
{
    public class CommonService : ICommonService
    {
        #region Feilds
        private readonly emiladbContext _dbContext;

        #endregion

        #region Constructor
        public CommonService(emiladbContext dbContext)
        {
            _dbContext = dbContext;

        }
        #endregion

        #region PublicMethod
        public void SaveError(Exception model, string function)
        {

            var _objErroLog = new TblErrorLog();
            _objErroLog.FunctonName = function;
            _objErroLog.CreateDate = System.DateTime.Now;

            if (model.InnerException == null)
                _objErroLog.Desc = model.Message;
            else
                _objErroLog.Desc = model.InnerException.Message;

            _dbContext.TblErrorLogs.Add(_objErroLog);
            _dbContext.SaveChanges();
        }

        public ServiceResponse<List<TblMilestone>> GetMilestones()
        {
            ServiceResponse<List<TblMilestone>> _response = new ServiceResponse<List<TblMilestone>>();

            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblMilestones.ToList();
            return _response;
        }

        public ServiceResponse<List<TblStatus>> GetStatus()
        {
            ServiceResponse<List<TblStatus>> _response = new ServiceResponse<List<TblStatus>>();

            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblStatuses.ToList();

            return _response;
        }

        public ServiceResponse<List<TblTaskGroup>> GetTaskGroup()
        {
            ServiceResponse<List<TblTaskGroup>> _response = new ServiceResponse<List<TblTaskGroup>>();

            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblTaskGroups.ToList();

            return _response;
        }

        public ServiceResponse<List<TblUserPersona>> GetUserPersona()
        {
            ServiceResponse<List<TblUserPersona>> _response = new ServiceResponse<List<TblUserPersona>>();

            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblUserPersonas.ToList();
            return _response;
        }

        public ServiceResponse<List<TblSection>> GetSections()
        {
            ServiceResponse<List<TblSection>> _response = new ServiceResponse<List<TblSection>>();


            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblSections.ToList();

            return _response;
        }

        public ServiceResponse<List<TblLoanFolder>> GetLoanFolder()
        {
            ServiceResponse<List<TblLoanFolder>> _response = new ServiceResponse<List<TblLoanFolder>>();


            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblLoanFolders.ToList();

            return _response;
        }

        public ServiceResponse<List<TblLoanStage>> GetLoanStage()
        {
            ServiceResponse<List<TblLoanStage>> _response = new ServiceResponse<List<TblLoanStage>>();


            _response.ErrorCode = "1";
            _response.Success = true;
            _response.Model = _dbContext.TblLoanStages.ToList();

            return _response;
        }

        public ServiceResponse<List<TblLoanSubFolder>> GetLoanSubFolder(int pos = 0)
        {
            ServiceResponse<List<TblLoanSubFolder>> _response = new ServiceResponse<List<TblLoanSubFolder>>();

            _response.ErrorCode = "1";
            _response.Success = true;
            if (pos == 0)
                _response.Model = _dbContext.TblLoanSubFolders.ToList();
            else
                _response.Model = _dbContext.TblLoanSubFolders.Where(x => x.MilestonePos == pos).ToList();

            return _response;
        }
        #endregion
    }
}
