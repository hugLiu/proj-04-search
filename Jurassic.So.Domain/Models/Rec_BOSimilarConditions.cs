using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Rec_BOSimilarConditions
    {
        [Key]
        public Guid GroupID { get; set; }

        [StringLength(100)]
        public string TableName { get; set; }

        [StringLength(100)]
        public string Cloumn { get; set; }

        [StringLength(100)]
        public string Conditions { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        [StringLength(200)]
        public string Desc { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Rec_BOSimilarPara Rec_BOSimilarPara { get; set; }
    }
}
