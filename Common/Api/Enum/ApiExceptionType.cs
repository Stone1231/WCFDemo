using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum ApiExceptionType
    {
        None,

        /// <summary>
        /// 调用api 异常
        /// </summary>
        InvokeApiEx,

        /// <summary>
        /// api内部执行时发生的异常
        /// </summary>
        ApiInternalEx,

        /// <summary>
        /// 调用成功，但是反序列化失败
        /// </summary>
        DeserialEx
    }
}
