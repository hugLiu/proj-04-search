using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Comment()
        {
            Sys_Comment_Judge = new HashSet<Sys_Comment_Judge>();
        }

        [Key]
        [StringLength(50)]
        public string commentId { get; set; }

        [StringLength(50)]
        public string parentId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string dataItem { get; set; }

        [Column(TypeName = "text")]
        public string comment { get; set; }

        public DateTime? commentDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_Comment_Judge> Sys_Comment_Judge { get; set; }
    }
}
