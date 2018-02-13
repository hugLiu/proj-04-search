using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Comment_Judge
    {
        [Key]
        [StringLength(50)]
        public string judgeId { get; set; }

        [StringLength(50)]
        public string commentId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        public int? judgeType { get; set; }

        public DateTime? judgeDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        public virtual Sys_Comment Sys_Comment { get; set; }
    }
}
