using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ploy.Model
{
    [Table("sysUserInfo")]
    public class sysUserInfo
    {
        [Key]
        public int Id { get; set; }

        public string sUserName { get; set; }
    }
}
