using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeApi.Controllers
{
    
    [EnableCors(origins:"*", headers:"*",methods:"*")]
    public class HomeController:ApiController
    {
        [HttpGet]
        [Route("api/Home/Get")]
        [AllowAnonymous]
        public string GetId()
        {
            return Convert.ToString("fgd");
        }
    }
}