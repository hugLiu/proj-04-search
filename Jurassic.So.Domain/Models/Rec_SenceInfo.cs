using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Rec_SenceInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rec_SenceInfo()
        {
            Rec_SenceBORelation = new HashSet<Rec_SenceBORelation>();
            Rec_SenceBOSimilar = new HashSet<Rec_SenceBOSimilar>();
            Rec_SenceBOSpatial = new HashSet<Rec_SenceBOSpatial>();
        }

        [Key]
        public Guid SenceID { get; set; }

        [StringLength(100)]
        public string SenceName { get; set; }

        [StringLength(200)]
        public string SenceDesc { get; set; }

        [StringLength(200)]
        public string SenceSouces { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(100)]
        public string LastUpdatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rec_SenceBORelation> Rec_SenceBORelation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rec_SenceBOSimilar> Rec_SenceBOSimilar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rec_SenceBOSpatial> Rec_SenceBOSpatial { get; set; }
    }
}
