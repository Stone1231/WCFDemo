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
    public class Child
    {
        public Child() { }

         [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 10, PropertyName = "Id", RequestCode = 1001, InvalidCode = 1201)]
         [DataMember(Name = "Id")]
         public string Id { get; set; }

         [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 10, PropertyName = "Name", RequestCode = 1002, InvalidCode = 1202)]
         [DataMember(Name = "Name")]
         public string Name { get; set; }

         [DataMember(Name = "HasPlay")]
         public bool HasPlay { get; set; }

         [DataMemberCheckerAttribute(ParentFiled = "HasPlay", MaxLength = 10, PropertyName = "Play", RequestCode = 1004, InvalidCode = 1204)]
         [DataMember(Name = "Play")]
         public string Play { get; set; }
    }
}
