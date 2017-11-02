using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;//add
using System.Text;

using Models;//add
using System.IO;//add

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService3" in both code and config file together.
    [ServiceContract]
    public interface IServiceTest
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWord/{word}")]
        string GetWord(string word);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHeader")]
        string GetHeader();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostRegisterIn")]
        RegisterIn PostRegisterIn(RegisterIn info);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostRegisterIn2")]
        RegisterIn PostRegisterIn2(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostRegisterIn3")]
        RegisterIn PostRegisterIn3(Stream stream);

        //失敗
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostRegisterInList", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<RegisterIn> PostRegisterInList(List<RegisterIn> list);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostRegisterInList2", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<RegisterIn> PostRegisterInList2(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostParent")]
        Parent PostParent(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostMonther")]
        Monther PostMonther(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CallGetWord/{word}")]
        string CallGetWord(string word);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CallPostRegisterIn3")]
        RegisterIn CallPostRegisterIn3(Stream stream);
    }
}
