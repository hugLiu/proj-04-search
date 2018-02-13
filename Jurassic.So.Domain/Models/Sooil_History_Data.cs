using Jurassic.AppCenter;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Jurassic.So.Domain.Models
{
    public partial class Sooil_History_Data:IId<int>
    {
        [Key]
        public int Id { get; set; }

        public Guid HistoryId { get; set; }

        public string HistoryData { get; set; }
    }
}
