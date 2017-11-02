using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Models
{

    [DataContract(Name = "DataResponse", Namespace = "http://newegg.global.it/ntech/service")]
    [XmlRoot(ElementName = "DataRespose", Namespace = "http://newegg.global.it/ntech/service")]
    public class DataResult
    {
        private string message;
        private bool m_bSuccess;

        public DataResult()
        {
            //message = string.Empty;
            Code = -1;
            m_bSuccess = false;        
        }

        [DataMember(Name = "message")]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string FullMessage { get; set; }

        [DataMember(Name="isSuccess")]
        public bool IsSuccess
        {
            get { return m_bSuccess; }
            set { m_bSuccess = value; }
        }

        [DataMember(Name="code")]
        public int Code { get; set; }

        public List<Object> Datas { get; set; }
    }
}
