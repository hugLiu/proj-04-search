using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Flow_Instance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flow_Instance()
        {
            Flow_Run = new HashSet<Flow_Run>();
            Flow_Run1 = new HashSet<Flow_Run>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int GraphId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }

        public int CreaterId { get; set; }

        public virtual Flow_Graph Flow_Graph { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flow_Run> Flow_Run { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flow_Run> Flow_Run1 { get; set; }
    }
}
