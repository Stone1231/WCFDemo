using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Common
{
    public class ResponseData<T> : Response
    {
        public ResponseData(HttpStatusCode statusCode, string rawBody, Exception ex, T data)
            : base(statusCode, rawBody, ex)
        {

            this.Data = data;
        }

        //public string RawBody { get { return base.RawBody; } }

        public T Data
        {
            get;
            private set;
        }
    }
}
