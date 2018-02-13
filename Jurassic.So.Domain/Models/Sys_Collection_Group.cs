using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Collection_Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Collection_Group()
        {
            Sys_Data_Collection = new HashSet<Sys_Data_Collection>();
        }

        [Key]
        [StringLength(50)]
        public string groupId { get; set; }

        [StringLength(50)]
        public string parentId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(100)]
        public string groupName { get; set; }

        public DateTime? createDate { get; set; }

        public bool? isSingleFile { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_Data_Collection> Sys_Data_Collection { get; set; }
    }
}
