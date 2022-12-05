using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblAuditLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? LoginDate { get; set; }
        public string Ip { get; set; }
    }
}
