/// <reference path="E:\袁权\0517\Jurassic.Sooil.Client\Jurassic.Sooil.WebApp_New\ResourcesFiles/dataset/68777FE0CAF14609376BE1B4307643A4/68777FE0CAF14609376BE1B4307643A4.html" />
/// <reference path="E:\袁权\0517\Jurassic.Sooil.Client\Jurassic.Sooil.WebApp_New\Views/WebApp/Index.cshtml" />


var SemanticsService = "/SemanticsService";
var SegmentAnalyzer = "/SegmentAnalyzer";
var SemanticsRecomment = "/SemanticsRecomment";
var Hierarchy = "/Hierarchy";
var Semantics = "/Semantics";
var SegSynTrans = "/SegSynTrans";
var GetTermInfo = "/GetTermInfo";
var GetParentById = "/GetParentById";
var GetChildByID = "/GetChildByID";

var SearchService = "/SearchService";
var GetMetaData = "/GetMetaData";
var Search = "/Search";
var InfoRecomment = "/InfoRecomment";
var AdvancedSearch = "/AdvancedSearch";
var RelatedSearch = "/TagsMatchSearch";


var BOServiceUrl = "/BOService";
var BOSpatialUrl = "/BOSpatial";
var AssociatedBOURL = "/AssociatedBO";
var BOTreeURL = "/BOTree";
var GetIDbyName = "/GetIDbyName";

//数据服务
var DataService = "/DataService";
var Datasources = "/Datasources";

var token_acc = "";

//能显示的文档格式(小写)------------以后有新增可显示格式需要添加
var FormatEnum = {
    "jpg": "jpg",
    "doc": "doc",
    "docx": "docx",
    "dataset": "dataset",
    "html": "html",
    "htm": "htm",
    "gdb": "gdb",
    "xls": "xls",
    "xlsx": "xlsx",
    "pdf": "pdf",
    "gif": "gif",
    "bmp": "bmp",
    "png": "png",
    "jepg": "jepg",
    "tif": "tif",
    "ico": "ico",
    "txt": "txt",
    "ppt": "ppt",
    "pptx":"pptx"
};

function GetResourcesView_old(id, iiid, title, urlFunction) {
    //Html.Action("Center", "ResourcesView", new {id = "", type = ViewBag.ext, url = ViewBag.url, data = ViewBag.data})
    $.ajax({
        url: "/WebApp/GetDetailedView",
        type: "get",
        data: { iiid: iiid, title: title },
        dataType: "json",
        success: function (data) {
            switch (data.ext) {
                case "html":
                    $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                    break;
                case "pdf":
                    $(".detailedView").removeClass("detailedView").addClass("detailedViewPdf");
                    //$("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                    $("#" + id).html("<iframe src=\"/ResourcesView/Center?id=''&type=" + data.ext + "&url=" + data.url + "&data=" + data.data + "\"  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                    if (urlFunction != null)
                        urlFunction(data.url);
                    break;
                    //case "doc":
                    //    $("#ResourcesView").html("<iframe src=\"/ResourcesView/Center?id=''&type=" + data.ext + "&url=" + data.url + "&data=" + data.data + "\"  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                    //    searchResultViewModel.FileURL(data.url);
                    //    break;
                default:
                    if (data.ext == "doc") {
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                    }
                    if (data.ext == "gdb") {
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewGdb");
                    }
                    $.ajax({
                        url: "/ResourcesView/Center",
                        type: "get",
                        data: { id: data.iiid, type: data.ext, url: data.url, data: data.data },
                        dataType: "html",
                        success: function (result) {
                            if (urlFunction != null)
                                urlFunction(data.url); 
                            $("#" + id).html(result);
                        },
                        error: function (err) {
                            WebAppToastr("error", err.status + " - " + err.statusText);
                        }
                    });
            }
        },
        error: function (err) {
            WebAppToastr("error", err.status + " - " + err.statusText);
        }
    });
}

// ================卢英杰
function GetResourcesView(id, iiid, title) {
    //Html.Action("Center", "ResourcesView", new {id = "", type = ViewBag.ext, url = ViewBag.url, data = ViewBag.data})
    $.ajax({
        url: "/WebApp/GetDetailedView",
        type: "get",
        data: { iiid: iiid, title: title },
        dataType: "json",
        success: function (data) {
            if (data.msg == "ok") {
                switch (data.ext) {
                    case "html":
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        break;
                    case "htm":
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        break;
                    case "pdf":
                        //$("#ResourcesView").html("<iframe src="+data.url+"  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewPdf");
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "doc":
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "docx":
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "xls":
                        //$(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "xlsx":
                        //$(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    default:
                        if (data.ext == "gdb") {
                            $(".detailedView").removeClass("detailedView").addClass("detailedViewGdb");
                        }
                        $.ajax({
                            url: "/ResourcesView/Center",
                            type: "get",
                            data: { id: data.iiid, type: data.ext, url: data.url, data: data.data },
                            dataType: "html",
                            success: function (result) {
                                //if (urlFunction != null)
                                //    urlFunction(data.url);
                                $("#" + id).html(result);
                            },
                            error: function (err) {
                                WebAppToastr("error", err.status + " - " + err.statusText);
                            }
                        });
                }
            } else {
                WebAppToastr("warning", data.msg);
                return;
            }

        },
        error: function (err) {
            WebAppToastr("error", err.status + " - " + err.statusText);
        }
    });
};

// ================yuanquan

function GetResourcesView_New(id, iiid, title, eventType) {
    //Html.Action("Center", "ResourcesView", new {id = "", type = ViewBag.ext, url = ViewBag.url, data = ViewBag.data})  
    $.ajax({
        url: "/WebApp/GetDetailedView",
        type: "get",
        data: { iiid: iiid, title: title, eventType: eventType },
        dataType: "json",
        success: function (data) {
            if (data.msg == "ok") {
                spinner.stop();
                switch (data.ext) {
                    case "html":
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        break;
                    case "htm":
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        break;
                    case "pdf":
                        //$("#ResourcesView").html("<iframe src="+data.url+"  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewPdf");
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "doc":
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "docx":
                        $(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "xls":
                        //$(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "xlsx":
                        //$(".detailedView").removeClass("detailedView").addClass("detailedViewDoc");
                        $("#" + id).html("<iframe src=" + data.url + "  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes'></iframe>");
                        //if (urlFunction != null)
                        //    urlFunction(data.url);
                        break;
                    case "dataset":
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        break;                  
                    case "txt":
                        $("#" + id).html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data.data);
                        break;
                    case "ppt":
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv'  frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        break;
                    case "pptx":
                        $("#" + id).html("<iframe src=" + data.url + " id='iframeDiv' frameborder='no' border='0' marginwidth='0' marginheight='0' width='100%' height='620px' scrolling='no' allowtransparency='yes' onLoad=\"reinitIframeEND();\"></iframe>");
                        break;
                    default:
                        if (data.ext == "gdb") {
                            $(".detailedView").removeClass("detailedView").addClass("detailedViewGdb");
                        }
                        $.ajax({
                            url: "/ResourcesView/Center",
                            type: "get",
                            data: { id: data.iiid, type: data.ext, url: data.url },
                            dataType: "html",
                            success: function (result) {
                                $("#" + id).html(result);
                            },
                            error: function (err) {
                                WebAppToastr("error", err.status + " - " + err.statusText);
                                spinner.stop();
                               
                            }
                        });
                }
            } else {
                WebAppToastr("warning", data.msg);
                spinner.stop();           
                return;
            }
        },
        error: function (err) {
            WebAppToastr("error", err.status + " - " + err.statusText);
            spinner.stop();
            
        }
    });
};

function reinitIframeEND() {
    try {
        var ifm = document.getElementById("iframeDiv");
        var subWeb = document.frames ? document.frames["iframeDiv"].document : ifm.contentDocument;
        if (ifm != null && subWeb != null) {
            ifm.height = subWeb.body.scrollHeight;
            ifm.width = subWeb.body.scrollWidth;
        }
    } catch (ex) { }
};
