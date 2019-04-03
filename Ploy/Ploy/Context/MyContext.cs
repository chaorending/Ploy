using Microsoft.EntityFrameworkCore;
using Ploy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ploy.Context
{
    public class MyContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //注入Sql链接字符串
            optionsBuilder.UseSqlServer(@"Server=.;Database=Site;Trusted_Connection=True;");
        }
        public DbSet<sysUserRoleRelationShip> sysUserRoleRelationShips { get; set; }

        public DbSet<sysRoleModuleRelationShip> sysRoleModuleRelationShips { get; set; }

        public DbSet<sysRole> sysRoles { get; set; }

        public DbSet<sysUserInfo> sysUserInfos { get; set; }

        public DbSet<sysModule> sysModules { get; set; }
    }
}
