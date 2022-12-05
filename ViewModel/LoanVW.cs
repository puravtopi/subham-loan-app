using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanVW
    {
        public int LoanId { get; set; }
        public int? TenantId { get; set; }
        public int? BranchId { get; set; }
        public DateTime? LeadDate { get; set; }
        public int? BorrowerId { get; set; }
        public int? CoBorrower1Id { get; set; }
        public int? CoBorrower2Id { get; set; }
        public int? CoBorrower3Id { get; set; }
        public int? CoBorrower4Id { get; set; }
        public int? LoanType { get; set; }
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
        public virtual BranchMasterVM Branch { get; set; }
        public virtual BorrowerVM CoBorrower1 { get; set; }
        public virtual BorrowerVM CoBorrower2 { get; set; }
        public virtual BorrowerVM CoBorrower3 { get; set; }
        public virtual BorrowerVM CoBorrower4 { get; set; }
        public virtual BorrowerVM Borrower { get; set; }

        public virtual LoanFolderVM Folder { get; set; }
        public virtual TenantMilestoneVM Milestone { get; set; }

        public virtual LoanStageVM Stage { get; set; }
        public virtual LoanSubFolderVM SubFolder { get; set; }
        public virtual TenantMasterVM Tenant { get; set; }
    }
}
