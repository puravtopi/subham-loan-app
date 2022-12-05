using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblErrorLog
    {
        public int Id { get; set; }
        public string FunctonName { get; set; }
        public string Desc { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
