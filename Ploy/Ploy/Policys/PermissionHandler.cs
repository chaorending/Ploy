using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ploy.Policys
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        public List<PermissionItem> PermissionItems { get; set; }

        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleModulePermissionServices"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            this.PermissionItems = new List<PermissionItem>() { new PermissionItem { Role = "Admin", Url = "/api/getValue" } };

            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;

            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();

            //判断请求是否停止
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();

            //暂时我也不知道这段代码是干嘛用的
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }

            //判断请求是否拥有凭据，即有没有登录
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    httpContext.User = result.Principal;

                    // 获取当前用户的角色信息
                    var currentUserRoles = (from item in httpContext.User.Claims
                                            where item.Type == requirement.ClaimType
                                            select item.Value).ToList();
                    int count = this.PermissionItems.Where(w => currentUserRoles.Contains(w.Role) && w.Url.ToLower()==questUrl).Count();
                    if (this.PermissionItems.Where(w => currentUserRoles.Contains(w.Role) && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        httpContext.Response.Redirect(requirement.DeniedAction);
                    }
                    return;
                }
            }
            //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
            if (!questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST")
               || !httpContext.Request.HasFormContentType))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
