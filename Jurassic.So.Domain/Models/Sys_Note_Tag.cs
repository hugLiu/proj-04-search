using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Note_Tag
    {
        [Key]
        [StringLength(50)]
        public string tag_id { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string tagContent { get; set; }

        public DateTime? tagDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
