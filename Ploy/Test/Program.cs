using Ploy.Context;
using Ploy.Model;
using System;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MyContext context = new MyContext();
           sysUserInfo sysUser=context.sysUserInfos.FirstOrDefault();
        }
    }
}
