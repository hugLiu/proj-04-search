using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_User_DataOperate_Log
    {
        [Key]
        [StringLength(50)]
        public string logId { get; set; }

        [StringLength(50)]
        public string parentLogId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(200)]
        public string dataItem { get; set; }

        [StringLength(50)]
        public string action { get; set; }

        [StringLength(200)]
        public string keyword { get; set; }

        public DateTime? actionDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        public virtual Sys_User_Search_Log Sys_User_Search_Log { get; set; }
    }
}
