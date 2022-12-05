using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanMst
    {
        public TblLoanMst()
        {
            TblLoanFinances = new HashSet<TblLoanFinance>();
            TblLoanNeeds = new HashSet<TblLoanNeed>();
            TblLoanProperties = new HashSet<TblLoanProperty>();
            TblLoanTasks = new HashSet<TblLoanTask>();
        }

        public int LoanId { get; set; }
        public int? TenantId { get; set; }
        public int? BranchId { get; set; }
        public DateTime? LeadDate { get; set; }
        public string CustomerName { get; set; }
        public int? BorrowerId { get; set; }
        public int? CoBorrower1Id { get; set; }
        public int? CoBorrower2Id { get; set; }
        public int? CoBorrower3Id { get; set; }
        public int? CoBorrower4Id { get; set; }
        public int? LoanType { get; set; }
        public int? LoanProg { get; set; }
        public string LoanSource { get; set; }
        public string Refferral { get; set; }
        public string BuyerAgent { get; set; }
        public bool? Fthb { get; set; }
        public DateTime? FollowupDate { get; set; }
        public bool? Priority { get; set; }
        public string TargetDates { get; set; }
        public string CreditRepair { get; set; }
        public int? MilestoneId { get; set; }
        public int? FolderId { get; set; }
        public int? SubFolderId { get; set; }
        public int? StageId { get; set; }
        public string BuyerAgentInfo { get; set; }
        public string ListingAgentInfo { get; set; }
        public string TitleCompanyInfo { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TblBorrowerMst Borrower { get; set; }
        public virtual TblBranchMaster Branch { get; set; }
        public virtual TblBorrowerMst CoBorrower1 { get; set; }
        public virtual TblBorrowerMst CoBorrower2 { get; set; }
        public virtual TblBorrowerMst CoBorrower3 { get; set; }
        public virtual TblBorrowerMst CoBorrower4 { get; set; }
        public virtual TblLoanFolder Folder { get; set; }
        public virtual TblTenantMilestone Milestone { get; set; }
        public virtual TblLoanStage Stage { get; set; }
        public virtual TblLoanSubFolder SubFolder { get; set; }
        public virtual TblTenantMaster Tenant { get; set; }
        public virtual ICollection<TblLoanFinance> TblLoanFinances { get; set; }
        public virtual ICollection<TblLoanNeed> TblLoanNeeds { get; set; }
        public virtual ICollection<TblLoanProperty> TblLoanProperties { get; set; }
        public virtual ICollection<TblLoanTask> TblLoanTasks { get; set; }
    }
}
