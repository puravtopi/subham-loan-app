using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanTeam
    {
        public int Id { get; set; }
        public int LoId { get; set; }
        public int LoaId { get; set; }
        public int LmId { get; set; }
        public int SmId { get; set; }
        public int BmId { get; set; }
        public int LoanId { get; set; }
    }
}
