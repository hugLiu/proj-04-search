using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Note
    {
        [Key]
        [StringLength(50)]
        public string note_id { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string dataItem { get; set; }

        [StringLength(50)]
        public string noteTitle { get; set; }

        [Column(TypeName = "text")]
        public string noteContent { get; set; }

        public DateTime? noteDate { get; set; }

        public DateTime? sysCreateDate { get; set; }

        public bool? isInvalid { get; set; }

        public long? orderIndex { get; set; }

        [StringLength(200)]
        public string remark { get; set; }
    }
}
