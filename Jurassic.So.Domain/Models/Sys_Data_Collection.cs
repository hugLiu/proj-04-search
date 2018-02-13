using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Data_Collection
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string collectionId { get; set; }
        
        [StringLength(50)]
        public string groupId { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(50)]
        public string account { get; set; }

        [StringLength(2000)]
        public string dataItem { get; set; }

        public DateTime? collectionDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        public virtual Sys_Collection_Group Sys_Collection_Group { get; set; }
    }
}
