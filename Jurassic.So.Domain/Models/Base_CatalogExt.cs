using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    public partial class Base_CatalogExt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Base_CatalogExt()
        {
            Base_ArticleExt = new HashSet<Base_ArticleExt>();
        }

        public int Id { get; set; }

        public int CatalogId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int DataType { get; set; }

        [StringLength(200)]
        public string DefaultValue { get; set; }

        public int DataSourceType { get; set; }

        public string DataSource { get; set; }

        public int Ord { get; set; }

        public int State { get; set; }

        public bool? AllowNull { get; set; }

        public int? MaxLength { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_ArticleExt> Base_ArticleExt { get; set; }

        public virtual Base_Catalog Base_Catalog { get; set; }
    }
}
