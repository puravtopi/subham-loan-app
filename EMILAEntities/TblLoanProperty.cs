using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanProperty
    {
        public int PropertyId { get; set; }
        public int? LoanId { get; set; }
        public string PropertyInfo { get; set; }
        public string ContingencyInfo { get; set; }
        public string CriticalNote { get; set; }
        public string AppraisalNotes { get; set; }
        public string PropertyZip { get; set; }
        public string AppraisalStatus { get; set; }
        public string CdStatus { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createddate { get; set; }

        public virtual TblLoanMst Loan { get; set; }
    }
}
