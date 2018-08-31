﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lsj.Util.Net.Web.Protocol;
using Lsj.Util.Text;

namespace Lsj.Util.Net.Web.Message
{
    /// <summary>
    /// HttpResponse for client
    /// </summary>
    public class HttpResponseForClient : HttpResponse
    {

        /// <summary>
        /// ContentLength
        /// </summary>
        public override int ContentLength => Headers[HttpHeader.ContentLength].ConvertToInt(0);
    }
}