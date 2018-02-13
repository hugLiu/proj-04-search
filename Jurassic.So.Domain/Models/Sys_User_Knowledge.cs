using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_User_Knowledge
    {
        [Key]
        [StringLength(50)]
        public string k_id { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(200)]
        public string dataType { get; set; }

        [StringLength(200)]
        public string sysStandardName { get; set; }

        [StringLength(200)]
        public string selfAliasName { get; set; }

        [StringLength(200)]
        public string keyword { get; set; }

        public DateTime? actionDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
