using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanNeed
    {
        public int LoanneedsId { get; set; }
        public string Customer { get; set; }
        public int? LoanId { get; set; }
        public int? SectionId { get; set; }
        public int? SectionPos { get; set; }
        public int? TaskGroupId { get; set; }
        public int? StatusId { get; set; }
        public int? OwnId { get; set; }
        public string NeedEn { get; set; }
        public string NeedSp { get; set; }
        public bool? Coordinating { get; set; }
        public bool? Tracking { get; set; }
        public bool? _3rdparty { get; set; }
        public string DescEn { get; set; }
        public string DescSp { get; set; }
        public string Sample { get; set; }
        public string Video { get; set; }

        public virtual TblLoanMst Loan { get; set; }
        public virtual TblUserPersona Own { get; set; }
        public virtual TblSection Section { get; set; }
        public virtual TblStatus Status { get; set; }
        public virtual TblTaskGroup TaskGroup { get; set; }
    }
}
