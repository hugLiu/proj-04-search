using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_User_WorkInfo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string Business { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Company { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Post { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(100)]
        public string LastUpdatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Sooil_User_Info Sooil_User_Info { get; set; }
    }
}
