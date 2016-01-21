﻿using Lsj.Util.Collections;
using Lsj.Util.Net.Web.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lsj.Util.Net.Web.Response
{
    public class HttpResponseHeaders : SafeStringToStringDirectionary
    {
        public string this[eHttpResponseHeader x]
        {
            get
            {
                return this[HeaderType[x]];
            }
            set
            {
                this[HeaderType[x]] = value;
            }
        }
        public static readonly headertype HeaderType = new headertype
        {
            {"Accept-Patch",eHttpResponseHeader.AcceptPatch},
            {"Accept-Patch",eHttpResponseHeader.AcceptRanges },
            {"Access-Control-Allow-Origin",eHttpResponseHeader.AccessControlAllowOrigin},
            {"Age",eHttpResponseHeader.Age},
            {"Allow",eHttpResponseHeader.Allow },
            {"Cache-Control",eHttpResponseHeader.CacheControl },
            {"Connection",eHttpResponseHeader.Connection },
            {"Content-Disposition",eHttpResponseHeader.ContentDisposition },
            {"Content-Encoding",eHttpResponseHeader.ContentEncoding },
            {"Content-Language",eHttpResponseHeader.ContentLanguage },
            {"Content-Length",eHttpResponseHeader.ContentLength },
            {"Content-Location",eHttpResponseHeader.ContentLocation },
            {"Content-MD5",eHttpResponseHeader.ContentMD5 },
            {"Content-Range",eHttpResponseHeader.ContentRange },
            {"Content-Type",eHttpResponseHeader.ContentType },
            {"Date",eHttpResponseHeader.Date },
            {"ETag",eHttpResponseHeader.ETag },
            {"Expires",eHttpResponseHeader.Expires },
            {"Last-Modified",eHttpResponseHeader.LastModified },
            {"Link",eHttpResponseHeader.Link },
            {"Location",eHttpResponseHeader.Location },
            {"P3P",eHttpResponseHeader.P3P },
            {"Pragma",eHttpResponseHeader.Pragma },
            {"Proxy-Authenticate" ,eHttpResponseHeader.ProxyAuthenticate},
            {"Public-Key-Pins",eHttpResponseHeader.PublicKeyPin },
            {"Refresh",eHttpResponseHeader.Refresh },
            {"Retry-After",eHttpResponseHeader.RetryAfter },
            {"Server",eHttpResponseHeader.Server },
            {"Set-Cookie",eHttpResponseHeader.SetCookie },
            {"Status",eHttpResponseHeader.Status },
            {"Transfer-Encoding",eHttpResponseHeader.TransferEncoding },
            {"Upgrade",eHttpResponseHeader.Upgrade },
            {"Vary",eHttpResponseHeader.Vary },
            {"Via",eHttpResponseHeader.Via },
            {"Warning",eHttpResponseHeader.Warning },
            {"WWW-Authenticate",eHttpResponseHeader.WWWAuthenticate },
            {"X-Powered-By",eHttpResponseHeader.XPoweredBy},
        };
        public void Add(eHttpResponseHeader x, string content)
        {
            this.Add(HeaderType[x], content);
        }
        public class headertype : TwoWayDictionary<string, eHttpResponseHeader>
        {
            public override eHttpResponseHeader GetNullValue(string key)
            {
                return eHttpResponseHeader.Unknown;
            }
            public override string GetNullKey(eHttpResponseHeader value)
            {
                return "";
            }
        }
        public string ContentType
        {
            get
            {
                return this[eHttpResponseHeader.ContentType];
            }
            set
            {
                this[eHttpResponseHeader.ContentType] = value;
            }
        }
        public int ContentLength
        {
             get
            {
                return this[eHttpResponseHeader.ContentLength].ConvertToInt(0);
            }
            set
            {
                this[eHttpResponseHeader.ContentLength] = value.ToString();
            }
        }
        public eConnectionType Connection
        {
            get
            {
                return this[eHttpResponseHeader.Connection].ToLower() == "keep-alive" ? eConnectionType.KeepAlive : eConnectionType.Close;
            }
            set
            {
                if (value == eConnectionType.KeepAlive)
                    this[eHttpResponseHeader.Connection] = "keep-alive";
                else
                    this[eHttpResponseHeader.Connection] = "close";
            }
        }
    }
}