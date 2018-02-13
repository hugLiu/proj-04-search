using System;
using System.Net;
using System.Net.Http;

namespace Jurassic.So.Core.Exceptions
{
    /// <summary>异常扩展</summary>
    public static class ExceptionExtension
    {
        /// <summary>异常代码</summary>
        private static readonly string Code = "Jurassic.So.Exception:Code";
        /// <summary>异常明细</summary>
        private static readonly string Details = "Jurassic.So.Exception:Details";
        /// <summary>异常代码</summary>
        private static readonly string HttpResponseStatusCode = "Jurassic.So.Exception:HttpResponseStatusCode";
        /// <summary>异常明细</summary>
        private static readonly string HttpResponseReasonPhrase = "Jurassic.So.Exception:HttpResponseReasonPhrase";
        /// <summary>抛出符合规范的异常</summary>
        public static void Throw<TCode>(this Exception ex, TCode code, string details)
            where TCode : struct
        {
            if (!(ex is UserFriendlyException) && !ex.Data.Contains(Code))
            {
                ex.Data[Code] = code.ToString();
                ex.Data[Details] = details;
            }
            throw ex;
        }
        /// <summary>抛出符合规范的异常</summary>
        public static void Throw(this HttpRequestException ex, HttpResponseMessage response)
        {
            ex.Data[HttpResponseReasonPhrase] = response.ReasonPhrase;
            ex.Data[HttpResponseStatusCode] = response.StatusCode;
            throw ex;
        }
        /// <summary>抛出用户友好异常</summary>
        public static void ThrowUserFriendly<TCode>(this TCode code, string message, string details)
            where TCode : struct
        {
            throw new UserFriendlyException(code.ToString(), message, details);
        }
   
    }
}
