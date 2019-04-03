using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ploy.Model
{
    [Table("sysRole")]
    public class sysRole
    {
        public int Id { get; set; }

        public string sRoleName { get; set; }

        public int iSort { get; set; }
        
        public bool bUsable { get; set; }
    }
}
