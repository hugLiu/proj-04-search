using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Flow_Direct
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StepId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NextStepId { get; set; }

        public int OnResult { get; set; }

        public virtual Flow_Step Flow_Step { get; set; }

        public virtual Flow_Step Flow_Step1 { get; set; }
    }
}
