using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
   [DataContract(Name = "ResponseContent")]
    public class ResponseContent<T>
    {
        /// <summary>
        /// 0=成功，其它表示失败
        /// </summary>
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }

       [DataMember]
        public string TXT { get; set; }

        [DataMember]
        public DataCollection<T> DataCollection { get; set; }

        public void Add(T t)
        {
            if (DataCollection == null)
                DataCollection = new DataCollection<T>();
            DataCollection.Add(t);
        }

        public void AddRange(IEnumerable<T> list)
        {
            if (DataCollection == null)
                DataCollection = new DataCollection<T>();
            DataCollection.AddRange(list);
        }
    }

    [CollectionDataContract(Name = "DataCollection")]
    public class DataCollection<T> : List<T>
    { }
}
