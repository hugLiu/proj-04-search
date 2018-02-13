using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_User_Setting
    {
        [Key]
        [StringLength(50)]
        public string UserID { get; set; }

        public int? IsInfo { get; set; }

        public int? Count { get; set; }

        public int? IsPreview { get; set; }

        public int? ShowHistory { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        [StringLength(100)]
        public string SearchMethod { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(100)]
        public string LastUpdatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Sooil_User_Info Sooil_User_Info { get; set; }
    }
}
