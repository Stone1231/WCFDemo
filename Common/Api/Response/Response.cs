using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Common
{
    public class Response
    {
        public Response()
        { }

        public Response(HttpStatusCode statusCode, string rawBody, Exception ex)
        {
            this.StatusCode = statusCode;
            this.RawBody = rawBody;
            this.Error = ex;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string RawBody { get; private set; }

        public Exception Error { get; private set; }
    }
}
