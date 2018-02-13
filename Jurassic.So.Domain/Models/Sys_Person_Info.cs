using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Person_Info
    {
        [Key]
        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "image")]
        public byte[] photo { get; set; }

        [Column(TypeName = "image")]
        public byte[] photoThumbnail { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(150)]
        public string department { get; set; }

        [StringLength(150)]
        public string major { get; set; }

        [StringLength(150)]
        public string job { get; set; }

        [StringLength(150)]
        public string graduateFrom { get; set; }

        [StringLength(150)]
        public string currentProject { get; set; }

        [StringLength(750)]
        public string historyProject { get; set; }

        [StringLength(750)]
        public string historyAchievement { get; set; }

        [StringLength(550)]
        public string interestPoint { get; set; }

        [StringLength(750)]
        public string publishPaper { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
