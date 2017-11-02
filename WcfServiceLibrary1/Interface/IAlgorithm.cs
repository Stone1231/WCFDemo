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
    [ServiceContract]
    public interface IAlgorithm
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Combination/{length}")]
        List<string> Combination(string length, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Perm/{length}")]
        List<string> Perm(string length, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Sort")]
        int[] Sort(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DictionaryOrder")]
        List<string> DictionaryOrder(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DictionaryOrder2/{length}")]
        List<string> DictionaryOrder2(string length, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "IntTake/{num}")]
        List<string> IntTake(string num);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Hanoi/{num}")]
        List<string> Hanoi(string num);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWord/{word}")]
        string GetWord(string word);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "QuickSort")]
        int[] QuickSort(Stream stream);
    }
}
