using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jurassic.So.Application.SearchConfig.Filter
{
    public class RecentDateSideFilter:ISideFilter
    {
        public RecentDateSideFilter()
        {

        }

        public string TemplateName { get; set; }

        public string MappingField { get; set; }

        public int Top { get; set; }

        public string Title { get; set; }

        public FilterPosition FilterPosition { get; set; }
    }
}
