using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblUserMaster
    {
        public int UserId { get; set; }
        public int? BranchId { get; set; }
        public string UserPassword { get; set; }
        public int? PersonaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserInitials { get; set; }
        public string UserEmail { get; set; }
        public string Phone { get; set; }
        public bool? IsVerified { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool? IsFirstLogin { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual TblBranchMaster Branch { get; set; }
        public virtual TblTenantUserPersona Persona { get; set; }
    }
}
