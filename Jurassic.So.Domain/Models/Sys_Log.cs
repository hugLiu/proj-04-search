using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jurassic.So.Domain.Models
{
    public partial class Sys_Log
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ModuleName { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(20)]
        public string ClientIP { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? OpTime { get; set; }

        public int? CatalogId { get; set; }

        public int? ObjectId { get; set; }

        [StringLength(20)]
        public string LogType { get; set; }

        [StringLength(2000)]
        public string Request { get; set; }

        public double? Costs { get; set; }

        [StringLength(2000)]
        public string Message { get; set; }

        [StringLength(50)]
        public string Browser { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BrowserVersion { get; set; }

        [StringLength(50)]
        public string Platform { get; set; }
    }
}
