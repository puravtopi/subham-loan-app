using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class UserPersonaVM
    {

        public int PersonaId { get; set; }
        public int GroupId { get; set; }
        public string PersonaName { get; set; }
        public string PersonaShortname { get; set; }
        public virtual UserGroupsVM Group { get; set; }


    }
}
