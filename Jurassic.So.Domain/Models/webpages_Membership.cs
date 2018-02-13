using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class webpages_Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(128)]
        public string ConfirmationToken { get; set; }

        public bool? IsConfirmed { get; set; }

        public DateTime? LastPasswordFailureDate { get; set; }

        public int PasswordFailuresSinceLastSuccess { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        public DateTime? PasswordChangedDate { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }

        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }
    }
}
