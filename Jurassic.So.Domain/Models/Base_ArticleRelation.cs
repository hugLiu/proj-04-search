namespace Jurassic.So.Domain.Models
{
    //using System.ComponentModel.DataAnnotations;
    //using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    public partial class Base_ArticleRelation
    {
        public int Id { get; set; }

        public int SourceId { get; set; }

        public int TargetId { get; set; }

        public int RelationType { get; set; }

        public int Ord { get; set; }

        public virtual Base_Article Base_Article { get; set; }

        public virtual Base_Article Base_Article1 { get; set; }
    }
}
