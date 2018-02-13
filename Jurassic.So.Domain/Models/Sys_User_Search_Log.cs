using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_User_Search_Log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_User_Search_Log()
        {
            Sys_User_DataOperate_Log = new HashSet<Sys_User_DataOperate_Log>();
        }

        [Key]
        [StringLength(50)]
        public string logId { get; set; }

        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(500)]
        public string keyword { get; set; }

        [StringLength(50)]
        public string searchMode { get; set; }

        public long? resultCount { get; set; }

        [StringLength(2000)]
        public string relatedWordsForObject { get; set; }

        [StringLength(2000)]
        public string relatedWordsForKeyword { get; set; }

        [Column(TypeName = "xml")]
        public string keywords { get; set; }

        public DateTime? actionDate { get; set; }

        public int? resultEval { get; set; }

        [StringLength(2000)]
        public string impoveOpinion { get; set; }

        public double? executeTime { get; set; }

        [StringLength(50)]
        public string searchSource { get; set; }

        [StringLength(50)]
        public string mac { get; set; }

        [StringLength(50)]
        public string ip { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_User_DataOperate_Log> Sys_User_DataOperate_Log { get; set; }
    }
}
