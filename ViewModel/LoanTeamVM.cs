using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanTeamVM
    {
        public int Id { get; set; }
        public int LoId { get; set; }
        public int LoaId { get; set; }
        public int LmId { get; set; }
        public int SmId { get; set; }
        public int BmId { get; set; }
    }
}
