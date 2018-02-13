using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Keyword_Collection
    {
        [Key]
        [StringLength(50)]
        public string collectionId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(500)]
        public string keyWord { get; set; }

        [StringLength(50)]
        public string searchMode { get; set; }

        public DateTime? collectionDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
