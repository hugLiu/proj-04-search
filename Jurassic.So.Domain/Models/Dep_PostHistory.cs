using System;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Dep_PostHistory
    {
        public int Id { get; set; }

        public int DepPostId { get; set; }

        [Required]
        [StringLength(20)]
        public string ChangeType { get; set; }

        [StringLength(128)]
        public string ChangeExplain { get; set; }

        public int DepId { get; set; }

        public int PostId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string ExamineType { get; set; }

        public int IsActive { get; set; }

        public int IsDisabled { get; set; }

        public int IsDeleted { get; set; }

        public DateTime CreateDatetime { get; set; }
    }
}
