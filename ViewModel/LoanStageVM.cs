using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanStageVM
    {
        public int StageId { get; set; }
        public int? SubFolderId { get; set; }
        public string StageName { get; set; }

        public virtual LoanSubFolderVM SubFolder { get; set; }
    }
}
