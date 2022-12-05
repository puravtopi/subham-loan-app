using System.Collections.Generic;
using WebApi.EMILAEntities;


namespace WebApi.Services.Interface
{
    public interface IUserPersonaService
    {
        public List<TblUserPersona> GetAll();

        public List<TblUserGroup> GetAllGroups();
    }
}
