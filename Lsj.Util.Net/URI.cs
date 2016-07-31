﻿using Lsj.Util.Collections;
using Lsj.Util.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lsj.Util.Net
{
    /// <summary>
    /// URI
    /// </summary>
    public class URI
    {
        private string raw;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public URI(string uri)
        {
            this.raw = uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return raw;
        }
        string filename;
        /// <summary>
        /// FileName
        /// </summary>
        public string FileName
        {
            get
            {
                if(filename==null)
                {
                    var x = raw.LastIndexOf('/');
                    x++;
                    filename = raw.Substring(x);
                    var a = filename.IndexOf('?');
                    if (a > 0)
                        filename = filename.Substring(0, a);
                }
                return filename;

            }
        }

        SafeStringToStringDirectionary querystring;
        /// <summary>
        /// QueryString
        /// </summary>
        public SafeStringToStringDirectionary QueryString
        {
            get
            {
                if(querystring==null)
                {
                    querystring = new SafeStringToStringDirectionary();
                    if (raw.IndexOf('?') != -1)
                    {
                        string z = raw.Substring(raw.IndexOf('?') + 1);
                        {
                            var a = z.Split('&');
                            foreach (var b in a)
                            {
                                var c = b.Split('=');
                                if (c.Length >= 2)
                                {
                                    var name = c[0].Trim();
                                    var content = c[1].Trim();
                                    querystring.Add(c[0], c[1]);
                                }
                            }
                        }
                    }
                }
                return querystring;
            }
        }
        string scheme;
        /// <summary>
        /// Scheme
        /// </summary>
        public string Scheme
        {
            get
            {
                if(scheme == null)
                {
                    int i = raw.IndexOf(':');
                    if(i>0)
                    {
                        scheme = raw.Substring(0, i);
                    }
                    else
                    {
                        scheme = "";
                    }
                }
                return scheme;
            }
        }
        string host;
        int? port;
        string localpath;
        /// <summary>
        /// Host
        /// </summary>
        public string Host
        {
            get
            {
                if (host == null)
                {
                    Solve();
                }
                return host;
            }
        }
        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get
            {
                if (port == null)
                {
                    Solve();
                }
                if (port == 0)
                {
                    switch (Scheme.ToLower())
                    {
                        case "http":
                            return 80;
                        case "https":
                            return 443;
                        default:
                            return port.Value;
                    }
                }
                return port.Value;
            }
        }
        /// <summary>
        /// LoaclPath
        /// </summary>
        public string LocalPath
        {
            get
            {
                if (localpath == null)
                {
                    Solve();
                }
                return localpath;
            }
        }


        void Solve()
        {
            int i = raw.IndexOf("//");
            if (i >= 0)
            {
                var x = raw.Substring(i + 2);
                i = x.IndexOf('/');
                if (i > 0)
                {
                    localpath = x.Substring(i);
                    x = x.Substring(0, i);
                    i = x.IndexOf(':');
                    if (i >0)
                    {
                        host = x.Substring(0, i);
                        port = x.Substring(i + 1).ConvertToInt(0);
                    }
                    else
                    {
                        host = x;
                    }
                }
                else
                {
                    host = x;
                    port = 0;
                    localpath = "/";
                }
            }
            else
            {
                host = "";
                port = 0;
                localpath = "";
            }
        }


    }
}
