using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Rec_BORelationPara
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rec_BORelationPara()
        {
            Rec_SenceBORelation = new HashSet<Rec_SenceBORelation>();
        }

        [Key]
        public Guid GroupID { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        [Required]
        [StringLength(100)]
        public string iBOT { get; set; }

        [Required]
        [StringLength(100)]
        public string oBOT { get; set; }

        [StringLength(100)]
        public string oBOC { get; set; }

        [Required]
        [StringLength(100)]
        public string Direction { get; set; }

        [StringLength(100)]
        public string APIFunction { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rec_SenceBORelation> Rec_SenceBORelation { get; set; }
    }
}
