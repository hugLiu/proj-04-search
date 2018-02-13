using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_DepPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep_DepPost()
        {
            Dep_PostUser = new HashSet<Dep_PostUser>();
        }

        public int Id { get; set; }

        public int DepId { get; set; }

        public int PostId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? PlanNumber { get; set; }

        [StringLength(20)]
        public string ExamineType { get; set; }

        [StringLength(512)]
        public string Describe { get; set; }

        [StringLength(512)]
        public string Duty { get; set; }

        [StringLength(512)]
        public string Requirement { get; set; }

        public int IsActive { get; set; }

        public int IsDisabled { get; set; }

        public int IsDeleted { get; set; }

        public DateTime CreateDatetime { get; set; }

        public virtual Dep_Department Dep_Department { get; set; }

        public virtual Dep_Post Dep_Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_PostUser> Dep_PostUser { get; set; }
    }
}
