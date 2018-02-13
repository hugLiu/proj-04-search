 var ison = "";
function submit() {
    var flag = 0;
    $('#message').html("");
    //$(":input[type=text]").each(function(){
    //    var thisValue = this.value;
    //    if(thisValue.replace(/(^\s+)|(\s+$)/g,'')==""){
    //        $('#message').html("提示：检索内容不能为空");
    //        flag = 1;
    //    }
    //})
    //if(flag==1){
    //    return false;
    //}else{
    //    $("#advanue").submit();
    //}
    var isValidate = true;
    $('tr input[type=checkbox]').each(function (index,ele) {
        var isChecked = ele.checked;
        if (!isChecked && isChecked != "checked" &&isChecked != "true") {
            return true;
        }
        var parentTr = $(this).parents("tr");
        var title = $("Span.title", parentTr).text();
        var children = $(".datarange", parentTr).children();

        var emptyFieldName = [];
        children.each(function() {
            if (!$(this).val()||$(this).val().replace(/(^\s+)|(\s+$)/g, '') == "") {
                $('#message').html($('#message').html()  + title + "不能为空<br/>");
                isValidate = false;
            }
        });
    });
    if (isValidate) {
        $("#advanue").submit();
    }
    return false;
}

$(document).ready(function() {
    var service = new AdvanceSearchService();
  
    var metadataDefinitions = service.getAllMetadataOperators();

    var conditions = service.getAdvanceSearchSetting();

    var data = {
        conditions: conditions,
       // defaultCondition: defaultCondition,
        //matchMethods: matchMethods,
        //articleClassiy: articleClassiy,
        //operators: operators,
        message: '',
        metadataDefinitions: metadataDefinitions,
        curcondition:''
    };
    var advanceSearchConfig = new SoVue({
        el: "#advancesearch",
        data: data,
        methods: {
            //add: function() {
            //    this.message = "";
            //    if (this.conditions.length >= 3) {
            //        this.message = "提示：最多可添加3个查询";
            //        return;
            //    }
            //    this.conditions.push($.extend(true, {}, this.defaultCondition));
            //},
            //del: function(index) {
            //    this.message = "";
            //    if (this.conditions.length <= 1) {
            //        this.message = "提示：至少保持一个查询";
            //        return;
            //    }
            //    //删除
            //    this.conditions.splice(index,1);
            //},
            getSelectedMetadata:function(){
                if (!this.curcondition) {
                    return null;
                }
                for (var i = 0; i < this.metadataDefinitions.length; i++) {
                    if (metadataDefinitions[i]._id == this.curcondition) {
                        return metadataDefinitions[i];
                    }
                }
                return null;
            },
            getMetadataDisabledState: function (id) {
                var hasSelected = false;
                for (var i = 0; i < this.conditions.length; i++) {
                    if (conditions[i]._id == id) {
                        hasSelected = true;
                        break;
                    }
                }
                return hasSelected ? "disabled" : "false";
            }
        },
        watch: {
            curcondition:function(newVal,oldVal) {
                this.message = "";
              
                if (!newVal)
                    return;
                this.message = "";
                if (this.conditions.length >= 10) {
                    this.message = "提示：最多可添加6个查询";
                    return;
                }
                var selectedMetadata = this.getSelectedMetadata();
                if (selectedMetadata) {
                    selectedMetadata.isadded = true;
                    this.conditions.push($.extend(true, {}, selectedMetadata));
                    
                }
            },
            conditions:function() {
                $('input[class=datepicker]').datepicker({
                    dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],// 设置控件的星期名称显示
                    firstDay: 1, //设置排在第一列的是星期几，星期天为0，星期一为1，以此类推。
                    changeMonth: true,  //改变月份下拉框
                    changeYear: true,    //改变年份下拉框
                    monthNamesShort: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                    dateFormat: 'yy-mm-dd' //设置日期格式
                });
            }
        }
    });

    
});

