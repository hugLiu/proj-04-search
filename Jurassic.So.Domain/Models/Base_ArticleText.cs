namespace Jurassic.So.Domain.Models
{
    public partial class Base_ArticleText
    {
        public int Id { get; set; }

        public string TEXT { get; set; }

        public virtual Base_Article Base_Article { get; set; }
    }
}
