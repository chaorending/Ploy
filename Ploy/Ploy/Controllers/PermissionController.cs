using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ploy.Policys;

namespace Ploy.Controllers
{
    [Authorize("Permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        /// <summary>
        /// 自定义策略参数
        /// </summary>
        PermissionRequirement _requirement;
        public PermissionController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }

        [HttpPost("/api/login")]
        public object GetJwt(string name, string pass)
        {
            string userRoles = "Admin";
            if (name!="Admin")
            {
                userRoles = "NoRole";
            }
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
            claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);

            var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
            return new JsonResult(token);
        }

        [AllowAnonymous]
        [HttpGet("/api/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new
            {
                Status = false,
                Message = "你无权限访问"
            });
        }

        [HttpGet("/api/getValue")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}