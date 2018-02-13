namespace Jurassic.So.Core.Exceptions
{
    /// <summary>搜索服务异常代码</summary>
    public enum SearchExceptionCodes
    {
        /// <summary>数据转化失败</summary>
        DataConvertFaild,
        /// <summary>ES请求参数不合法 </summary>
        InvalidEsRequest,
        /// <summary> ES处理请求异常 </summary>
        EsProcessFaild,
        /// <summary> Mongo处理请求异常 </summary>
        MongoProcessFaild,
    }
}
