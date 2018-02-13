using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep_Post()
        {
            Dep_DepPost = new HashSet<Dep_DepPost>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PostName { get; set; }

        [Required]
        [StringLength(20)]
        public string PostType { get; set; }

        [StringLength(20)]
        public string PostLevelType { get; set; }

        [StringLength(20)]
        public string PostEngageType { get; set; }

        public int OperatorID { get; set; }

        public DateTime CreateDatetime { get; set; }

        public int IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_DepPost> Dep_DepPost { get; set; }
    }
}
