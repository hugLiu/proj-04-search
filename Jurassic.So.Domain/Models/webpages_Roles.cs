using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class webpages_Roles
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
