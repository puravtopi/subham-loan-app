using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class BranchMasterVM
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress1 { get; set; }
        public string BranchAddress2 { get; set; }
        public string BranchCity { get; set; }
        public string BranchState { get; set; }
        public string BranchCountry { get; set; }
        public string BranchNotes { get; set; }
        public string BranchPcName { get; set; }
        public string BranchPcEmail { get; set; }
        public string BranchPcPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual CompanyMasterVM Company { get; set; }
    }
}
