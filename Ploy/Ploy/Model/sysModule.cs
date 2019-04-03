using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ploy.Model
{

    [Table("sysModule")]
    public class sysModule
    {
        [Key]
        public int Id { get; set; }

        public string sMuouleName { get; set; }

        public string sLinkUrl { get; set; }
        
        public int iParentId { get; set; }
    }
}
