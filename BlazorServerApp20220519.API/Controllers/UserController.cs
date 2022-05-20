using BlazorServerApp20220519.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<UserModel> GetUsers()
        {
            return Enumerable.Range(1, 10).Select(x => new UserModel
            {
                userId = x,
                userName = Guid.NewGuid().ToString()
            }).ToList();
        }

        [HttpPost]
        public object AddUser()
        {
            var lst = Enumerable.Range(1, 10).Select(x => new UserModel
            {
                userId = x,
                userName = Guid.NewGuid().ToString()
            }).ToList();
            string res = lst.ToJson();
            string res2 = JsonConvert.SerializeObject(lst);
            
            return new { DevMethod = res, DirectMethod = res2  };
        }
    }
}
