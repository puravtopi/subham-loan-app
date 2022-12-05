using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblBorrowerMst
    {
        public TblBorrowerMst()
        {
            TblLoanMstBorrowers = new HashSet<TblLoanMst>();
            TblLoanMstCoBorrower1s = new HashSet<TblLoanMst>();
            TblLoanMstCoBorrower2s = new HashSet<TblLoanMst>();
            TblLoanMstCoBorrower3s = new HashSet<TblLoanMst>();
            TblLoanMstCoBorrower4s = new HashSet<TblLoanMst>();
        }

        public int BorrowerId { get; set; }
        public int? TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LangInfo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? IsMainBorrower { get; set; }
        public int? LoanId { get; set; }

        public virtual TblTenantMaster Tenant { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMstBorrowers { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMstCoBorrower1s { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMstCoBorrower2s { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMstCoBorrower3s { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMstCoBorrower4s { get; set; }
    }
}
