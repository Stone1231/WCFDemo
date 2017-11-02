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
    public class RegisterIn
    {
        public RegisterIn() { }

        [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 20, PropertyName = "licenseKey", RequestCode = 10130, InvalidCode = 12130)]
        [DataMember(Name = "LicenseKey")]
        public string LicenseKey { get; set; }

        [DataMember(Name = "SiteChannel")]
        public int SiteChannel { get; set; }

        [DataMemberCheckerAttribute(IsRequired = true, IsMac = true, MaxLength = 18, PropertyName = "Mac", RequestCode = 10134, InvalidCode = 12134)]
        [DataMember(Name = "Mac")]
        public string Mac { get; set; }

        [DataMemberCheckerAttribute(IsRequired = true, MinIntValue = 0, MaxIntValue = 1, PropertyName = "IsDigital", RequestCode = 10132, InvalidCode = 12132)]
        [DataMember(Name = "IsDigital")]
        public int IsDigital { get; set; }

        [DataMemberCheckerAttribute(IsRequired = true, MaxLength = 15, IsIPv4 = true, PropertyName = "Ip", RequestCode = 10133, InvalidCode = 12133)]
        [DataMember(Name = "Ip")]
        public string Ip { get; set; }

        [DataMember(Name = "ErrorCode")]
        public string ErrorCode { get; set; }
    }


    [CollectionDataContract]
    public class RegisterInList : List<RegisterIn> { }
}
