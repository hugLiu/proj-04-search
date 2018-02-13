var SearchResultService = BaseService.extend({
    init: function() {
        this._super();
    },
    /**
     * 获取元数据中的标题
     * @param {} metadataItem 
     * @returns {} 
     */
    getTitle: function(metadataItem) {
        if (!metadataItem || !metadataItem.dc || !metadataItem.dc.title || !metadataItem.dc.title.length)
            return null;
        for (var i = 0; i < metadataItem.dc.title.length; i++) {
            if (metadataItem.dc.title[i].type == 'Formal') {
                return metadataItem.dc.title[i].text;
            }
        }
        return null;
    },
    /**
    * 获取元数据数据项中的数据来源
    * @param {} metadataItem 
    * @returns {} 
    */
    getSourceName: function(metadataItem) {
        if (!metadataItem || !metadataItem.source || !metadataItem.source.name)
            return null;
        return metadataItem.source.name;
    },
    /**
 * 获取元数据数据项中的文件格式
 * @param {} metadataItem 
 * @returns {} 
 */
    getSourceFormat: function (metadataItem) {
        if (!metadataItem || !metadataItem.source || !metadataItem.source.name)
            return null;
        return metadataItem.source.format;
    },
    /**
    * 获取创建作者
    * @param {} metadataItem 
    * @returns {} 
    */
    getCreator: function(metadataItem) {
        if (!metadataItem || !metadataItem.dc || !metadataItem.dc.contributor)
            return null;
        for (var i = 0; i < metadataItem.dc.contributor.length; i++) {
            if (metadataItem.dc.contributor[i].type == 'Author') {
                return metadataItem.dc.contributor[i].name;
            }
        }
        return null;
    },
    /**
    * 获取创建日期
    * @param {} metadataItem 
    * @returns {} 
    */
    getCreatedDate: function(metadataItem) {
        if (!metadataItem || !metadataItem.dc || !metadataItem.dc.date)
            return null;
        for (var i = 0; i < metadataItem.dc.date.length; i++) {
            if (metadataItem.dc.date[i].type == 'Created') {
                return metadataItem.dc.date[i].value;
            }
        }
        return null;
    },
    /**
    * 获取指定的摘要
    * @param {} metadataItem 
    * @returns {} 
    */
    getAbstract: function(metadataItem) {
        if (!metadataItem || !metadataItem.dc || !metadataItem.dc.description)
            return null;
        for (var i = 0; i < metadataItem.dc.description.length; i++) {
            if (metadataItem.dc.description[i].type == 'Abstract') {
                return metadataItem.dc.description[i].text;
            }
        }
        return null;
    },

    convertToResultItem: function(data) {
        var result = {};
        //数据列表
        result.list = { data: [], total: 0 };
        if (data && data.hits) {
            result.list.total = data.hits.total;
            var metadtas = data.hits.hits;
            for (var i = 0; i < metadtas.length; i++) {
                var dataItem = {
                    
                };
                var metadta = metadtas[i]._source;
                dataItem.id = metadta._id;
                dataItem.iiid = metadta.iiid;
                //标题
                dataItem.title = this.getTitle(metadta);
                //来源
                dataItem.source = this.getSourceName(metadta);
                //文件格式
                dataItem.sourceFormat = this.getSourceFormat(metadta);
                //作者
                dataItem.creator = this.getCreator(metadta);
                //创建时间
                dataItem.createdDate = this.getCreatedDate(metadta);
                //摘要
                dataItem.abstract = this.getAbstract(metadta);
                var thumbnail = metadta.thumbnail;
                dataItem.showThumbnail = thumbnail ? true : false;
                dataItem.thumbnail = thumbnail;
                if (thumbnail) {
                    //暂时通过判断不带图片后缀时为图片base64
                    if (thumbnail.indexOf('.png') == - 1 &&
                        thumbnail.indexOf('.jpg') == - 1 &&
                        thumbnail.indexOf('.bmp') == -1)
                        dataItem.thumbnail = 'data:image/png;base64,' + dataItem.thumbnail;
                }
                result.list.data.push(dataItem);
            }
        }

        //聚合赋值
        for (var aggName in data.aggregations) {
            var buckets = data.aggregations[aggName].buckets;
            if (!buckets)
                continue;
            result[aggName] = [];
            for (var j = 0; j < buckets.length; j++) {
                result[aggName].push({
                    "name": buckets[j].key,
                    "value": (j + 1).toString(),
                    "count": buckets[j]["doc_count"],
                    "linkname": buckets[j].key + "(" + buckets[j]["doc_count"] + ")"
                });
            }
        }


        ////成果类型
        //result.productTypes = [];
        //if (data && data.aggregations && data.aggregations.producttype.buckets) {
        //    var buckets = data.aggregations.producttype.buckets;
        //    for (var i = 0; i < buckets.length; i++) {
        //        result.productTypes.push({
        //            "name": buckets[i].key,
        //            "value": (i + 1).toString(),
        //            "count": buckets[i]["doc_count"],
        //            "linkname": buckets[i].key + "(" + buckets[i]["doc_count"] + ")"
        //        });
        //    }
        //}
        ////作者
        //result.creators = [];
        //if (data && data.aggregations && data.aggregations.auditor.buckets) {
        //    var buckets = data.aggregations.auditor.buckets;
        //    for (var i = 0; i < buckets.length; i++) {
        //        result.creators.push({
        //            "name": buckets[i].key,
        //            "value": (i + 1).toString(),
        //            "count": buckets[i]["doc_count"],
        //            "linkname": buckets[i].key + "(" + buckets[i]["doc_count"] + ")"
        //        });
        //    }
        //}
        ////创建时间
        //result.createdDates = [];
        //if (data && data.aggregations && data.aggregations.createdDate.buckets) {
        //    var buckets = data.aggregations.createdDate.buckets;
        //    for (var i = 0; i < buckets.length; i++) {
        //        result.createdDates.push({
        //            "name": buckets[i].key,
        //            "value": (i + 1).toString(),
        //            "count": buckets[i]["doc_count"],
        //            "linkname": buckets[i].key + "(" + buckets[i]["doc_count"] + ")"
        //        });
        //    }
        //}
        return result;
    },
    getAdvanceFieldMatch:function(condition) {
        var matchMethod = condition.accuracy == "M" ? "match" : "term";
        var field = condition.articleClassiy == "title" ? "dc.title.text" : "dc.description.text";
        var fieldKeyword = condition.accuracy == "M" ? field : (field + ".keyword");

        var fieldMatch = {};
        fieldMatch[fieldKeyword] = condition.text;
        var fieldMethod = {};
        fieldMethod[matchMethod] = fieldMatch;
        return fieldMethod;
    },
    getMetadataDefinitions:function() {
        if (!this.metadataDefinitions)
            this.metadataDefinitions = this.esService.getMetadataDefinitions();
        return this.metadataDefinitions;
    },
    getMetadataDefinition:function(field) {
        var metadatas = this.getMetadataDefinitions();
        for (var i = 0; i < metadatas.length; i++) {
            if (metadatas[i].name == field)
                return metadatas[i];
        }
        return null;
    },
    buildAdvanceSearchFilter: function(boolFilter) {
        var advanceConditions = this.getAdvanceConditions();
        var mustFilters = [];
        var mustNotFilters = [];
        //term 加keyword
        for (var i = 0; i < advanceConditions.length; i++) {
            var metadata = this.getMetadataDefinition(advanceConditions[i].field);
            var operator = advanceConditions[i].operator;
            if (operator == "equal") {
                var termFilter = { "term": {} };
                termFilter.term[metadata.mapping + ".keyword"] = advanceConditions[i].matchtext1;
                mustFilters.push(termFilter);
            }
            if (operator == "notequal") {
                var mustNotTerm = { "term": {} };
                mustNotTerm.term[metadata.mapping + ".keyword"] = advanceConditions[i].matchtext1;
                mustNotFilters.push(mustNotTerm);
            }
            if (operator == "contain") {
                var containFilter = { "match": {} };
                containFilter.match[metadata.mapping] = advanceConditions[i].matchtext1;
                mustFilters.push(containFilter);
            }
            if (operator == "notcontain") {
                var notcontainFilter = { "match": {} };
                notcontainFilter.match[metadata.mapping] = advanceConditions[i].matchtext1;
                mustNotFilters.push(notcontainFilter);
            }
            if (operator == ">=") {
                var gteFilter = { "range": {} };
                var matchText = advanceConditions[i].matchtext1;
                if (advanceConditions[i].type == "DateString")
                    matchText = new Date(matchText).format("yyyy-MM-ddThh:mm:ss");
                gteFilter.range[metadata.mapping + ".keyword"] = { "gte": matchText };
                mustFilters.push(gteFilter);
            }
            if (operator == "<=") {
                var lteFilter = { "range": {} };
                var matchText = advanceConditions[i].matchtext1;
                if (advanceConditions[i].type == "DateString")
                    matchText = new Date(matchText).format("yyyy-MM-ddThh:mm:ss");
                lteFilter.range[metadata.mapping + ".keyword"] = { "lte": matchText };
                mustFilters.push(lteFilter);
            }
            if (operator == "between") {
                var betweenFilter = { "range": {} };
                var matchText1 = advanceConditions[i].matchtext1;
                var matchText2 = advanceConditions[i].matchtext2;
                if (advanceConditions[i].type == "DateString") {
                    matchText1 = new Date(matchText1).format("yyyy-MM-ddT00:00:00");
                    matchText2 = new Date(matchText1).format("yyyy-MM-ddT23:59:59");
                }
                betweenFilter.range[metadata.mapping] = {
                    "gte": matchText1,
                    "lte": matchText2
                };
                mustFilters.push(betweenFilter);
            }
        }
        if (mustNotFilters.length > 0) {
            if (!boolFilter.must_not)
                boolFilter.must_not = [];
            boolFilter.must_not = boolFilter.must_not.concat(mustNotFilters);
        }
        if (mustFilters.length > 0) {
            if (!boolFilter.must)
                boolFilter.must = [];
            boolFilter.must = boolFilter.must.concat(mustFilters);
        }
    },

    /**
    * 构造查询条件 https://elasticsearch.cn/book/elasticsearch_definitive_guide_2.x/combining-filters.html
    * @param {} metadataItem 
    * @returns {} 
    */
    buildSearchFilter: function(paramInfos) {
        var searchText = paramInfos.searchText;
        var filter = {"query":{"bool":{}}};
        var postFilters = {"bool":{"must":[]}};
        //查询
        //数据来源精确匹配
        if (paramInfos.sourceName && paramInfos.sourceName !== "全部") {
            if (!filter.query.bool.must)
                filter.query.bool.must = [];
            filter.query.bool.must.push({ "term": { "source.name.keyword": paramInfos.sourceName } });
        }
        //左侧查询条件匹配
        if (paramInfos.dimension) {
            //todo
            var dimensionValue = paramInfos[paramInfos.dimension];
            if (dimensionValue) {
                //if (paramInfos.dimension === "producttype") {
                //    mustParams.push({ "term": { "ep.producttype.keyword": dimensionValue } });
                //}
                //if (paramInfos.dimension === "creator") {
                //    mustParams.push({ "term": { "dc.contributor.name.keyword": dimensionValue } });
                //}
                //左侧分类作为后置查询条件【查询结果不能影响左侧数据】
                //即输入框输入后，只影响查询结果列表，不影响聚合结果
                //post_filter是一个顶层元素，只会对搜索结果进行过滤
                if (paramInfos.dimension === "producttype") {
                    postFilters.bool.must.push({
                        "term": { "ep.producttype.keyword": dimensionValue }
                    });
                }
                if (paramInfos.dimension === "creator") {
                    postFilters.bool.must.push({
                        "term": { "dc.contributor.name.keyword": dimensionValue }
                    });
                }
            }
        }
        //时间范围过滤
        // if (paramInfos.dimension === "recentdatecode") {
        if (paramInfos.recentDateRange && paramInfos.recentDateRange.startDate) {

            var startDate = paramInfos.recentDateRange.startDate.format("yyyy-MM-ddThh:mm:ss");
            var endDate = paramInfos.recentDateRange.endDate.format("yyyy-MM-ddThh:mm:ss");
            //todo:创建时间字段dc.data.value 过滤无效，暂用indexeddate过滤
            //等元数据定义修改后，修改下方的indexeddate
            //时间过滤也作为post_filter过滤
            var dateRangeFilter = {
                "range":
                {
                    "indexeddate": { "gte": startDate, "lte": endDate }
                }
            };
            postFilters.bool.must.push(dateRangeFilter);
        }
        if (postFilters.bool.must.length > 0)
            filter.post_filter = postFilters;
        //文本匹配
       
        var isAdvanceSearch = this.getPageParam("isadvance");
        if (isAdvanceSearch == "true" || isAdvanceSearch == "1") {
            //高级搜索
            this.buildAdvanceSearchFilter(filter.query.bool);
        } else {
            var searchFields = [];
            if (!paramInfos.seachFields) {
                searchFields = ["dc.title.text", "dc.description.text"];
            }
            var boolShouldParams = [];
            for (var i = 0; i < searchFields.length; i++) {
                var paramName = searchFields[i];
                var matchParam = {};
                matchParam[paramName] = searchText;
                boolShouldParams.push({
                    "match": matchParam
                });
            }
            filter.query.bool = { "should": boolShouldParams };
        }

        //聚合
        var aggFields = [];
        var aggFilter = {};
        if (!paramInfos.aggsFields) {
            aggFields = [
                { "name": "productTypes", "field": "ep.producttype" },
                { "name": "creators", "field": "dc.contributor.name" }
            ];
            //{ "name": "createdDates", "field": "dc.date.value" }  时间无法聚合
        }
        for (var j = 0; j < aggFields.length; j++) {
            var aggName = aggFields[j].name;
            aggFilter[aggName] = { "terms": { "field": aggFields[j].field+".keyword" } };
        }
        filter.aggs = aggFilter;

        //分页
        var pageInfo = paramInfos.pageInfo;
        if (pageInfo) {
            filter.from = (pageInfo.pageIndex - 1) * pageInfo.pageSize;
            filter.size = pageInfo.pageSize;
        }
        return filter;
    },
    /**
     * 根据查询条件从ES搜索数据
     * @param {} metadataItem 
     * @returns {} 
     */
    searchResult: function(paramInfos) {
        var filter = this.buildSearchFilter(paramInfos);
        var startTime = new Date().getTime();
        var result = this.esService.searchResult(filter);
        result.executionTime = (new Date().getTime() - startTime);//单位：毫秒
       
        if (result.success) {
            result.data = this.convertToResultItem(result.data);
        }
        return result;
    },
    /**
     * 获取热词
     * @param {} 靠前多少个 
     * @returns {} 
     */
    getTopHotWords:function(succCallback) {
        var searchService = new SearchHistoryService();
        searchService.getTopHotWords(succCallback, 5);
    },

    getAdvanceConfig:function() {
       // articleClassiy=title&accuracy=M&text=q&idOr=and&articleClassiy=title&accuracy=M&text=q&idOr=and&articleClassiy=title&accuracy=M&text=q&idOr=and
        var query = decodeURI(location.search);
        if (query.indexOf('?') == 0)
            query = query.substring(1);
        var paramArray = query.split('&');

        var conditions = [];

        var condition = {};
        for (var i = 0; i < paramArray.length; i++) {
            var conditionArray = paramArray[i].split('=');
            if (conditionArray[0] == "articleClassiy" ||
                conditionArray[0] == "accuracy" ||
                conditionArray[0] == "text" || conditionArray[0] == "idOr") {
                condition[conditionArray[0]] = conditionArray[1];
                if (conditionArray[0] == "idOr") {
                    conditions.push($.extend(true, {}, condition));
                }
            }
            else
                continue;
        }
        return conditions;
    },
    /**
     * 获取高级搜索查询条件
     * @returns {} 
     */
    getAdvanceConditions: function () {
        // articleClassiy=title&accuracy=M&text=q&idOr=and&articleClassiy=title&accuracy=M&text=q&idOr=and&articleClassiy=title&accuracy=M&text=q&idOr=and
        var query = decodeURI(location.search);
        if (query.indexOf('?') == 0)
            query = query.substring(1);
        var paramArray = query.split('&');

       

        var condition = {};
        var params = [];
        for (var i = 0; i < paramArray.length; i++) {
            var conditionArray = paramArray[i].split('=');
            if (conditionArray[0] == "ischecked" ||
                conditionArray[0] == "field" ||
                conditionArray[0] == "operator" ||
                conditionArray[0] == "matchtext1" ||
                conditionArray[0] == "matchtext2") {
                params.push(conditionArray);
            }
            else
                continue;
        }
        var conditions = [];
        for (var j = 0; j < params.length; j++) {
            condition[params[j][0]] = params[j].length>1?params[j][1]:"";
            //如果下一个参数为ischecked,则加入集合
            if (j < params.length - 1) {
                if (params[j+1][0] == "ischecked") {
                    conditions.push($.extend(true, {}, condition));
                    condition = {};
                }
            } else {
                conditions.push($.extend(true, {}, condition));
            }
        }
        return conditions;
    }
});
di.register("SearchResult", SearchResultService);