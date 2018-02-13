
var SearchBarService = BaseService.extend({
    init: function () {
        this._super();
    },
    /**
   * 获取搜索框下拉数据[获取推荐词]
   * @param {successCallback} 调用成功回调方法
   * @param {topCount} 靠前多少个
   * @returns {} 
   */
    getRecommandWords: function (successCallback,inputWord) {
        $.ajax({
            url: "/TypeAhead/GetTypeAheadJson",
            dataType: "json",
            data: { "query": inputWord },
            success: function (data) {
                successCallback(data);
            }
        });
    },
    /**
   * 获取热词
   * @param {successCallback} 调用成功回调方法
   * @param {topCount} 靠前多少个
   * @returns {} 
   */
    getTopHotWords: function (successCallback) {
        //热词 【异步】
        var searchHistorySerivce = new SearchHistoryService();
        searchHistorySerivce.getTopHotWords(successCallback, 6);
    },
    /**
     * 获取数据来源
     * @returns {} 
     */
    getSourcenames:function() {
        return this.esService.getSourcenames();
    }
});