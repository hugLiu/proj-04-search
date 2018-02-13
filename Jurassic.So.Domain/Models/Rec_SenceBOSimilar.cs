using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Rec_SenceBOSimilar
    {
        [Key]
        [Column(Order = 0)]
        public Guid SenceID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid GroupID { get; set; }

        [StringLength(100)]
        public string Domain { get; set; }

        [StringLength(200)]
        public string Desc { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Rec_BOSimilarPara Rec_BOSimilarPara { get; set; }

        public virtual Rec_SenceInfo Rec_SenceInfo { get; set; }
    }
}
