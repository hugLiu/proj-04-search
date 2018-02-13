using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Data_Rating
    {
        [Key]
        [StringLength(50)]
        public string ratingId { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string dataItem { get; set; }

        public byte? level { get; set; }

        public DateTime? ratingDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
