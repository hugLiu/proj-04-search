/**
 * ES 服务 
 * @param {} urlPrefix 
 * @returns {} 
 */
function ESService(urlPrefix) {
    this.urlPrefix = urlPrefix;
}

ESService.prototype.getAggregation = function (field) {
    var aggregationValues = [];
    var params = {
        "size": "0",
        "aggs": {
            "sourcename": {
                "terms": {
                    "field": field+".keyword"
                }
            }
        }
    }

    var url = this.urlPrefix + "/_search";
    $.ajax({
        async: false,
        type: "post",
        url: url,
        //crossDomain: true,
        data: JSON.stringify(params),
        dataType: 'json',
        cache: false,
        beforeSend: function () {

        },
        success: function (data) {
            if (data && data.aggregations && data.aggregations.sourcename.buckets) {
                var buckets = data.aggregations.sourcename.buckets;
                for (var i = 0; i < buckets.length; i++) {
                    aggregationValues.push({ "text": buckets[i].key, "value": (i + 1).toString() });
                }
            }
        },
        error: function () {

        }
    });
    return aggregationValues;
}

/**
 * 获取所有数据来源
 * @returns {} 
 */
ESService.prototype.getSourcenames = function () {
    var allAdapterNames = this.getAggregation("source.name");
    allAdapterNames.splice(0,0,{ "text": '全部', "value": "0" });
    return allAdapterNames;
}


ESService.prototype.searchResult = function (paramInfos)
{
    //var params = {
    //    "query": {
    //        "more_like_this": {
    //            "fields": ["dc.title.text", "dc.description.text"],
    //            "like": searchText,
    //            "min_term_freq": 1,
    //            "max_query_terms": 20
    //        }
    //    },
    //    "aggs": {
    //        "producttype": {
    //            "terms": {
    //                "field": "ep.producttype.keyword"
    //            }
    //        },
    //        "auditor": {
    //            "terms": {
    //                "field": "dc.contributor.name.keyword"
    //            }
    //        },
    //        "createdDate": {
    //            "terms": {
    //                "field": "dc.date.value.keyword"
    //            }
    //        }
    //    }
    //};
    var result = {};
    var url = this.urlPrefix + "/_search";
    $.ajax({
        async: false,
        type: "post",
        url: url,
        //crossDomain: true,
        data: JSON.stringify(paramInfos),
        dataType: 'json',
        cache: false,
        beforeSend: function () {

        },
        success: function (data) {
            result.success=true,
            result.data = data;
        },
        error: function () {
            result.error = arguments;
        }
    });
    return result;
}


/**
 * 获取元数据定义
 */
ESService.prototype.getMetadataDefinitions = function()
{
    //todo 元数据需要改为从ES中查询，待ES元数据定义完成再修改此处，暂时写死
    var metadataDefinitions = [];
    metadataDefinitions.push({ "_id": "587f235c36d71a2910ddd17c", "name": "ProductType", "mapping": "ep.producttype", "title": "成果类型", "type": "String", "format": "", "innertag": "", "groupname": "基础信息集", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235a36d71a2910ddd151", "name": "IndexedDate", "mapping": "indexeddate", "title": "索引时间", "type": "DateString", "format": "", "innertag": "", "groupname": "内部标签", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd155", "name": "SourceType", "mapping": "source.type", "title": "数据源类型", "type": "String", "format": "", "innertag": "", "groupname": "数据源", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd156", "name": "SourceName", "mapping": "source.name", "title": "数据源名称", "type": "String", "format": "", "innertag": "", "groupname": "数据源", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd157", "name": "SourceFormat", "mapping": "source.format", "title": "数据源格式", "type": "String", "format": "", "innertag": "", "groupname": "数据源", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd158", "name": "SourceMedia", "mapping": "source.media", "title": "数据源介质", "type": "String", "format": "", "innertag": "", "groupname": "数据源", "itemorder": 1 });

    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd159", "name": "Title", "mapping": "dc.title.text", "title": "标题", "type": "String", "format": "", "innertag": "", "groupname": "标题", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd15a", "name": "Author", "mapping": "dc.contributor.name", "title": "作者", "type": "String", "format": "", "innertag": "", "groupname": "贡献者", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd15b", "name": "Auditor", "mapping": "dc.contributor.name", "title": "审核者", "type": "String", "format": "", "innertag": "", "groupname": "贡献者", "itemorder": 1 });
    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd15d", "name": "Abstract", "mapping": "dc.description.text", "title": "摘要", "type": "String", "format": "", "innertag": "", "groupname": "描述", "itemorder": 1 });

    metadataDefinitions.push({ "_id": "587f235b36d71a2910ddd161", "name": "CreatedDate", "mapping": "dc.date.value", "title": "创建日期", "type": "DateString", "format": "", "innertag": "", "groupname": "日期", "itemorder": 1 });

    metadataDefinitions.push({ "_id": "587f235c36d71a2910ddd16e", "name": "BoWorkarea", "mapping": "ep.bo.value", "title": "工区", "type": "String", "format": "", "innertag": "", "groupname": "日期", "itemorder": 1 });
    return metadataDefinitions;
}

ESService.prototype.get=function(id) {
    var result = {};
    var url = this.urlPrefix + "/"+id;
    $.ajax({
        async: false,
        type: "get",
        url: url,
        cache: false,
        beforeSend: function () {

        },
        success: function (data) {
            result.success = true,
            result.data = data;
        },
        error: function () {
            result.error = arguments;
        }
    });
    return result;
}


