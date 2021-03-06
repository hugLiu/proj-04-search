﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Jurassic.So.Core.Exceptions;
using Nest;

namespace Jurassic.So.Search.ES
{
    /// <summary>
    /// ES数据访问
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ESAccess<T> where T : class
    {
        /// <summary>
        /// ES客户端
        /// </summary>
        private static ElasticClient Client { get; set; }
        private static string EsUri = ConfigurationManager.AppSettings["es_uri"];
        private static string EsType = ConfigurationManager.AppSettings["es_type"];
        private static string EsIndex = ConfigurationManager.AppSettings["es_index"];

        /// <summary>
        /// 构造函数
        /// </summary>
        public ESAccess()
        {
            Client = EsClient.Create(EsUri);
        }
        public ESAccess(string type)
        {
            EsType = type;
            Client = EsClient.Create(EsUri);
        }
        #region 插入索引
        /// <summary>
        /// 索引一个文档
        /// </summary>
        /// <param name="doc">文档</param>
        /// <returns></returns>
        public IIndexResponse IndexOne(T doc)
        {
            return Client.Index(doc);
        }
        /// <summary>
        /// 索引一个文档（异步）
        /// </summary>
        /// <param name="doc">文档</param>
        /// <returns></returns>
        public async Task<IIndexResponse> IndexOneAsync(T doc)
        {
            return await Client.IndexAsync(doc);
        }
        #endregion

        #region 删除索引
        /// <summary>
        /// 删除索引，支持批量
        /// </summary>
        /// <param name="query">查询符合条件的索引</param>
        /// <returns></returns>
        public IDeleteByQueryResponse DeleteByQuery(QueryContainer query)
        {
            return Client.DeleteByQuery(new DeleteByQueryDescriptor<T>(EsIndex));
        }
        /// <summary>
        /// 删除索引，支持批量 （异步）
        /// </summary>
        /// <param name="query">查询符合条件的索引</param>
        /// <returns></returns>
        public async Task<IDeleteByQueryResponse> DeleteByQueryAsync(QueryContainer query)
        {
            return await Client.DeleteByQueryAsync(new DeleteByQueryDescriptor<T>(EsIndex));
        }
        #endregion

        #region 更新索引
        /// <summary>
        /// 更新全部字段（All Fields）
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public IUpdateResponse<T> UpdateAFields(IUpdateRequest<T, T> request)
        {
            return Client.Update<T, T>(request);
        }
        /// <summary>
        /// 更新全部字段（All Fields）（异步）
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public async Task<IUpdateResponse<T>> UpdateAFieldsAsync(IUpdateRequest<T, T> request)
        {
            return await Client.UpdateAsync<T, T>(request);
        }
        /// <summary>
        /// 更新部分字段（Patial Fields）
        /// </summary>
        /// <typeparam name="T2">包含部分字段的实体类</typeparam>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public IUpdateResponse<T> UpdatePFields<T2>(IUpdateRequest<T, T2> request) where T2 : class
        {
            return Client.Update(request);
        }
        /// <summary>
        /// 更新部分字段（Patial Fields）（异步）
        /// </summary>
        /// <typeparam name="T2">包含部分字段的实体类</typeparam>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public async Task<IUpdateResponse<T>> UpdatePFieldsAsync<T2>(IUpdateRequest<T, T2> request) where T2 : class
        {
            return await Client.UpdateAsync(request);
        }
        /// <summary>
        /// 更新部分字段（Patial Fields）T2=》object
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public IUpdateResponse<T> UpdatePFields(IUpdateRequest<T, object> request)
        {
            return Client.Update(request);
        }
        /// <summary>
        /// 更新部分字段（Patial Fields）T2=》object（异步）
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns></returns>
        public async Task<IUpdateResponse<T>> UpdatePFieldsAsync(IUpdateRequest<T, object> request)
        {
            return await Client.UpdateAsync(request);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据文档唯一ID查询指定文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns></returns>
        public IGetResponse<T> Get(string id)
        {
            return Client.Get(new DocumentPath<T>(id));
        }
        /// <summary>
        /// 根据文档唯一ID查询指定文档(异步)
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns></returns>
        public async Task<IGetResponse<T>> GetAsync(string id)
        {
            return await Client.GetAsync(new DocumentPath<T>(id));
        }
        /// <summary>
        /// 根据多个id返回指定的文档列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public IMultiGetResponse MultiGet(IEnumerable<string> ids)
        {
            return Client.MultiGet(m => m.GetMany<T>(ids));
        }
        /// <summary>
        /// 根据多个id返回指定的文档列表（异步）
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public async Task<IMultiGetResponse> MultiGetAsync(IEnumerable<string> ids)
        {
            return await Client.MultiGetAsync(m => m.GetMany<T>(ids));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序规则</param>
        /// <param name="fields">返回字段</param>
        /// <param name="aggs">聚合条件</param>
        /// <param name="from">记录开始</param>
        /// <param name="size">记录数</param>
        /// <returns></returns>
        public ISearchResponse<T> PagingQuery(QueryContainer query, SortDescriptor<T> sort, SourceFilterDescriptor<T> fields,
            AggregationContainerDescriptor<T> aggs, int from = 0, int size = 10)
        {
            ISearchResponse<T> result = new SearchResponse<T>();
            try
            {
                result = Client.Search<T>(
                             s => BuildSearchDescriptor(query, sort, fields, aggs, from, size));
                if (!result.IsValid)
                {
                    SearchExceptionCodes.InvalidEsRequest
                        .ThrowUserFriendly(result.ServerError.ToString(), result.OriginalException.Message);
                }
            }
            catch (Exception ex)
            {
                SearchExceptionCodes.EsProcessFaild.ThrowUserFriendly(ex.Message, "ES 服务器处理异常");
            }
            return result;
        }
        /// <summary>
        /// 分页查询（异步）
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序规则</param>
        /// <param name="fields">返回字段</param>
        /// <param name="aggs">聚合条件</param>
        /// <param name="from">记录开始</param>
        /// <param name="size">记录数</param>
        /// <returns></returns>
        public async Task<ISearchResponse<T>> PagingQueryAsync(QueryContainer query, SortDescriptor<T> sort, SourceFilterDescriptor<T> fields,
         AggregationContainerDescriptor<T> aggs, int from = 0, int size = 10)
        {
            ISearchResponse<T> result = new SearchResponse<T>();
            try
            {
                result = await Client.SearchAsync<T>(
                    s => BuildSearchDescriptor(query, sort, fields, aggs, from, size));
                if (!result.IsValid)
                {
                    
                    SearchExceptionCodes.InvalidEsRequest
                        .ThrowUserFriendly(result.ServerError.ToString(), result.OriginalException.Message);
                }
            }
            catch (Exception ex)
            {
                SearchExceptionCodes.EsProcessFaild.ThrowUserFriendly(ex.Message, "ES 服务器处理异常");
            }
            return result;
        }
        /// <summary>
        /// 分页查询，只返回文档信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序规则</param>
        /// <param name="fields">返回字段</param>
        /// <param name="from">记录开始</param>
        /// <param name="size">记录数</param>
        /// <returns></returns>
        public IEnumerable<T> GetDocuments(QueryContainer query, SortDescriptor<T> sort,
           SourceFilterDescriptor<T> fields, int from = 0, int size = 10)
        {
            return PagingQuery(query, sort, fields, null, from, size).Documents;
        }
        /// <summary>
        /// 分页查询，只返回文档信息（异步）
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序规则</param>
        /// <param name="fields">返回字段</param>
        /// <param name="from">记录开始</param>
        /// <param name="size">记录数</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetDocumentsAsync(QueryContainer query, SortDescriptor<T> sort,
         SourceFilterDescriptor<T> fields, int from = 0, int size = 10)
        {
            var queryResult = await PagingQueryAsync(query, sort, fields, null, from, size);
            return queryResult.Documents;
        }
        #endregion

        #region Builder 条件构建器
        /// <summary>
        /// 构建查询描述器
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sort"></param>
        /// <param name="fields"></param>
        /// <param name="aggs"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private SearchDescriptor<T> BuildSearchDescriptor(QueryContainer query, SortDescriptor<T> sort, ISourceFilter fields,
            IAggregationContainer aggs, int from = 0, int size = 10)
        {

            var searchdesc = new SearchDescriptor<T>()
                .Index(EsIndex)
                .Type(EsType)
                .From(from)
                .Size(size);

            if (query != null) searchdesc = searchdesc.Query(q => query);
            if (sort != null) searchdesc = searchdesc.Sort(s => sort);
            if (fields != null) searchdesc = searchdesc.Source(s => fields);
            if (aggs != null) searchdesc = searchdesc.Aggregations(a => aggs);

            return searchdesc;
        }
        #endregion

        public List<T> GetAllDocuments()
        {
            var allDocuments = new List<T>();
            var scanResults = Client.Search<T>(s => s
                .Index(EsIndex)
                .Type(EsType)
                .From(0)
                .Size(10)
                .MatchAll().
                SearchType(SearchType.DfsQueryThenFetch)
                .Scroll("2s"));

            var results = Client.Scroll<T>("4s", scanResults.ScrollId);
            while (results.Documents.Any())
            {
                allDocuments.AddRange(results.Documents);
                results = Client.Scroll<T>("4s", results.ScrollId);
            }
            return allDocuments;
        }
    }
}
