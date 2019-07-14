using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private const string HAS_ACOUNT_INFO= "hasAccountInfo";


        public AccountController(IOptions<ConnectionStrings> settings)
        {
            ConfigSettings = settings.Value;
        }

        [HttpPost]
        public IActionResult Check([FromBody] Account account)
        {
            if (HasAccount(account))
                return Ok(HAS_ACOUNT_INFO);

            return Ok();
        }
        [HttpPost]
        public IActionResult Login([FromBody] Account account)
        {
            account.Password = Encrypt(account.Password);
            string sql = @"SELECT * FROM account WHERE username=@username AND password=@password;";
            using (var conn = new SqlConnection(ConfigSettings.DefaultConnection))
            {
                var foundUser = conn.QueryFirstOrDefault<Account>(sql,account);
                if (foundUser != null)
                {
                    return Ok();
                }
                else
                {
                    return Ok("notFound");
                }
            }
        }

        // POST api/account/register
        [HttpPost]
        public IActionResult Register([FromBody] Account account)
        {
            if (HasAccount(account))
            {
                return Ok(HAS_ACOUNT_INFO);
            }
            account.Password = Encrypt(account.Password);
            string sqlRegister =
                @"INSERT INTO account
                 ([username],[password],[nickname],[phone],[role])
                  VALUES
                 (@username,@password,@nickname,@phone,@role)";
            using (var conn = new SqlConnection(ConfigSettings.DefaultConnection))
            {
                var affectedRows = conn.Execute(sqlRegister,
                    account);
            }
            return Ok();
        }

        // POST api/account/delete
        [HttpPost]
        public IActionResult Delete([FromBody] JArray ids)
        {
            return Ok();
        }


        /// <summary>
        /// MD5编码
        /// </summary>
        /// <param name="str_raw"></param>
        /// <returns></returns>
        private static string Encrypt(string str_raw)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str_raw));

            string encryptStr = "";
            foreach (byte b in bytes)
            {
                encryptStr += b.ToString("x2");
            }
            return encryptStr;
        }
        /// <summary>
        /// 判断是否存在相同的用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private bool HasAccount(Account account)
        {
            string sql = @"SELECT 1 AS count FROM account WHERE ";
            DynamicParameters parameter = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(account.Username))
            {
                sql += @" [username]=@username ";
                parameter.Add("@username", account.Username);
            }
            if (!string.IsNullOrWhiteSpace(account.Phone))
            {
                if (parameter.ParameterNames.Count() > 0)
                {
                    sql += " OR ";
                }
                sql += @" [phone]=@phone ";
                parameter.Add("@phone", account.Phone);
            }

            if (parameter.ParameterNames.Count() == 0)
            {
                throw new Exception("传入检查的参数");
            }
            using (var conn = new SqlConnection(ConfigSettings.DefaultConnection))
            {
                var result = conn.QueryFirstOrDefault(sql, parameter);
                if (result != null)//没有行不会查出东西，即为null
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}