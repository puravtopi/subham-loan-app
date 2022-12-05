using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class UserMasterVM
    {
        public int UserId { get; set; }
        public int? BranchId { get; set; }
        public int? PersonaId { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserInitials { get; set; }
        public string UserEmail { get; set; }
        public string Phone { get; set; }
        public bool? IsVerified { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool? IsFirstLogin { get; set; }

        public string BranchName { get; set; }
        public string PersonaName { get; set; }
        public int TenantId { get; set; }
        public virtual BranchMasterVM Branch { get; set; }
        public virtual TenantUserPersonaVM Persona { get; set; }
    }
}
