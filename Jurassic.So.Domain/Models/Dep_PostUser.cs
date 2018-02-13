using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_PostUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepPostId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepUserId { get; set; }

        public int OperationId { get; set; }

        public DateTime CreateDatetime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public virtual Dep_DepPost Dep_DepPost { get; set; }

        public virtual Dep_DepUser Dep_DepUser { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
