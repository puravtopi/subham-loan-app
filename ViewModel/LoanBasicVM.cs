using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanBasicVM
    {
        public int LoanId { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LangInfo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? LoanType { get; set; }
        public int? LoanProg { get; set; }
        public string LoanSource { get; set; }
        public string Refferral { get; set; }
        public string BuyerAgent { get; set; }
        public bool? Fthb { get; set; }
        public bool? Priority { get; set; }
        public DateTime? LeadDate { get; set; }
        public int? TenantId { get; set; }
        public int? MilestoneId { get; set; }
        public string MileStoneName { get; set; }
        public int? FolderId { get; set; }
        public string FolderName { get; set; }
        public int? SubFolderId { get; set; }
        public string SubFolderName { get; set; }
        public int? StageId { get; set; }
        public string StageName { get; set; }
    }
}
