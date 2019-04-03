using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ploy.Model
{
    [Table("sysRoleModuleRelationShip")]
    public class sysRoleModuleRelationShip
    {
        [Key]
        public int Id { get; set; }

        public int iRoleId { get; set; }

        public int iModule { get; set; }
    }
}
