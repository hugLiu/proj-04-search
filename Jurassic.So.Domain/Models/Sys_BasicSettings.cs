using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_BasicSettings
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [Required]
        [StringLength(50)]
        public string PreferencesItemName { get; set; }

        public int Sequence { get; set; }

        [Required]
        [StringLength(128)]
        public string Value1 { get; set; }

        [StringLength(128)]
        public string Value2 { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public DateTime? EffectivedStartDateTime { get; set; }

        public DateTime? EffectivedEndDateTime { get; set; }

        public DateTime CreateDatetime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
