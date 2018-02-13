using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Base_Catalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Base_Catalog()
        {
            Base_Article = new HashSet<Base_Article>();
            Base_Catalog1 = new HashSet<Base_Catalog>();
            Base_CatalogExt = new HashSet<Base_CatalogExt>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Abstract { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        [StringLength(200)]
        public string IconLocation { get; set; }

        [StringLength(20)]
        public string Language { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EditTime { get; set; }

        public int Ord { get; set; }

        public int State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_Article> Base_Article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_Catalog> Base_Catalog1 { get; set; }

        public virtual Base_Catalog Base_Catalog2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_CatalogExt> Base_CatalogExt { get; set; }
    }
}
