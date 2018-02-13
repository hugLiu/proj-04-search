using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_DepUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep_DepUser()
        {
            Dep_PostUser = new HashSet<Dep_PostUser>();
        }

        public int Id { get; set; }

        public int DepId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string ExamineType { get; set; }

        [StringLength(20)]
        public string ContractType { get; set; }

        public int? ContractLenght { get; set; }

        [Column(TypeName = "date")]
        public DateTime JoinDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OutDateTime { get; set; }

        public DateTime CreateDatetime { get; set; }

        public int IsSuspension { get; set; }

        public int IsDeleted { get; set; }

        public virtual Dep_Department Dep_Department { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep_PostUser> Dep_PostUser { get; set; }
    }
}
