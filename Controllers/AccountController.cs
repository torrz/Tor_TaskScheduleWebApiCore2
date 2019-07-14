using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Tor_TaskScheduleWebApiCore2.Classes;

namespace Tor_TaskScheduleWebApiCore2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ConnectionStrings ConfigSettings { get; set; }

        public AccountController(IOptions<ConnectionStrings> settings)
        {
            ConfigSettings = settings.Value;
        }
        // POST api/account/register
        [HttpPost]
        public string Register([FromBody] Account account)
        {
            string sqlRegister =
                @"INSERT INTO account
                 ([username],[password],[nickname],[phone],[role])
                  VALUES
                 (@username,@password,@nickname,@phone,@role)";
            using (var conn = new SqlConnection(ConfigSettings.DefaultConnection))
            {
                var affectedRows = conn.Execute(sqlRegister, new { });
            }
            return "急啊急啊就";
        }
    }
}