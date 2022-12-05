using WebApi.Services.Interface;
using System.Linq;
using System.Collections.Generic;
using WebApi.EMILAEntities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services.Implementation
{
    public class UserPersonaService : IUserPersonaService
    {
        #region Feilds
        private readonly emiladbContext _dbContext;
        #endregion

        #region Constructor
        public UserPersonaService(emiladbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region PublicMethod
        public List<TblUserPersona> GetAll()
        {
            List<TblUserPersona> _data = new List<TblUserPersona>();
            try
            {
                _data = _dbContext.TblUserPersonas.ToList();
            }
            catch (System.Exception ex)
            {

            }

            return _data;
        }

        public List<TblUserGroup> GetAllGroups()
        {
            List<TblUserGroup> _data = new List<TblUserGroup>();
            try
            {
                _data = _dbContext.TblUserGroups.Include(x=>x.TblUserPersonas).ToList();
            }
            catch (System.Exception ex)
            {

            }

            return _data;
        }
        #endregion
    }
}
