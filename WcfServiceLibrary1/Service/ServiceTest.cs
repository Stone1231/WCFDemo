using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Models;//add
using System.IO;//add
using Common;//add
using System.ServiceModel.Web;//add

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service3" in both code and config file together.
    public class ServiceTest : IServiceTest
    {
        string baseServiceUrl = "http://localhost:8020/OfficeWorkflow";

        public string GetWord(string word)
        {
            return word;
        }

        public string GetHeader()
        {
            var context = WebOperationContext.Current.IncomingRequest;
            return context.Headers["testheader"];
        }

        public RegisterIn PostRegisterIn(RegisterIn info)
        {
            return info;

            /* Content-Type application/json
            {
                "LicenseKey":"LicenseKeyTest",
                "SiteChannel":2,
                "Mac":"MacTest",
                "IsDigital":1,
                "Ip":"101.101.101.101",
                "ErrorCode":"xxx"
            }
            */
        }

        public RegisterIn PostRegisterIn2(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            RegisterIn info = new RegisterIn();

            try
            {
                info = JsonHelper.ParseFromJson<RegisterIn>(content);
            }
            catch (Exception ex)
            {
                info.ErrorCode = ex.Message;
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return info;
        }

        public RegisterIn PostRegisterIn3(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            RegisterIn info = new RegisterIn();

            try
            {
                info = JsonHelper.ParseFromJson<RegisterIn>(content);

                try
                {
                    JsonHelper.Check(info);
                }
                catch (CheckDataException ex)
                {
                    info.ErrorCode = ex.Code.ToString();
                    //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, ex.Message);
                    //result.IsSuccess = false;
                    //result.Code = ex.Code;
                    //result.Message = ex.Message;
                    //return result;
                }
            }
            catch (Exception ex)
            {
                info.ErrorCode = ex.Message;
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return info;
        }

        public List<RegisterIn> PostRegisterInList(List<RegisterIn> list)
        {
            return list;
        }

        public List<RegisterIn> PostRegisterInList2(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            List<RegisterIn> list = null;

            try
            {
                list = JsonHelper.ParseFromJson<List<RegisterIn>>(content);

                try
                {
                    JsonHelper.Check(list);
                }
                catch (CheckDataException ex)
                {
                    //info.ErrorCode = ex.Code.ToString();
                    //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, ex.Message);
                    //result.IsSuccess = false;
                    //result.Code = ex.Code;
                    //result.Message = ex.Message;
                    //return result;
                }
            }
            catch (Exception ex)
            {
                //info.ErrorCode = ex.Message;
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return list;
        }

        public Parent PostParent(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            Parent info = new Parent();

            try
            {
                info = JsonHelper.ParseFromJson<Parent>(content);

                try
                {
                    JsonHelper.Check(info);
                }
                catch (CheckDataException ex)
                {
                    info.ErrorCode = ex.Code.ToString();
                    //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, ex.Message);
                    //result.IsSuccess = false;
                    //result.Code = ex.Code;
                    //result.Message = ex.Message;
                    //return result;
                }
            }
            catch (Exception ex)
            {
                info.ErrorCode = ex.Message;
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return info;

            /*
            {
                "Id":"IdTest",
                "Name":"NameTest",
                "AChild":
                {
                    "Id":"IdTest",
                    "Name":"NameTest",
                    "HasPlay":true,
                    "Play":"PlayTest"
                },
                "ErrorCode":""
            }             
             */
        }

        public Monther PostMonther(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            Monther info = new Monther();

            try
            {
                info = JsonHelper.ParseFromJson<Monther>(content);

                try
                {
                    JsonHelper.Check(info);
                    foreach (var child in info.Childs) {
                        JsonHelper.Check(child);
                    }
                }
                catch (CheckDataException ex)
                {
                    info.ErrorCode = ex.Code.ToString();
                    //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, ex.Message);
                    //result.IsSuccess = false;
                    //result.Code = ex.Code;
                    //result.Message = ex.Message;
                    //return result;
                }
            }
            catch (Exception ex)
            {
                info.ErrorCode = ex.Message;
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return info;

            /*
            {
                "Id":"IdTest",
                "Name":"NameTest",
                "Childs":[
                {
                    "Id":"IdTest1",
                    "Name":"NameTest1",
                    "HasPlay":true,
                    "Play":"PlayTest1"
                },
                {
                    "Id":"IdTest2",
                    "Name":"NameTest2",
                    "HasPlay":true,
                    "Play":"PlayTest2"
                },
                {
                    "Id":"IdTest3",
                    "Name":"NameTest3",
                    "HasPlay":true,
                    "Play":"PlayTest3"
                },
                ],
                "ErrorCode":""
            }             
            */
        }

        public string CallGetWord(string word)
        {
            string url = string.Format("{0}/GetWord/{1}", baseServiceUrl, word);

            string body = string.Empty;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            ResponseData<string> response = ApiManager.Execute<string>(url, ApiMethod.GET, headers, body, null);

            //var result = JsonHelper.ParseFromJson<string>(response.RawBody);

            return response.RawBody;
        }

        public RegisterIn CallPostRegisterIn3(Stream stream)
        {
            string url = string.Format("{0}/PostRegisterIn3", baseServiceUrl);

            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            var info = JsonHelper.ParseFromJson<RegisterIn>(content);

            string body = content;// JsonHelper.GetJson<RegisterIn>(info);

            Dictionary<string, string> headers = new Dictionary<string, string>();

            //ResponseData<ResponseContent<RegisterIn>> response = ApiManager.Execute<ResponseContent<RegisterIn>>(url, ApiMethod.POST, headers, body, null);
            //return response.Data.DataCollection[0];

            ResponseData<RegisterIn> response = ApiManager.Execute<RegisterIn>(url, ApiMethod.POST, headers, body, "text/plain");
            return response.Data;
        }
    }
}
