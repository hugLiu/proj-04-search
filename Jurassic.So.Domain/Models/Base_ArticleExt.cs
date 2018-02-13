using System.ComponentModel.DataAnnotations;

namespace Jurassic.So.Domain.Models
{
    //using System.Data.Entity.Spatial;

    public partial class Base_ArticleExt
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int CatlogExtId { get; set; }

        [StringLength(200)]
        public string Value { get; set; }

        public virtual Base_Article Base_Article { get; set; }

        public virtual Base_CatalogExt Base_CatalogExt { get; set; }
    }
}
