using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class BorrowerVM
    {
        public int BorrowerId { get; set; }
        public int? TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LangInfo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual TenantMasterVM Tenant { get; set; }
    }
}
