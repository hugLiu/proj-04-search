using System.Collections.Generic;
using Jurassic.So.Application.SearchConfig.Config;

namespace Jurassic.So.Application.SearchConfig
{
    public interface ISearchConfigService
    {
        SearchResultConfig GetSearchResultConfig();
        List<HtmlSegmentTemplate> LoadAllHtmlTemplate();
    }
}