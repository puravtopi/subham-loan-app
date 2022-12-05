using AutoMapper;
using System.Collections.Generic;
using WebApi.EMILAEntities;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.ViewModel;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();

            CreateMap<CreateRequest, Account>();

            CreateMap<UserMasterVM, TblUserMaster>();
            CreateMap<TblUserMaster, UserMasterVM>();

            CreateMap<TblTasksDefalt, TblTenantTask>().ReverseMap();

            CreateMap<TblTenantTask, TenantTasksVM>().ReverseMap();
            CreateMap<List<TblTenantTask>, List<TenantTasksVM>>().ReverseMap();

            CreateMap<TblTenantMilestone, TenantMilestoneVM>().ReverseMap();
            CreateMap<List<TblTenantMilestone>, List<TenantMilestoneVM>>().ReverseMap();

            CreateMap<TblTenantUserPersona, TenantUserPersonaVM>().ReverseMap();
            CreateMap<List<TblTenantUserPersona>, List<TenantUserPersonaVM>>().ReverseMap();

            CreateMap<TblMilestone, MilestoneVM>().ReverseMap();
            CreateMap<List<TblMilestone>, List<MilestoneVM>>().ReverseMap();

            CreateMap<TblStatus, StatusVM>().ReverseMap();
            CreateMap<List<TblStatus>, List<StatusVM>>().ReverseMap();

            CreateMap<TblTaskGroup, TaskGroupVM>().ReverseMap();
            CreateMap<List<TblTaskGroup>, List<TaskGroupVM>>().ReverseMap();


            CreateMap<UserGroupsVM, TblUserGroup>().ReverseMap();
            CreateMap<TenantMasterVM, TblTenantMaster>().ReverseMap();
          

            CreateMap<List<UserGroupsVM>, List<TblUserGroup>>();
            CreateMap<List<TblUserGroup>, List<UserGroupsVM>>();


            CreateMap<TblUserPersona, UserPersonaVM>().ReverseMap();
            CreateMap<List<TblUserPersona>, List<UserPersonaVM>>().ReverseMap();

            CreateMap<TblCompanyMaster, CompanyMasterVM>().ReverseMap();
            CreateMap<List<TblCompanyMaster>, List<CompanyMasterVM>>().ReverseMap();

            CreateMap<TblBranchMaster, BranchMasterVM>().ReverseMap();
            CreateMap<List<BranchMasterVM>, List<CompanyMasterVM>>().ReverseMap();

            CreateMap<TblUserMaster, UserMasterVM>().ReverseMap();
            CreateMap<List<TblUserMaster>, List<UserMasterVM>>().ReverseMap();

            CreateMap<TblTenantNeed, TenantNeedVM>().ReverseMap();
            CreateMap<List<TblTenantNeed>, List<TenantNeedVM>>().ReverseMap();

            CreateMap<TblTenantTask, TenantTasksVM>().ReverseMap();
            CreateMap<List<TblTenantTask>, List<TenantTasksVM>>().ReverseMap();

            CreateMap<TblSection, SectionVM>().ReverseMap();
            CreateMap<List<TblSection>, List<SectionVM>>().ReverseMap();

            CreateMap<TblTenantMilestone, TblMilestone>().ReverseMap();
            CreateMap<TblTenantUserPersona, TblUserPersona>().ReverseMap();

            CreateMap<LoanBasicVM, TblLoanMst>().ReverseMap();
            CreateMap<LoanPropertyVM, TblLoanProperty>().ReverseMap();

            CreateMap<LoanTeamVM, TblLoanTeam>().ReverseMap();
            CreateMap<LoanFinanceVM, TblLoanFinance>().ReverseMap();

            CreateMap<LoanNeedVM, TblLoanNeed>().ReverseMap();
            CreateMap<LoanTasksVM, TblLoanTask>().ReverseMap();


            CreateMap<UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
        }
    }
}