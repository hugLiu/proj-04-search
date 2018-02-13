using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_Range_BP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TermClassID { get; set; }

        [Required]
        [StringLength(255)]
        public string CCCode { get; set; }

        [Required]
        [StringLength(255)]
        public string Term { get; set; }

        public int? OrderIndex { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(100)]
        public string LastUpdatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Sooil_User_Info Sooil_User_Info { get; set; }
    }
}
