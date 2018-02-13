using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_ISSUser_Infor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ISSUserID { get; set; }

        [Required]
        [StringLength(128)]
        public string ISSUserKey { get; set; }

        [Required]
        [StringLength(128)]
        public string ISSUserName { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
