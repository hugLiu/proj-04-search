using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_User_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sooil_User_Info()
        {
            Sooil_Range_BO = new HashSet<Sooil_Range_BO>();
            Sooil_Range_BP = new HashSet<Sooil_Range_BP>();
            Sooil_Range_Source = new HashSet<Sooil_Range_Source>();
            Sooil_User_Education = new HashSet<Sooil_User_Education>();
            Sooil_User_WorkInfo = new HashSet<Sooil_User_WorkInfo>();
        }

        [Key]
        [StringLength(50)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string LoginName { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Sex { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "image")]
        public byte[] PhotoThumbnail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(200)]
        public string BirthPlace { get; set; }

        [StringLength(200)]
        public string LifePlace { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Cellphone { get; set; }

        [StringLength(50)]
        public string UserAccount { get; set; }

        public int? IsInvalid { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public virtual Sooil_Range_BaseSetting Sooil_Range_BaseSetting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sooil_Range_BO> Sooil_Range_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sooil_Range_BP> Sooil_Range_BP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sooil_Range_Source> Sooil_Range_Source { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sooil_User_Education> Sooil_User_Education { get; set; }

        public virtual Sooil_User_Setting Sooil_User_Setting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sooil_User_WorkInfo> Sooil_User_WorkInfo { get; set; }
    }
}
