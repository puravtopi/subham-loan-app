using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class CompanyMasterVM
    {
        public int CompanyId { get; set; }
        public int? TenantId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyNotes { get; set; }
        public string CompanyPcName { get; set; }
        public string CompanyPcEmail { get; set; }
        public string CompanyPcPhone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual TenantMasterVM Tenant { get; set; }
    }
}
