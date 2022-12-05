using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class TenantMasterVM
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public bool? IsTrial { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
            

    }
}
