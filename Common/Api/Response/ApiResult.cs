using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            ExceptionType = ApiExceptionType.None;
            Code = 903;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public ApiExceptionType ExceptionType { get; set; }

        public Exception Error { get; set; }

        public T Data { get; set; }

        public List<T> List { get; set; }

        public string RawBody { get; set; }
    }


}
