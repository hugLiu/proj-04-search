using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep_Department()
        {
            Dep_DepPost = new HashSet<Dep_DepPost>();
            Dep_DepUser = new HashSet<Dep_DepUser>();
            Dep_DepHistory = new HashSet<Dep_DepHistory>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string DepHID { get; set; }

        public int Ord { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [StringLength(20)]
        public string DepType { get; set; }

        [StringLength(20)]
        public string ExamineType { get; set; }

        public int IsActive { get; set; }

        public int IsDisabled { get; set; }

        public int IsDeleted { get; set; }

        public DateTime CreateDatetime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_DepPost> Dep_DepPost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_DepUser> Dep_DepUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_DepHistory> Dep_DepHistory { get; set; }
    }
}
