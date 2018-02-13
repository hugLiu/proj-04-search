/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: IndexManager
* 创 建 者：zhoush
* 创建日期：2017/5/19 10:24:57
* 功能描述: 
* 
* 修 改 人：    
* 修改时间:     
* 修改日志:    
*
* 审 查 者:     
* 审 查 时 间:  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json.Linq;

namespace Jurassic.So.Search.ES.Index
{
    public static class IndexManager
    {
    //    public static void Reindex()
    //    {
    //        var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/"));
    //        //connectionSettings.SetDefaultIndex("customers");
    //        var elasticClient = new ElasticClient(connectionSettings);
    //        //ReindexOnServerRequest serverRequest=new 
    //        //elasticClient.ReindexOnServer(ReindexOnServerRequest)
   
    //elasticClient.Reindex<Customer>(r =>
    //    r.FromIndex("customers-v1")
    //        .ToIndex("customers-v2")
    //        .Query(q => q.MatchAll())
    //        .Scroll("10s")
    //        .CreateIndex(i =>
    //            i.AddMapping<Customer>(m =>
    //                m.Properties(p =>
    //                    p.String(n => n.Name(name => name.Zipcode).Index(FieldIndexOption.not_analyzed))))));
    //        var o = new ReindexObserver<Customer>(onError: e => { });
    //        reindex.Subscribe(o);
    //    }

        // Assuming you have already created and setup the index yourself
        //public static void Reindex(ElasticClient client, string aliasName, string currentIndexName, string nextIndexName)
        //{
            
        //    Console.WriteLine("Reindexing documents to new index...");
        //    var searchResult = client.Search<object>(s => s.Index(currentIndexName).AllTypes().From(0).Size(100).Query(q => q.MatchAll()).SearchType(SearchType.QueryThenFetch).Scroll("2m"));
        //    if (searchResult.Total <= 0)
        //    {
        //        Console.WriteLine("Existing index has no documents, nothing to reindex.");
        //    }
        //    else
        //    {
        //        var page = 0;
        //        IBulkResponse bulkResponse = null;
        //        do
        //        {
        //            var result = searchResult;
        //            searchResult = client.Scroll<object>(s => s.Scroll("2m").ScrollId(result.ScrollId));
        //            if (searchResult.Documents != null && searchResult.Documents.Any())
        //            {
        //                //searchResult.ThrowOnError("reindex scroll " + page);
        //                bulkResponse = client.Bulk(b =>
        //                {
        //                    foreach (var hit in searchResult.Hits)
        //                    {
        //                        var jo = hit.Source as JObject;
        //                        JToken jt;
        //                        if (jo != null && jo.TryGetValue("parentId", out jt))
        //                        {
        //                            // Document is child-document => add parent reference
        //                            string parentId = (string)jt;
        //                            b.Index<object>(bi => bi.Document(hit.Source).Type(hit.Type).Index(nextIndexName).Id(hit.Id).Parent(parentId));
        //                        }
        //                        else
        //                        {
        //                            b.Index<object>(bi => bi.Document(hit.Source).Type(hit.Type).Index(nextIndexName).Id(hit.Id));
        //                        }
        //                    }

        //                    return b;
        //                });
        //                //.ThrowOnError("reindex page " + page);
        //                Console.WriteLine("Reindexing progress: " + (page + 1) * 100);
        //            }

        //            ++page;
        //        }
        //        while (searchResult.IsValid && bulkResponse != null && bulkResponse.IsValid && searchResult.Documents != null && searchResult.Documents.Any());
        //        Console.WriteLine("Reindexing complete!");
        //    }

        //    Console.WriteLine("Updating alias to point to new index...");
        //    client.Alias(a => a
        //        .Add(aa => aa.Alias(aliasName).Index(nextIndexName))
        //        .Remove(aa => aa.Alias(aliasName).Index(currentIndexName)));

        //    // TODO: Don't forget to delete the old index if you want
        //}
    }
}
