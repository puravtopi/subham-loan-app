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
using BC = BCrypt.Net.BCrypt;

namespace WebApi.Services.Implementation
{
    public class TenantService : ITenantService
    {

        #region Feilds
        private readonly emiladbContext _dbContext;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TenantService(emiladbContext dbContext, IMapper mapper, ICommonService commonService)
        {
            _commonService = commonService;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region PublicMethod

        #region Tasks
        public ServiceResponse<TenantTasksVM> AddTask(TenantTasksVM model)
        {
            ServiceResponse<TenantTasksVM> _response = new ServiceResponse<TenantTasksVM>();

            try
            {
                var _objTenantTask = _mapper.Map<TblTenantTask>(model);
                _dbContext.TblTenantTasks.Add(_objTenantTask);
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

                _commonService.SaveError(ex, "ServiceResponse<TenantTasks> Add(TenantTasks model)");
            }

            return _response;
        }

        public ServiceResponse<TenantTasksVM> EditTask(TenantTasksVM model)
        {
            ServiceResponse<TenantTasksVM> _response = new ServiceResponse<TenantTasksVM>();

            try
            {
                var _objTenantTask = _mapper.Map<TblTenantTask>(model);
                _dbContext.Update(_objTenantTask);
                _dbContext.SaveChanges();

                _response.Message = "Task Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<TenantTasks> Edit(TenantTasks model)");
            }

            return _response;
        }

        public ServiceResponse<bool> RemoveTask(int Id)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();

            try
            {
                var _objTenantTask = _dbContext.TblTenantTasks.Where(x => x.TenantTaskId == Id).FirstOrDefault();
                _dbContext.Remove(_objTenantTask);
                _dbContext.SaveChanges();

                _response.Message = "Task removed Successfully.";
                _response.Model = true;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = false;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<bool> Remove(int Id)");
            }

            return _response;
        }

        public ServiceResponse<TenantTasksVM> GetTaskById(int Id)
        {
            ServiceResponse<TenantTasksVM> _response = new ServiceResponse<TenantTasksVM>();
            try
            {
                var _objTenantTask = _dbContext.TblTenantTasks.Where(x => x.TenantTaskId == Id).FirstOrDefault();

                var _objTenantTaskVM = _mapper.Map<TenantTasksVM>(_objTenantTask);

                _response.Message = "Tenant Task Data";
                _response.Success = true;
                _response.Model = _objTenantTaskVM;
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

        public ServiceResponse<List<TenantTasksVM>> GetAllTask(int Id)
        {
            ServiceResponse<List<TenantTasksVM>> _response = new ServiceResponse<List<TenantTasksVM>>();
            try
            {
                var _objTenantTask = _dbContext.TblTenantTasks
                    .Include(x => x.Milestone)
                    .Include(x => x.Own)
                    .Include(x => x.Status)
                    .Include(x => x.TaskGroup)
                    .Where(x => x.TenantId == Id).ToList();

                var _listTenantTask = new List<TenantTasksVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objTenantTask)
                {
                    var _objTenantTaskVM = new TenantTasksVM()
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
                        Tenant = _mapper.Map<TenantMasterVM>(item.Tenant),
                        TenantId = item.TenantId,
                        TenantTaskId = item.TenantTaskId
                    };
                    _listTenantTask.Add(_objTenantTaskVM);
                }

                _response.Message = "Tenant Task Data";
                _response.Success = true;
                _response.Model = _listTenantTask;
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
        #endregion

        #region Needs
        public ServiceResponse<TenantNeedVM> AddNeed(TenantNeedVM model)
        {
            ServiceResponse<TenantNeedVM> _response = new ServiceResponse<TenantNeedVM>();

            try
            {
                var _objTenantNeed = _mapper.Map<TblTenantNeed>(model);
                _dbContext.TblTenantNeeds.Add(_objTenantNeed);
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

                _commonService.SaveError(ex, "ServiceResponse<TenantNeedVM> AddNeed(TenantNeedVM model)");
            }

            return _response;
        }

        public ServiceResponse<TenantNeedVM> EditNeed(TenantNeedVM model)
        {
            ServiceResponse<TenantNeedVM> _response = new ServiceResponse<TenantNeedVM>();

            try
            {
                var _objTenantNeed = _mapper.Map<TblTenantNeed>(model);
                _dbContext.Update(_objTenantNeed);
                _dbContext.SaveChanges();

                _response.Message = "Need Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<TenantNeedVM> EditNeed(TenantNeedVM model)");
            }

            return _response;
        }

        public ServiceResponse<bool> RemoveNeed(int Id)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();

            try
            {
                var _objTenantNeed = _dbContext.TblTenantNeeds.Where(x => x.TenantneedId == Id).FirstOrDefault();
                _dbContext.Remove(_objTenantNeed);
                _dbContext.SaveChanges();

                _response.Message = "Needs removed Successfully.";
                _response.Model = true;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = false;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<bool> Remove(int Id)");
            }

            return _response;
        }

        public ServiceResponse<TenantNeedVM> GetNeedById(int Id)
        {
            ServiceResponse<TenantNeedVM> _response = new ServiceResponse<TenantNeedVM>();
            try
            {
                var _objTenantNeed = _dbContext.TblTenantNeeds.Where(x => x.TenantneedId == Id).FirstOrDefault();

                var _objTenantNeedVM = _mapper.Map<TenantNeedVM>(_objTenantNeed);

                _response.Message = "Tenant Need Data";
                _response.Success = true;
                _response.Model = _objTenantNeedVM;
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

        public ServiceResponse<List<TenantNeedVM>> GetAllNeed(int Id)
        {
            ServiceResponse<List<TenantNeedVM>> _response = new ServiceResponse<List<TenantNeedVM>>();
            try
            {
                var _objTenantNeed = _dbContext.TblTenantNeeds
                    .Include(x => x.Section)
                    .Include(x => x.Own)
                    .Include(x => x.Status)
                    .Include(x => x.TaskGroup)

                    .Where(x => x.TenantId == Id).ToList();


                var _listTenantNeed = new List<TenantNeedVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objTenantNeed)
                {
                    var _objTenantNeedVM = new TenantNeedVM()
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
                        Tenant = _mapper.Map<TenantMasterVM>(item.Tenant),
                        TenantId = item.TenantId,
                        TenantneedId = item.TenantneedId,
                        Coordinating = item.Coordinating,
                        Sample = item.Sample,
                        Tracking = item.Tracking,
                        Video = item.Video,
                        NeedEn = item.NeedEn,
                        NeedSp = item.NeedSp,
                        _3rdparty = item._3rdparty
                    };
                    _listTenantNeed.Add(_objTenantNeedVM);
                }

                _response.Message = "Tenant Need Data";
                _response.Success = true;
                _response.Model = _listTenantNeed;
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
        #endregion

        #region MileStone
        public ServiceResponse<TenantMilestoneVM> EditMileStone(TenantMilestoneVM model)
        {
            ServiceResponse<TenantMilestoneVM> _response = new ServiceResponse<TenantMilestoneVM>();

            try
            {
                var _objTenantMileStone = _mapper.Map<TblTenantMilestone>(model);
                _dbContext.Update(_objTenantMileStone);
                _dbContext.SaveChanges();

                _response.Message = "MileStone Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<TenantMilestoneVM> EditMileStone(TenantMilestoneVM model)");
            }

            return _response;
        }

        public ServiceResponse<TenantMilestoneVM> GetMileStoneById(int Id)
        {
            ServiceResponse<TenantMilestoneVM> _response = new ServiceResponse<TenantMilestoneVM>();
            try
            {
                var _objTenantMilestone = _dbContext.TblTenantMilestones.Where(x => x.TenantMilestoneId == Id).FirstOrDefault();

                var _objTenantMileStoneVM = _mapper.Map<TenantMilestoneVM>(_objTenantMilestone);

                _response.Message = "Tenant Need Data";
                _response.Success = true;
                _response.Model = _objTenantMileStoneVM;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<TenantMilestoneVM> GetMileStoneById(int Id)");
            }
            return _response;
        }

        public ServiceResponse<List<TenantMilestoneVM>> GetAllMileStone(int Id)
        {
            ServiceResponse<List<TenantMilestoneVM>> _response = new ServiceResponse<List<TenantMilestoneVM>>();
            try
            {
                var _objTenantMileStone = _dbContext.TblTenantMilestones
                                   .Where(x => x.TenantId == Id).ToList();



                var _listTenantMileStone = new List<TenantMilestoneVM>();


                foreach (var item in _objTenantMileStone)
                {
                    var _objTenantMilestoneVM = new TenantMilestoneVM()
                    {

                        MilestonePos = item.MilestonePos,
                        Tenant = _mapper.Map<TenantMasterVM>(item.Tenant),
                        TenantId = item.TenantId,
                        MilestoneName = item.MilestoneName,
                        TenantMilestoneId = item.TenantMilestoneId

                    };
                    _listTenantMileStone.Add(_objTenantMilestoneVM);
                }

                _response.Message = "Tenant Need Data";
                _response.Success = true;
                _response.Model = _listTenantMileStone;
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
        #endregion

        #region Persona
        public ServiceResponse<TenantUserPersonaVM> EditPersona(TenantUserPersonaVM model)
        {
            ServiceResponse<TenantUserPersonaVM> _response = new ServiceResponse<TenantUserPersonaVM>();

            try
            {
                var _objTenantPersona = _mapper.Map<TblTenantUserPersona>(model);
                _dbContext.Update(_objTenantPersona);
                _dbContext.SaveChanges();

                _response.Message = "Persona Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<TenantUserPersonaVM> EditPersona(TenantUserPersonaVM model)");
            }

            return _response;
        }

        public ServiceResponse<TenantUserPersonaVM> GetPersonaById(int Id)
        {
            ServiceResponse<TenantUserPersonaVM> _response = new ServiceResponse<TenantUserPersonaVM>();
            try
            {
                var _objTenantPersona = _dbContext.TblTenantUserPersonas.Where(x => x.TenantPersonaId == Id).FirstOrDefault();

                var _objTenantPersonaVM = _mapper.Map<TenantUserPersonaVM>(_objTenantPersona);

                _response.Message = "Tenant Persona Data";
                _response.Success = true;
                _response.Model = _objTenantPersonaVM;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<TenantUserPersonaVM> GetPersonaById(int Id)");
            }
            return _response;
        }

        public ServiceResponse<List<TenantUserPersonaVM>> GetAllPersona(int Id)
        {
            ServiceResponse<List<TenantUserPersonaVM>> _response = new ServiceResponse<List<TenantUserPersonaVM>>();
            try
            {
                var _objTenantPersona = _dbContext.TblTenantUserPersonas
                                   .Where(x => x.TenantId == Id).ToList();



                // var _listTenantPersona = _mapper.Map<List<TenantUserPersonaVM>>(_objTenantPersona);

                var _listTenantPersona = new List<TenantUserPersonaVM>();


                foreach (var item in _objTenantPersona)
                {
                    var _objTenantUserPersonaVM = new TenantUserPersonaVM()
                    {

                        Group = _mapper.Map<UserGroupsVM>(item.Group),
                        Tenant = _mapper.Map<TenantMasterVM>(item.Tenant),
                        TenantId = item.TenantId,
                        GroupId = item.GroupId,
                        PersonaName = item.PersonaName,
                        PersonaShortname = item.PersonaShortname,
                        TenantPersonaId = item.TenantPersonaId

                    };
                    _listTenantPersona.Add(_objTenantUserPersonaVM);
                }




                _response.Message = "Tenant Persona Data";
                _response.Success = true;
                _response.Model = _listTenantPersona;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<List<TenantUserPersonaVM>> GetAllPersona(int Id)");
            }
            return _response;
        }
        #endregion

        #region Company

        public ServiceResponse<CompanyMasterVM> GetCompanyById(int Id)
        {
            ServiceResponse<CompanyMasterVM> _response = new ServiceResponse<CompanyMasterVM>();
            try
            {
                var _objCompany = _dbContext.TblCompanyMasters.Where(x => x.CompanyId == Id).FirstOrDefault();

                var _objCompanyVM = _mapper.Map<CompanyMasterVM>(_objCompany);

                _response.Message = "Company Data";
                _response.Success = true;
                _response.Model = _objCompanyVM;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<CompanyMasterVM> GetCompanyById(int Id)");
            }
            return _response;
        }
        public ServiceResponse<CompanyMasterVM> EditCompany(CompanyMasterVM model)
        {
            ServiceResponse<CompanyMasterVM> _response = new ServiceResponse<CompanyMasterVM>();

            try
            {
                var _objTenantCompany = _mapper.Map<TblCompanyMaster>(model);
                _dbContext.Update(_objTenantCompany);
                _dbContext.SaveChanges();

                _response.Message = "Company Details Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<CompanyMasterVM> EditCompany(CompanyMasterVM model)");
            }

            return _response;
        }

        #endregion

        #region Branch
        public ServiceResponse<BranchMasterVM> AddBranch(BranchMasterVM model)
        {
            ServiceResponse<BranchMasterVM> _response = new ServiceResponse<BranchMasterVM>();

            try
            {
                var _objBranch = _mapper.Map<TblBranchMaster>(model);
                _dbContext.TblBranchMasters.Add(_objBranch);
                _dbContext.SaveChanges();

                _response.Message = "Branch Added Successfully.";
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

                _commonService.SaveError(ex, "ServiceResponse<BranchMasterVM> AddBranch(BranchMasterVM model)");
            }

            return _response;
        }

        public ServiceResponse<BranchMasterVM> EditBranch(BranchMasterVM model)
        {
            ServiceResponse<BranchMasterVM> _response = new ServiceResponse<BranchMasterVM>();

            try
            {
                var _objBranch = _mapper.Map<TblBranchMaster>(model);
                _dbContext.Update(_objBranch);
                _dbContext.SaveChanges();

                _response.Message = "Branch Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<BranchMasterVM> EditBranch(BranchMasterVM model)");
            }

            return _response;
        }

        public ServiceResponse<bool> RemoveBranch(int Id)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();

            try
            {
                var _objBranch = _dbContext.TblBranchMasters.Where(x => x.BranchId == Id).FirstOrDefault();
                _dbContext.Remove(_objBranch);
                _dbContext.SaveChanges();

                _response.Message = "Branch removed Successfully.";
                _response.Model = true;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = false;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<bool> RemoveBranch(int Id)");
            }

            return _response;
        }

        public ServiceResponse<BranchMasterVM> GetBranchById(int Id)
        {
            ServiceResponse<BranchMasterVM> _response = new ServiceResponse<BranchMasterVM>();
            try
            {
                var _objbranch = _dbContext.TblBranchMasters.Where(x => x.BranchId == Id).FirstOrDefault();

                var _objbranchVM = _mapper.Map<BranchMasterVM>(_objbranch);

                _response.Message = "Tenant Task Data";
                _response.Success = true;
                _response.Model = _objbranchVM;
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

        public ServiceResponse<List<BranchMasterVM>> GetAllBranch(int cmpId)
        {
            ServiceResponse<List<BranchMasterVM>> _response = new ServiceResponse<List<BranchMasterVM>>();
            try
            {
                var _objBranch = _dbContext.TblBranchMasters
                    .Include(x => x.Company)
                                       .Where(x => x.CompanyId == cmpId).ToList();

                var _listBranch = new List<BranchMasterVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objBranch)
                {
                    var _objBranchVM = new BranchMasterVM()
                    {
                        BranchId = item.BranchId,
                        Company = _mapper.Map<CompanyMasterVM>(item.Company),
                        BranchAddress1 = item.BranchAddress1,
                        BranchAddress2 = item.BranchAddress2,
                        BranchCity = item.BranchCity,
                        BranchCountry = item.BranchCountry,
                        BranchName = item.BranchName,
                        BranchNotes = item.BranchName,
                        BranchPcEmail = item.BranchPcEmail,
                        BranchPcName = item.BranchPcName,
                        BranchPcPhone = item.BranchPcPhone,
                        BranchState = item.BranchState,
                        CompanyId = item.CompanyId,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt
                    };
                    _listBranch.Add(_objBranchVM);
                }

                _response.Message = "Branch List";
                _response.Success = true;
                _response.Model = _listBranch;
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
        #endregion

        #region Users
        public ServiceResponse<UserMasterVM> AddUser(UserMasterVM model)
        {
            ServiceResponse<UserMasterVM> _response = new ServiceResponse<UserMasterVM>();

            try
            {
                var _objUsers = _mapper.Map<TblUserMaster>(model);


                _objUsers.UserPassword = BC.HashPassword("Test@123");

                _dbContext.TblUserMasters.Add(_objUsers);
                _dbContext.SaveChanges();

                _response.Message = "User Added Successfully.";
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

                _commonService.SaveError(ex, "ServiceResponse<UserMasterVM> AddUser(UserMasterVM model)");
            }

            return _response;
        }

        public ServiceResponse<UserMasterVM> EditUser(UserMasterVM model)
        {
            ServiceResponse<UserMasterVM> _response = new ServiceResponse<UserMasterVM>();
            try
            {
                var _objUser = _mapper.Map<TblUserMaster>(model);
                _objUser.UserPassword = BC.HashPassword("Test@123");

                _dbContext.Update(_objUser);
                _dbContext.SaveChanges();

                _response.Message = "Branch Updated Successfully.";
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
                _commonService.SaveError(ex, "ServiceResponse<UserMasterVM> EditUser(UserMasterVM model)");
            }

            return _response;
        }

        public ServiceResponse<bool> RemoveUser(int Id)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();

            try
            {
                var _objUser = _dbContext.TblUserMasters.Where(x => x.UserId == Id).FirstOrDefault();
                _dbContext.Remove(_objUser);
                _dbContext.SaveChanges();

                _response.Message = "User removed Successfully.";
                _response.Model = true;
                _response.Success = true;
                _response.ErrorCode = "1";

            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = false;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<bool> RemoveUser(int Id)");
            }

            return _response;
        }

        public ServiceResponse<UserMasterVM> GetUserById(int Id)
        {
            ServiceResponse<UserMasterVM> _response = new ServiceResponse<UserMasterVM>();
            try
            {
                var _objUser = _dbContext.TblUserMasters.Where(x => x.UserId == Id).FirstOrDefault();

                var _objUserVM = _mapper.Map<UserMasterVM>(_objUser);



                _response.Message = "User Data";
                _response.Success = true;
                _response.Model = _objUserVM;
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

        public ServiceResponse<List<UserMasterVM>> GetAllUser(int cmpId)
        {
            ServiceResponse<List<UserMasterVM>> _response = new ServiceResponse<List<UserMasterVM>>();
            try
            {
                var _objUser = _dbContext.TblUserMasters
                    .Include(x => x.Branch)
                    .Include(x => x.Persona)
                    .Where(x => x.Branch.CompanyId == cmpId).ToList();


                var _listUser = new List<UserMasterVM>();
                // _listTenantTask = _mapper.Map<List<TenantTasksVM>>(_objTenantTask);

                foreach (var item in _objUser)
                {
                    var _objUserVM = new UserMasterVM()
                    {
                        BranchId = item.BranchId,
                        BranchName = item.Branch.BranchName,
                        PersonaName = item.Persona.PersonaShortname,
                        //Branch = _mapper.Map<BranchMasterVM>(item.Branch),
                        FirstName = item.FirstName,
                        IsFirstLogin = item.IsFirstLogin,
                        IsVerified = item.IsVerified,
                        LastName = item.LastName,
                        // Persona = _mapper.Map<TenantUserPersonaVM>(item.Persona),
                        PersonaId = item.PersonaId,
                        Phone = item.Phone,
                        UserEmail = item.UserEmail,
                        UserId = item.UserId,
                        UserInitials = item.UserInitials,

                    };
                    _listUser.Add(_objUserVM);
                }

                _response.Message = "Branch List";
                _response.Success = true;
                _response.Model = _listUser;
                _response.ErrorCode = "1";
            }
            catch (Exception ex)
            {
                _response.Message = "Sorry !! something went wronge";
                _response.Model = null;
                _response.Success = false;
                _response.ErrorCode = "0";
                _commonService.SaveError(ex, "ServiceResponse<List<UserMasterVM>> GetAllUser(int cmpId)");
            }
            return _response;
        }
        #endregion

        #endregion
    }
}
