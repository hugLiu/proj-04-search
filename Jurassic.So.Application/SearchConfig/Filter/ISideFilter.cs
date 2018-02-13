using Jurassic.So.Application.SearchConfig.Filter;

namespace Jurassic.So.Application.SearchConfig
{
    public interface ISideFilter
    {
        string MappingField { get; set; }
        int Top { get; set; }
        string TemplateName { get; set; }

        string Title { get; set; }
        FilterPosition FilterPosition { get; set; }
    }
}