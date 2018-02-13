using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    //using System.Data.Entity.Spatial;

    public partial class Base_Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Base_Article()
        {
            Base_ArticleExt = new HashSet<Base_ArticleExt>();
            Base_ArticleRelation = new HashSet<Base_ArticleRelation>();
            Base_ArticleRelation1 = new HashSet<Base_ArticleRelation>();
        }

        public int Id { get; set; }

        public int CatalogId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Keywords { get; set; }

        [StringLength(200)]
        public string Abstract { get; set; }

        [StringLength(50)]
        public string UrlTitle { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        public int EditorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EditTime { get; set; }

        public int Ord { get; set; }

        public int State { get; set; }

        public int Clicks { get; set; }

        public virtual Base_Catalog Base_Catalog { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_ArticleExt> Base_ArticleExt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_ArticleRelation> Base_ArticleRelation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Base_ArticleRelation> Base_ArticleRelation1 { get; set; }

        public virtual Base_ArticleText Base_ArticleText { get; set; }
    }
}
