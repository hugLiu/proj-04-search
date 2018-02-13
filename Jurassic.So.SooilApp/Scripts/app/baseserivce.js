var BaseService = Class.extend({
    init:function() {
        var esServiceUrlPrefix = "http://192.168.1.152:9222/test/Metadata";
        this.urlPrefix = esServiceUrlPrefix;
        this.esService = new ESService(esServiceUrlPrefix);
    },
    getPageParam: function (paramname) {
        if (!paramname)
            return '';
        return decodeURI($.util.pageParams[paramname.toLowerCase()]);
    }
});