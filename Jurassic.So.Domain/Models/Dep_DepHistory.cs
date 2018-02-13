namespace Jurassic.So.Domain.Models
{
    public partial class Dep_DepHistory
    {
        public int Id { get; set; }

        public int DepId { get; set; }

        public virtual Dep_Department Dep_Department { get; set; }
    }
}
