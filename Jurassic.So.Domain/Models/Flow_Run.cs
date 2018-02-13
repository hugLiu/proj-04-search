using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Flow_Run
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flow_Run()
        {
            Flow_RunUser = new HashSet<Flow_RunUser>();
        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int InstanceId { get; set; }

        [StringLength(200)]
        public string RunUrl { get; set; }

        public int StepId { get; set; }

        public int Result { get; set; }

        public int? OperatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OperateTime { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Flow_Instance Flow_Instance { get; set; }

        public virtual Flow_Instance Flow_Instance1 { get; set; }

        public virtual Flow_Step Flow_Step { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flow_RunUser> Flow_RunUser { get; set; }
    }
}
