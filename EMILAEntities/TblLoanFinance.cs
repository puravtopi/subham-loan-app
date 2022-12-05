using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanFinance
    {
        public int FinanceId { get; set; }
        public int? LoanId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? MarginBps { get; set; }
        public decimal? Margin { get; set; }
        public decimal? LcBps { get; set; }
        public decimal? Lc { get; set; }
        public decimal? AdjustedMarginBps { get; set; }
        public decimal? AdjustedMargin { get; set; }
        public decimal? LoCommBps { get; set; }
        public decimal? LoComm { get; set; }
        public decimal? Lo2CommBps { get; set; }
        public decimal? Lo2Comm { get; set; }
        public decimal? SmCommBps { get; set; }
        public decimal? SmComm { get; set; }
        public decimal? TotalCommBps { get; set; }
        public decimal? TotalComm { get; set; }
        public decimal? BranchCommBps { get; set; }
        public decimal? BranchComm { get; set; }

        public virtual TblLoanMst Loan { get; set; }
    }
}
