/// <reference path="../vue.2.3.2.js" />
app.init(function() {
    var searchText = $('#searchContentId').val();
    var dimensionParam = $.util.getPageParam("dimension");
    var strPageIndex = $.util.getPageParam("pageindex");
    var pageIndex = strPageIndex ? parseInt(strPageIndex) : 1;
    var productTypeParam = $.util.getPageParam("producttype");
    var creatorParam = $.util.getPageParam("creator");
    var recentDateParam = $.util.getPageParam("recentdatecode");
    var sourceNameParam = $.util.getPageParam("sourcename"); //$.util.getPageParam("sourcename");
    var recentDates = new RecentDates();
    var recentDateNames = recentDates.Dates;
    var recentDateRange = recentDates.getBeginEndDate({ "code": recentDateParam });
    var dataConfig = {
        searchText: searchText,
        dimension: dimensionParam,
        pageInfo: {
            pageIndex: pageIndex,
            pageSize: 10,
            total: 0,
            totalPage: 1,
            showPageNums:[] //页面显示页码
        },
        //当前成果类型
        producttype: productTypeParam,
        productTypes: [],
        //当前作者
        creator: creatorParam,
        creators: [],
    
        //当前选择的数据来源
        sourceName: sourceNameParam,
        sourceNameList: [],
        searchResultList: [
        ],
        //时间过滤
        recentdatecode: recentDateParam,
        recentDates: recentDateNames,
        //时间范围
        recentDateRange: recentDateRange,
        //左侧热词
        hotWords: [],
        //执行时间
        executionTime:0,
        created: function() {
            //created 这个钩子在实例被创建之后被调用
        }
    };
    var vueConfig = {
        el: "#pagecontent",
        data: dataConfig,
        methods: {
            /**
             * 分页导航
             * @param {} pageIndex 
             * @returns {} 
             */
            navigateToPage: function (pageIndex) {
                if (pageIndex < 1)
                    pageIndex = 1;
                if (pageIndex > this.pageInfo.totalPage)
                    pageIndex = this.pageInfo.totalPage;
                this.pageInfo.pageIndex = pageIndex;
                var params = {};
                params.pageindex = pageIndex;
                var url = $.url.concat(decodeURI(location.href), params);
                location.href = url;
            },
            /**
             * 高亮摘要
             * @param {} abstract 
             * @returns {} 
             */
            hightLightAbstract:function(abstractText) {
                return "<span style=\"color: #666\">摘要：</span>" + highlight(abstractText, this.searchText);
            },
            /**
             * 高亮标题
             * @param {} sourceName 
             * @param {} title 
             * @returns {} 
             */
            hightLightTitle: function (sourceName,title) {
                return "<span style=\"color: #F0C44E\">[" + sourceName + "]</span>" + highlight(title, this.searchText);
            },
            /**
             * 最近**跳转链接
             * @param {} code 
             * @returns {} 
             */
            getRecentDataNavigateUrl:function(code) {
                return $.url.concat(decodeURI(location.href), { "recentdatecode": code });
            }
            
        }
    }
    
   
    var vm = new SoVue(vueConfig);
    var service = di.resolve("SearchResult");
    //todo 整理分页相关运算
    /**
    * 加载列表数据及左侧聚合信息
    */
    var searchResult = service.searchResult(vm);
    if (searchResult.success) {
        vm.searchResultList = searchResult.data.list.data;
        //分页
        vm.pageInfo.total = searchResult.data.list.total;
        if (vm.pageInfo.total == 0)
            vm.pageInfo.totalPage = 0;
        else {
            if (vm.pageInfo.total < vm.pageInfo.pageSize)
                vm.pageInfo.totalPage = 1;
            else {
                if (vm.pageInfo.total % vm.pageInfo.pageSize == 0) {
                    vm.pageInfo.totalPage = vm.pageInfo.total / vm.pageInfo.pageSize;
                } else {
                    vm.pageInfo.totalPage = parseInt(vm.pageInfo.total/vm.pageInfo.pageSize) + 1;
                }
            }
        }
        //分页：查找起始页码和结束页码
        var totalPage = vm.pageInfo.totalPage;
        var curPageIndex = vm.pageInfo.pageIndex;
        var middlePageNum = totalPage - 3;//页面只显示7个页码
        var showPageNums = [];
        if (vm.pageInfo.totalPage <= 7) {
            for (var i = 0; i < vm.pageInfo.totalPage; i++) {
                showPageNums.push(i + 1);
            }
        } else {
            if (curPageIndex <= middlePageNum) {
                var preNum = 0;
                for (var j = curPageIndex - 1; j >= 1; j--) {
                    //当前页前最多取3个数
                    if (preNum < 3) {
                        showPageNums.push(j);
                        preNum++;
                    } else
                        break;
                }
                showPageNums.push(curPageIndex);
                //剩余的页码数
                var leftPageNums = 7 - preNum - 1;
                var afterNum = 0;
                for (var j = curPageIndex + 1; j < totalPage; j++) {
                    if (afterNum < leftPageNums) {
                        showPageNums.push(j);
                        afterNum++;
                    } else
                        break;
                }
            } else {
                var afterNum = 0;
                for (var k = curPageIndex + 1; k < totalPage + 1; k++) {
                    afterNum++;
                    showPageNums.push(k);
                }
                showPageNums.push(curPageIndex);
                //剩余的页码数
                var leftPageNums = 7 - afterNum - 1;
                var preNum = 0;
                for (var m =curPageIndex-1 ; m>=1 ; m--) {
                    if (preNum < leftPageNums) {
                        showPageNums.push(m);
                        preNum++;
                    }
                }
            }
        }
        vm.pageInfo.showPageNums = showPageNums.sort(function(a,b) { return a - b; });

        vm.productTypes = searchResult.data.productTypes;
        vm.creators = searchResult.data.creators;
        vm.executionTime = searchResult.executionTime;
        //vm.createdDates = searchResult.data.createdDates;
    } else {
        alert('加载数据失败');
    }

    /**
   * 加载左侧下方热词【异步加载】
   */
    service.getTopHotWords(function(data) {
        vm.hotWords = data;
    });
});



