using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using WebApi.Helpers;
using WebApi.Services.Interface;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserPersonaService _service;
        private readonly IMapper _mapper;
       

        public UsersController(IUserPersonaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
          
        }
        [HttpGet]
        public ActionResult<List<UserGroupsVM>> GetAll()
        {
            var accounts = _service.GetAllGroups().ToList();

            var result = _mapper.Map<List<UserGroupsVM>>(accounts);
            //var result = new List<UserGroupsVM>();

            //foreach (var item in accounts)
            //{
            //    result.Add(new UserGroupsVM()
            //    {
            //        GroupId= item.GroupId,
            //        GroupName=item.GroupName,
            //        TblUserPersonas= _mapper.Map<List<UserPersonaVM>>(item.TblUserPersonas)
            //    });
            //}

            return Ok(result);
        }
    }
}
