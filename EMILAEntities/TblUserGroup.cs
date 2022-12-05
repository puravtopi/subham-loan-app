using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblUserGroup
    {
        public TblUserGroup()
        {
            TblTenantUserPersonas = new HashSet<TblTenantUserPersona>();
            TblUserPersonas = new HashSet<TblUserPersona>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<TblTenantUserPersona> TblTenantUserPersonas { get; set; }
        public virtual ICollection<TblUserPersona> TblUserPersonas { get; set; }
    }
}
