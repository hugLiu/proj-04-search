namespace Jurassic.So.Application.SearchHistory.Dto
{
    public class SearchHistoryModel
    {
        public string id { get; set; }
        public string fdate { get; set; }
        public string ftime { get; set; }

        public string isDelete { get; set; }
        public string word { get; set; }
        public string urlStr { get; set; }
        public int num { get; set; }
    }
}
