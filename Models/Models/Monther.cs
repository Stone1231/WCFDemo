using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;//add
using System.Xml.Serialization;
using Common;


namespace Models
{
    [DataContract(Namespace = "http://newegg.global.it/ntech/service")]
    [XmlRoot(Namespace = "http://newegg.global.it/ntech/service")]
    public class Monther
    {
        public Monther() { }

         [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 20, PropertyName = "Id", RequestCode = 1001, InvalidCode = 1201)]
         [DataMember(Name = "Id")]
         public string Id { get; set; }

         [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 20, PropertyName = "Name", RequestCode = 1002, InvalidCode = 1202)]
         [DataMember(Name = "Name")]
         public string Name { get; set; }

        [DataMemberCheckerAttribute(IsRequired = true, RequestCode = 1005, InvalidCode = 1205)]
        [DataMember(Name = "Childs")]
        public List<Child> Childs { get; set; }

        [DataMember(Name = "ErrorCode")]
        public string ErrorCode { get; set; }
    }
}
