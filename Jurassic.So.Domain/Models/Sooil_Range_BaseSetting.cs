using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_Range_BaseSetting
    {
        [Key]
        [StringLength(50)]
        public string UserID { get; set; }

        public int? UseBO { get; set; }

        public int? UseBP { get; set; }

        public int? UseSource { get; set; }

        public int? AllowNullBO { get; set; }

        public int? AllowNullBP { get; set; }

        public int? AllowNullSource { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(100)]
        public string LastUpdatedBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Sooil_User_Info Sooil_User_Info { get; set; }
    }
}
