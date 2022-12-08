using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Helpers;
using WebApi.Services.Interface;
using WebApi.ViewModel;

namespace WebApi.Services.Implementation
{
    public class LoanService : ILoanService
    {
        #region Feilds
        private readonly emiladbContext _dbContext;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public LoanService(emiladbContext dbContext, IMapper mapper, ICommonService commonService)
        {
            _commonService = commonService;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region PublicMethod
        public ServiceResponse<LoanBasicVM> AddLoan(LoanBasicVM model)
        {
            ServiceResponse<LoanBasicVM> _response = new ServiceResponse<LoanBasicVM>();

            try
            {
                //add master borrower

                var _objborrower = new TblBorrowerMst()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    TenantId = model.TenantId,
                    LangInfo = model.LangInfo,
                    IsMainBorrower = true

                };

                _dbContext.TblBorrowerMsts.Add(_objborrower);
                _dbContext.SaveChanges();
                              
                model.FolderId = 1;

                var _objTenantMileStone = _dbContext.TblTenantMilestones.Where(x => x.MilestonePos == 1 && x.TenantId == model.TenantId).FirstOrDefault();

                if (_objTenantMileStone != null)
                {
                    model.MilestoneId = _objTenantMileStone.TenantMilestoneId;
                }


                var _objLoanMast = _mapper.Map<TblLoanMst>(model);

                _objLoanMast.BorrowerId = _objborrower.BorrowerId;


                _dbContext.TblLoanMsts.Add(_objLoanMast);
                _dbContext.SaveChanges();

                //add data into loan needs
                var _listNeeds = _dbContext.TblTenantNeeds.ToList();

                var _listLoanNeeds = new List<TblLoanNeed>();

                foreach (var item in _listNeeds)
                {
                    var _objLoanNeeds = _mapper.Map<TblLoanNeed>(item);
                    _objLoanNeeds.LoanId = _objLoanMast.LoanId;
                    _listLoanNeeds.Add(_objLoanNeeds);
                }


                _dbContext.TblLoanNeeds.AddRange(_listLoanNeeds);
                _dbContext.SaveChanges();

                //add data into loan needs
                var _listTasks = _dbContext.TblTenantTasks.ToList();

                var _listLoanTasks = new List<TblLoanTask>();

                foreach (var item in _listTasks)
                {
                    var _objLoanTasks = _mapper.Map<TblLoanTask>(item);
                    _objLoanTasks.LoanId = _objLoanMast.LoanId;
                    _listLoanTasks.Add(_objLoanTasks);
                }


                _dbContext.TblLoanTasks.Add(_listLoanTasks[2]);
                _dbContext.SaveChanges();

                model.LoanId = _objLoanMast.LoanId;
                _response.Message = "Loan Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanMstVW> AddLoan(LoanMstVW model)");
            }

            return _response;

        }
        public ServiceResponse<LoanBasicVM> EditLoan(int loadId)
        {
            ServiceResponse<LoanBasicVM> _response = new ServiceResponse<LoanBasicVM>();

            try
            {
                //edit loan data

                var _objLoanBasic = _dbContext.TblLoanMsts.Where(x => x.LoanId == loadId).Include(x => x.Borrower).FirstOrDefault();

                var _objLoanBasicVM = _mapper.Map<LoanBasicVM>(_objLoanBasic);

                _objLoanBasicVM.FirstName = _objLoanBasic.Borrower.FirstName;
                _objLoanBasicVM.LangInfo = _objLoanBasic.Borrower.LangInfo;
                _objLoanBasicVM.LastName = _objLoanBasic.Borrower.LastName;
                _objLoanBasicVM.Email = _objLoanBasic.Borrower.Email;
                _objLoanBasicVM.Phone = _objLoanBasic.Borrower.Phone;

                _response.Message = "Loan Details.";
                _response.Model = _objLoanBasicVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanBasicVM> EditLoan(int loadId)");
            }

            return _response;

        }
        public ServiceResponse<LoanBasicVM> UpdateLoan(LoanBasicVM model)
        {
            ServiceResponse<LoanBasicVM> _response = new ServiceResponse<LoanBasicVM>();

            try
            {
                //update master borrower

                var _objborrower = _dbContext.TblBorrowerMsts.Where(x => x.LoanId == model.LoanId && x.IsMainBorrower == true).FirstOrDefault();

                var _isEdit = true;
                if (_objborrower == null)
                {
                    _objborrower = new TblBorrowerMst();
                    _isEdit = false;
                }

                _objborrower.Email = model.Email;
                _objborrower.FirstName = model.FirstName;
                _objborrower.LangInfo = model.LangInfo;
                _objborrower.LastName = model.LastName;
                _objborrower.Phone = model.Phone;

                if (_isEdit)
                {
                    _dbContext.Update(_objborrower);
                }
                else
                {
                    _objborrower.TenantId = model.TenantId;
                    _objborrower.LoanId = model.LoanId;
                    _objborrower.IsMainBorrower = true;
                    _dbContext.TblBorrowerMsts.Add(_objborrower);
                }


                _dbContext.SaveChanges();

                var _objLoanMast = _mapper.Map<TblLoanMst>(model);

                _objLoanMast.BorrowerId = _objborrower.BorrowerId;

                _dbContext.Update(_objLoanMast);
                _dbContext.SaveChanges();

                _response.Message = "Loan Update Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanBasicVM> UpdateLoan(LoanBasicVM model)");
            }

            return _response;

        }
        public ServiceResponse<CoBorrowerVM> ManageCoBorrower(CoBorrowerVM model)
        {
            ServiceResponse<CoBorrowerVM> _response = new ServiceResponse<CoBorrowerVM>();

            try
            {
                var _objLoanInfo = _dbContext.TblLoanMsts.Where(x => x.LoanId == model.LoanId).FirstOrDefault();

                var _lstBorrower = _dbContext.TblBorrowerMsts.Where(x => x.LoanId == model.LoanId && x.IsMainBorrower == false).ToList();

                if (_lstBorrower != null)
                {
                    _dbContext.TblBorrowerMsts.RemoveRange(_lstBorrower);
                    _dbContext.SaveChanges();
                }

                if (!string.IsNullOrEmpty(model.FirstName_1))
                {
                    var _objTblBorrowerMst = new TblBorrowerMst()
                    {
                        Email = model.Email_1,
                        FirstName = model.FirstName_1,
                        LangInfo = model.LangInfo_1,
                        LastName = model.LastName_1,
                        Phone = model.Phone_1,
                        TenantId = model.TenantId,
                        IsMainBorrower = false,
                        LoanId = model.LoanId
                    };

                    _dbContext.TblBorrowerMsts.Add(_objTblBorrowerMst);
                    _dbContext.SaveChanges();

                    _objLoanInfo.CoBorrower1Id = _objTblBorrowerMst.BorrowerId;
                }

                if (!string.IsNullOrEmpty(model.FirstName_2))
                {
                    var _objTblBorrowerMst = new TblBorrowerMst()
                    {
                        Email = model.Email_2,
                        FirstName = model.FirstName_2,
                        LangInfo = model.LangInfo_2,
                        LastName = model.LastName_2,
                        Phone = model.Phone_2,
                        TenantId = model.TenantId,
                        IsMainBorrower = false,
                        LoanId = model.LoanId
                    };

                    _dbContext.TblBorrowerMsts.Add(_objTblBorrowerMst);
                    _dbContext.SaveChanges();

                    _objLoanInfo.CoBorrower2Id = _objTblBorrowerMst.BorrowerId;
                }

                if (!string.IsNullOrEmpty(model.FirstName_3))
                {
                    var _objTblBorrowerMst = new TblBorrowerMst()
                    {
                        Email = model.Email_3,
                        FirstName = model.FirstName_3,
                        LangInfo = model.LangInfo_3,
                        LastName = model.LastName_3,
                        Phone = model.Phone_3,
                        TenantId = model.TenantId,
                        IsMainBorrower = false,
                        LoanId = model.LoanId
                    };

                    _dbContext.TblBorrowerMsts.Add(_objTblBorrowerMst);
                    _dbContext.SaveChanges();

                    _objLoanInfo.CoBorrower3Id = _objTblBorrowerMst.BorrowerId;
                }

                if (!string.IsNullOrEmpty(model.FirstName_4))
                {
                    var _objTblBorrowerMst = new TblBorrowerMst()
                    {
                        Email = model.Email_4,
                        FirstName = model.FirstName_4,
                        LangInfo = model.LangInfo_4,
                        LastName = model.LastName_4,
                        Phone = model.Phone_4,
                        TenantId = model.TenantId,
                        IsMainBorrower = false,
                        LoanId = model.LoanId
                    };

                    _dbContext.TblBorrowerMsts.Add(_objTblBorrowerMst);
                    _dbContext.SaveChanges();

                    _objLoanInfo.CoBorrower4Id = _objTblBorrowerMst.BorrowerId;
                }

                _dbContext.SaveChanges();

                _response.Message = "CoBorrower Details saved.";
                _response.Success = true;
                _response.Model = model;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<CoBorrowerVM> ManageCoBorrower(CoBorrowerVM model)");
            }

            return _response;

        }
        public ServiceResponse<List<LoanBasicVM>> GetAllLoan(int tenantId)
        {
            ServiceResponse<List<LoanBasicVM>> _response = new ServiceResponse<List<LoanBasicVM>>();
            try
            {
                var _objBranch = _dbContext.TblLoanMsts
                    .Include(x => x.Borrower)
                    .Include(x => x.Milestone)
                    .Include(x => x.Folder)
                    .Include(x => x.SubFolder)
                    .Include(x => x.Stage)
                    .Where(x => x.TenantId == tenantId).ToList();

                var _listLoan = new List<LoanBasicVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objBranch)
                {
                    var _objLoanVM = new LoanBasicVM()
                    {
                        CustomerName = "",
                        Email = item.Borrower.Email,
                        FirstName = item.Borrower.FirstName,
                        FolderName = item.Folder.FolderName,
                        Fthb = item.Fthb,
                        LangInfo = item.Borrower.LangInfo,
                        LastName = item.Borrower.LastName,
                        LeadDate = item.LeadDate,
                        LoanSource = item.LoanSource,
                        LoanType = item.LoanType != null ? item.LoanType.Value : 0,
                        Phone = item.Borrower.Phone,
                        StageName = item.Stage.StageName,
                        SubFolderName = item.SubFolder.SubFolderName,
                        LoanId = item.LoanId

                    };
                    _listLoan.Add(_objLoanVM);
                }

                _response.Message = "Loan List";
                _response.Success = true;
                _response.Model = _listLoan;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<List<BranchMasterVM>> GetAllBranch(int cmpId)");
            }
            return _response;
        }

        public ServiceResponse<List<LoanBasicVM>> GetAllLoanByFolder(int TenantId, int FolderId)
        {
            ServiceResponse<List<LoanBasicVM>> _response = new ServiceResponse<List<LoanBasicVM>>();
            try
            {
                var _objBranch = _dbContext.TblLoanMsts
                   .Include(x => x.Borrower)
                   .Include(x => x.Milestone)
                   .Include(x => x.Folder)
                   .Include(x => x.SubFolder)
                   .Include(x => x.Stage)
                   .Where(x => x.TenantId == TenantId && x.FolderId == FolderId).ToList();

                var _listLoan = new List<LoanBasicVM>();

                foreach (var item in _objBranch)
                {
                    var _objLoanVM = new LoanBasicVM()
                    {
                        CustomerName = "",
                        Email = item.Borrower.Email,
                        FirstName = item.Borrower.FirstName,
                        FolderName = item.Folder.FolderName,
                        Fthb = item.Fthb,
                        LangInfo = item.Borrower.LangInfo,
                        LastName = item.Borrower.LastName,
                        LeadDate = item.LeadDate,
                        LoanSource = item.LoanSource,
                        LoanType = item.LoanType != null ? item.LoanType.Value : 0,
                        Phone = item.Borrower.Phone,
                        StageName = item.Stage.StageName,
                        SubFolderName = item.SubFolder.SubFolderName,
                        LoanId = item.LoanId

                    };
                    _listLoan.Add(_objLoanVM);
                }

                _response.Message = "Loan List";
                _response.Success = true;
                _response.Model = _listLoan;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<List<BranchMasterVM>> GetAllBranch(int cmpId)");
            }
            return _response;
        }
        public ServiceResponse<CoBorrowerVM> GetCoBorrower(int loadId)
        {
            ServiceResponse<CoBorrowerVM> _response = new ServiceResponse<CoBorrowerVM>();
            try
            {
                var _lstBorrowerVM = _dbContext.TblBorrowerMsts
                                       .Where(x => x.LoanId == loadId && x.IsMainBorrower == true).ToList();

                var _objCoBorrowerVM = new CoBorrowerVM();

                _objCoBorrowerVM.LoanId = loadId;

                if (_lstBorrowerVM.Count == 1)
                {
                    _objCoBorrowerVM.Email_1 = _lstBorrowerVM[0].Email;
                    _objCoBorrowerVM.FirstName_1 = _lstBorrowerVM[0].FirstName;
                    _objCoBorrowerVM.LangInfo_1 = _lstBorrowerVM[0].LangInfo;
                    _objCoBorrowerVM.LastName_1 = _lstBorrowerVM[0].LastName;
                    _objCoBorrowerVM.Phone_1 = _lstBorrowerVM[0].Phone;
                    _objCoBorrowerVM.TenantId = _lstBorrowerVM[0].TenantId.Value;
                }

                if (_lstBorrowerVM.Count == 2)
                {
                    _objCoBorrowerVM.Email_2 = _lstBorrowerVM[1].Email;
                    _objCoBorrowerVM.FirstName_2 = _lstBorrowerVM[1].FirstName;
                    _objCoBorrowerVM.LangInfo_2 = _lstBorrowerVM[1].LangInfo;
                    _objCoBorrowerVM.LastName_2 = _lstBorrowerVM[1].LastName;
                    _objCoBorrowerVM.Phone_2 = _lstBorrowerVM[1].Phone;
                }

                if (_lstBorrowerVM.Count == 3)
                {
                    _objCoBorrowerVM.Email_3 = _lstBorrowerVM[2].Email;
                    _objCoBorrowerVM.FirstName_3 = _lstBorrowerVM[2].FirstName;
                    _objCoBorrowerVM.LangInfo_3 = _lstBorrowerVM[2].LangInfo;
                    _objCoBorrowerVM.LastName_3 = _lstBorrowerVM[2].LastName;
                    _objCoBorrowerVM.Phone_3 = _lstBorrowerVM[2].Phone;
                }

                if (_lstBorrowerVM.Count == 4)
                {
                    _objCoBorrowerVM.Email_4 = _lstBorrowerVM[3].Email;
                    _objCoBorrowerVM.FirstName_4 = _lstBorrowerVM[3].FirstName;
                    _objCoBorrowerVM.LangInfo_4 = _lstBorrowerVM[3].LangInfo;
                    _objCoBorrowerVM.LastName_4 = _lstBorrowerVM[3].LastName;
                    _objCoBorrowerVM.Phone_4 = _lstBorrowerVM[3].Phone;
                }

                _response.Message = "CoBorrower Details";
                _response.Success = true;
                _response.Model = _objCoBorrowerVM;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<CoBorrowerVM> getCoBorrower(int loadId)");
            }
            return _response;
        }
        public ServiceResponse<LoanPropertyVM> AddProperty(LoanPropertyVM model)
        {
            ServiceResponse<LoanPropertyVM> _response = new ServiceResponse<LoanPropertyVM>();

            try
            {
                var _objLoanProperty = _mapper.Map<TblLoanProperty>(model);

                _dbContext.TblLoanProperties.Add(_objLoanProperty);
                _dbContext.SaveChanges();



                _response.Message = "Loan Property Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanPropertyVM> AddProperty(LoanPropertyVM model)");
            }

            return _response;

        }
        public ServiceResponse<LoanPropertyVM> UpdateProperty(LoanPropertyVM model)
        {
            ServiceResponse<LoanPropertyVM> _response = new ServiceResponse<LoanPropertyVM>();

            try
            {
                var _objLoanProperty = _mapper.Map<TblLoanProperty>(model);

                _dbContext.Update(_objLoanProperty);
                _dbContext.SaveChanges();



                _response.Message = "Loan Property Update Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanPropertyVM> UpdateProperty(LoanPropertyVM model)");
            }

            return _response;

        }
        public ServiceResponse<LoanPropertyVM> GetProperty(int loadId)
        {
            ServiceResponse<LoanPropertyVM> _response = new ServiceResponse<LoanPropertyVM>();

            try
            {
                var _objLoanProperty = _dbContext.TblLoanProperties.Where(x => x.LoanId == loadId).FirstOrDefault();
                var _objLoanPropertyVM = _mapper.Map<LoanPropertyVM>(_objLoanProperty);

                _response.Message = "Loan Property Added Successfully.";
                _response.Model = _objLoanPropertyVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanPropertyVM> getProperty(int loadId)");
            }

            return _response;
        }
        public ServiceResponse<LoanFinanceVM> AddFinance(LoanFinanceVM model)
        {
            ServiceResponse<LoanFinanceVM> _response = new ServiceResponse<LoanFinanceVM>();

            try
            {
                var _objLoanFinance = _mapper.Map<TblLoanFinance>(model);

                if (model.FinanceId == 0)
                    _dbContext.TblLoanFinances.Add(_objLoanFinance);
                else
                    _dbContext.Update(_objLoanFinance);
                _dbContext.SaveChanges();



                _response.Message = "Loan Finance Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanFinanceVM> AddFinance(LoanFinanceVM model)");
            }

            return _response;

        }
        public ServiceResponse<LoanFinanceVM> GetFinance(int loanId)
        {
            ServiceResponse<LoanFinanceVM> _response = new ServiceResponse<LoanFinanceVM>();

            try
            {
                var _objLoanFinance = _dbContext.TblLoanFinances.Where(x => x.LoanId == loanId).FirstOrDefault();
                var _objLoanFinanceVM = _mapper.Map<LoanFinanceVM>(_objLoanFinance);

                _response.Message = "Loan Finance details.";
                _response.Model = _objLoanFinanceVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanPropertyVM> getProperty(int loadId)");
            }

            return _response;
        }
        public ServiceResponse<LoanTeamVM> AddTeam(LoanTeamVM model)
        {
            ServiceResponse<LoanTeamVM> _response = new ServiceResponse<LoanTeamVM>();

            try
            {
                var _objLoanTeam = _mapper.Map<TblLoanTeam>(model);

                if (model.Id == 0)
                    _dbContext.TblLoanTeams.Add(_objLoanTeam);
                else
                    _dbContext.Update(_objLoanTeam);

                _dbContext.SaveChanges();



                _response.Message = "Loan Team Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanFinanceVM> AddFinance(LoanFinanceVM model)");
            }

            return _response;

        }
        public ServiceResponse<LoanTeamVM> GetTeam(int loanId)
        {
            ServiceResponse<LoanTeamVM> _response = new ServiceResponse<LoanTeamVM>();

            try
            {
                var _objLoanTeam = _dbContext.TblLoanTeams.Where(x => x.LoanId == loanId).FirstOrDefault();
                var _objLoanTeamVM = _mapper.Map<LoanTeamVM>(_objLoanTeam);

                _response.Message = "Loan Team details.";
                _response.Model = _objLoanTeamVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanTeamVM> GetTeam(int loanId)");
            }

            return _response;
        }

        public ServiceResponse<List<LoanTasksVM>> GetAllTask(int loanId)
        {
            ServiceResponse<List<LoanTasksVM>> _response = new ServiceResponse<List<LoanTasksVM>>();
            try
            {
                var _objLoanTask = _dbContext.TblLoanTasks
                    .Include(x => x.Milestone)
                    .Include(x => x.Own)
                    .Include(x => x.Status)
                    .Include(x => x.TaskGroup)
                    .Where(x => x.LoanId == loanId).ToList();

                var _listLoanTask = new List<LoanTasksVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _listLoanTask)
                {
                    var _objLoanTaskVM = new LoanTasksVM()
                    {
                        Customer = item.Customer,
                        //Milestone = _mapper.Map<MilestoneVM>(item.Milestone),
                        MilestoneName = item.Milestone.MilestoneName,
                        MilestoneId = item.MilestoneId,
                        MilestonePos = item.MilestonePos,
                        Note = item.Note,
                        //Own = _mapper.Map<UserPersonaVM>(item.Own),
                        OwnName = item.Own.PersonaShortname,
                        OwnId = item.OwnId,
                        //Status = _mapper.Map<StatusVM>(item.Status),
                        StatusName = item.Status.StatusName,
                        StatusId = item.StatusId,
                        TaskEn = item.TaskEn,
                        //TaskGroup = _mapper.Map<TaskGroupVM>(item.TaskGroup),
                        TaskGroupName = item.TaskGroup.GroupName,
                        TaskGroupId = item.TaskGroupId,
                        TaskSp = item.TaskSp,
                        LoanTaskId = item.LoanTaskId
                    };
                    _listLoanTask.Add(_objLoanTaskVM);
                }

                _response.Message = "Loan Task Data";
                _response.Success = true;
                _response.Model = _listLoanTask;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<TenantTasksVM> GetById(int Id)");
            }
            return _response;
        }

        public ServiceResponse<LoanTasksVM> AddTask(LoanTasksVM model)
        {
            ServiceResponse<LoanTasksVM> _response = new ServiceResponse<LoanTasksVM>();
            try
            {
                var _objLoanTask = _mapper.Map<TblLoanTask>(model);
                _dbContext.TblLoanTasks.Add(_objLoanTask);
                _dbContext.SaveChanges();


                _response.Message = "Task Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanTasksVM> AddTask(LoanTasksVM model)");
            }
            return _response;
        }

        public ServiceResponse<LoanTasksVM> EditTask(int LoanId)
        {
            ServiceResponse<LoanTasksVM> _response = new ServiceResponse<LoanTasksVM>();

            try
            {
                //edit Task data

                var _objTaskBasic = _dbContext.TblLoanTasks.Where(x => x.LoanId == LoanId).FirstOrDefault();

                var _objTaskBasicVM = _mapper.Map<LoanTasksVM>(_objTaskBasic);

                _objTaskBasicVM.Customer = _objTaskBasic.Customer;
                _objTaskBasicVM.MilestoneId = _objTaskBasic.MilestoneId;
                _objTaskBasicVM.MilestonePos = _objTaskBasic.MilestonePos;
                _objTaskBasicVM.TaskGroupId = _objTaskBasic.TaskGroupId;
                _objTaskBasicVM.StatusId = _objTaskBasic.StatusId;
                _objTaskBasicVM.OwnId = _objTaskBasic.OwnId;
                _objTaskBasicVM.TaskEn = _objTaskBasic.TaskEn;
                _objTaskBasicVM.TaskSp = _objTaskBasic.TaskSp;
                _objTaskBasicVM.Note = _objTaskBasic.Note;


                _response.Message = "Task Details.";
                _response.Model = _objTaskBasicVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanTasksVM> EditTask(int LoanId)");
            }

            return _response;
        }

        public ServiceResponse<LoanTasksVM> UpdateTask(LoanTasksVM model)
        {
            ServiceResponse<LoanTasksVM> _response = new ServiceResponse<LoanTasksVM>();

            try
            {
                //update Task

                var _objloantask = _dbContext.TblLoanTasks.Where(x => x.LoanId == model.LoanId && x.LoanTaskId == model.LoanTaskId).FirstOrDefault();

                var _objLoanTask = _mapper.Map<TblLoanTask>(model);
                _dbContext.TblLoanTasks.Update(_objLoanTask);
                _dbContext.SaveChanges();



                _response.Message = "Task Update Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanBasicVM> UpdateLoan(LoanBasicVM model)");
            }

            return _response;
        }

        public ServiceResponse<List<LoanNeedVM>> GetAllNeeds(int loanId)
        {
            ServiceResponse<List<LoanNeedVM>> _response = new ServiceResponse<List<LoanNeedVM>>();
            try
            {
                var _objLoanNeed = _dbContext.TblLoanNeeds
                    .Include(x => x.Section)
                    .Include(x => x.Own)
                    .Include(x => x.Status)
                    .Include(x => x.TaskGroup)
                    .Where(x => x.LoanId == loanId).ToList();


                var _listLoanNeed = new List<LoanNeedVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objLoanNeed)
                {
                    var _objLoanNeedVM = new LoanNeedVM()
                    {
                        Customer = item.Customer,
                        // Section = _mapper.Map<SectionVM>(item.Section),
                        SectionName = item.Section.SectionName,
                        SectionId = item.SectionId,
                        SectionPos = item.SectionPos,
                        // Own = _mapper.Map<UserPersonaVM>(item.Own),
                        OwnName = item.Own.PersonaShortname,
                        OwnId = item.OwnId,
                        // Status = _mapper.Map<StatusVM>(item.Status),
                        StatusName = item.Status.StatusName,
                        StatusId = item.StatusId,
                        DescEn = item.DescEn,
                        //TaskGroup = _mapper.Map<TaskGroupVM>(item.TaskGroup),
                        TaskGroupName = item.TaskGroup.GroupName,
                        TaskGroupId = item.TaskGroupId,
                        DescSp = item.DescSp,
                        LoanId = item.LoanId.Value,
                        LoanneedsId = item.LoanneedsId,
                        Coordinating = item.Coordinating,
                        Sample = item.Sample,
                        Tracking = item.Tracking,
                        Video = item.Video,
                        NeedEn = item.NeedEn,
                        NeedSp = item.NeedSp,
                        _3rdparty = item._3rdparty
                    };
                    _listLoanNeed.Add(_objLoanNeedVM);
                }

                _response.Message = "Loan Need Data";
                _response.Success = true;
                _response.Model = _listLoanNeed;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<TenantTasksVM> GetById(int Id)");
            }
            return _response;
        }

        public ServiceResponse<LoanNeedVM> AddNeeds(LoanNeedVM model)
        {
            ServiceResponse<LoanNeedVM> _response = new ServiceResponse<LoanNeedVM>();
            try
            {
                var _objLoanNeed = _mapper.Map<TblLoanNeed>(model);
                _dbContext.TblLoanNeeds.Add(_objLoanNeed);
                _dbContext.SaveChanges();


                _response.Message = "Need Added Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanNeedVM> AddNeeds(LoanNeedVM model)");
            }
            return _response;
        }

        public ServiceResponse<LoanNeedVM> EditNeeds(int loanId)
        {
            ServiceResponse<LoanNeedVM> _response = new ServiceResponse<LoanNeedVM>();

            try
            {
                //edit Needs data

                var _objneedBasic = _dbContext.TblLoanNeeds.Where(x => x.LoanId == loanId).FirstOrDefault();

                var _objneedBasicVM = _mapper.Map<LoanNeedVM>(_objneedBasic);

                _objneedBasicVM.Customer = _objneedBasic.Customer;
                _objneedBasicVM.SectionId = _objneedBasic.SectionId;
                _objneedBasicVM.SectionPos = _objneedBasic.SectionPos;
                _objneedBasicVM.TaskGroupId = _objneedBasic.TaskGroupId;
                _objneedBasicVM.StatusId = _objneedBasic.StatusId;
                _objneedBasicVM.OwnId = _objneedBasic.OwnId;
                _objneedBasicVM.NeedEn = _objneedBasic.NeedEn;
                _objneedBasicVM.NeedSp = _objneedBasic.NeedSp;
                _objneedBasicVM.Coordinating = _objneedBasic.Coordinating;
                _objneedBasicVM.Tracking = _objneedBasic.Tracking;
                _objneedBasicVM._3rdparty = _objneedBasic._3rdparty;
                _objneedBasicVM.DescEn = _objneedBasic.DescEn;
                _objneedBasicVM.DescSp = _objneedBasic.DescSp;
                _objneedBasicVM.Sample = _objneedBasic.Sample;
                _objneedBasicVM.Video = _objneedBasic.Video;




                _response.Message = "Need Details.";
                _response.Model = _objneedBasicVM;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanNeedVM> EditNeeds(int loanId)");
            }

            return _response;
        }

        public ServiceResponse<LoanNeedVM> UpdateNeeds(LoanNeedVM model)
        {
            ServiceResponse<LoanNeedVM> _response = new ServiceResponse<LoanNeedVM>();
            try
            {
                //update Task

                var _objloanneed = _dbContext.TblLoanTasks.Where(x => x.LoanId == model.LoanId && x.LoanTaskId == model.LoanneedsId).FirstOrDefault();

                var _objLoanNeed = _mapper.Map<LoanNeedVM>(model);
                _dbContext.TblLoanTasks.Update(_objloanneed);
                _dbContext.SaveChanges();



                _response.Message = "Need Update Successfully.";
                _response.Model = model;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = model;
                _response.Success = false;
                _response.ErrorCode = "0";

                _commonService.SaveError(ex, "ServiceResponse<LoanNeedVM> UpdateNeeds(LoanNeedVM model)");
            }

            return _response;
        }
        #endregion

    }
}
