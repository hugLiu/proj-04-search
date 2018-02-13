using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Rec_BOSpatialPara
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rec_BOSpatialPara()
        {
            Rec_SenceBOSpatial = new HashSet<Rec_SenceBOSpatial>();
        }

        [Key]
        public Guid GroupID { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        [Required]
        [StringLength(100)]
        public string iBOT { get; set; }

        public long Distance { get; set; }

        [Required]
        [StringLength(100)]
        public string oBOT { get; set; }

        [StringLength(100)]
        public string oBOC { get; set; }

        [StringLength(100)]
        public string LBOT { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rec_SenceBOSpatial> Rec_SenceBOSpatial { get; set; }
    }
}
