using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HapusPlant.Client.Controllers
{
    public class BaseController : Controller
    {
        public UserDTO? userData => HttpContext.Items["UserData"] as UserDTO;
    }
}