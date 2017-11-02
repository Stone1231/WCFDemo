/*******************************************************
 * 
 * Author:Quentus.g.gou
 * 
 * Date:2013-01-08
 * 
 * Functino:execute http request
 * 
 * ******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Common
{
    public static class ApiManager
    {
        public static string ExecuteAPI(string url, ApiMethod method, Dictionary<string, string> header, string body, string contentType)
        {
            ResponseData<string> response = Execute<string>(url, method, header, body, contentType);
            return response.RawBody;
        }

        public static ResponseData<T> Execute<T>(string url, ApiMethod method, Dictionary<string, string> header, string body, string contentType)
        {
            HttpWebRequest httpRequest = null;

            System.IO.Stream dataStream = null;
            HttpWebResponse httpResponse = null;
            string fullUrl = url ;

            ResponseData<T> response = null;

            try
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
                httpRequest.Method = method.ToString();

                if (header != null)
                {
                    foreach (string key in header.Keys)
                    {
                        httpRequest.Headers.Add(key, header[key]);
                    }
                }

                if (string.IsNullOrEmpty(contentType))
                    httpRequest.ContentType = "application/json";
                else
                    httpRequest.ContentType = contentType;

                httpRequest.Timeout = 120000;

                if (url.ToLower().Trim().StartsWith("https://"))
                {
                    ServicePointManager.ServerCertificateValidationCallback = ValidateRemoteCertificate;
                }

                //httpRequest.Headers.Add("Origin", "http://10.16.140.34");
                if (!string.IsNullOrEmpty(body))
                {
                    byte[] bty = Encoding.UTF8.GetBytes(body);
                    httpRequest.ContentLength = bty.Length;
                    Stream newStream = httpRequest.GetRequestStream();
                    newStream.Write(bty, 0, bty.Length);
                    newStream.Close();
                }
                else 
                {
                    httpRequest.ContentLength = 0;
                }

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                dataStream = httpResponse.GetResponseStream();
                StreamReader sr = new StreamReader(dataStream, Encoding.UTF8);
                string result = sr.ReadToEnd();
                result = result.Replace("True", "true");

                if (typeof(T) == typeof(string))
                {
                    response = new ResponseData<T>(httpResponse.StatusCode, result, null, default(T));
                }
                else
                {
                    try
                    {
                        T t = JsonHelper.ParseFromJson<T>(result);
                        response = new ResponseData<T>(httpResponse.StatusCode, result, null, t);
                    }
                    catch (Exception ex)
                    {
                        response = new ResponseData<T>(httpResponse.StatusCode, result, ex, default(T));
                    }
                    
                }

            }
            catch (System.Net.WebException webEx)
            {
                if (httpResponse != null)
                    response = new ResponseData<T>(httpResponse.StatusCode, webEx.Message, webEx, default(T));
                else
                {
                    if(webEx.Response != null)
                        response = new ResponseData<T>((webEx.Response as HttpWebResponse).StatusCode, webEx.Message, webEx, default(T));
                    else
                    {
                        if(webEx.Status == WebExceptionStatus.Timeout)
                            response = new ResponseData<T>(HttpStatusCode.RequestTimeout, webEx.Message, webEx, default(T));
                        else
                            response = new ResponseData<T>(HttpStatusCode.BadRequest, webEx.Message, webEx, default(T));
                    }
                }
            }
            catch (Exception ex)
            {
                if(httpResponse !=null)
                    response = new ResponseData<T>(httpResponse.StatusCode, ex.Message, ex, default(T));
                else
                    response = new ResponseData<T>(HttpStatusCode.BadRequest, ex.Message, ex, default(T));
            }
            finally
            {
                if (httpResponse != null)
                    httpResponse.Close();
            }

            return response;
        }


        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        {
            bool result = false;
            //if (cert.Subject.ToUpper().Contains("YourServerName"))
            //{
            result = true;
            //}

            return result;
        }

        public static ApiResult<T> GetApiResult<T>(ResponseData<ResponseContent<T>> response,string errorCode)
        {
            ApiResult<T> apiResult = new ApiResult<T>();
            if (!string.IsNullOrEmpty(errorCode))
                apiResult.Code = Convert.ToInt32(apiResult.Code.ToString() + errorCode);

            if (response.Error != null)
            {
                apiResult.Error = response.Error;
                if (response.RawBody == response.Error.Message)
                {
                    apiResult.ExceptionType = ApiExceptionType.InvokeApiEx;
                    apiResult.Message = response.Error.ToString();
                }
                else
                {
                    apiResult.ExceptionType = ApiExceptionType.DeserialEx;
                    apiResult.RawBody = response.RawBody ?? "";
                    apiResult.Code = 906;
                    apiResult.Message = response.RawBody ?? "";
                }
                apiResult.Data = default(T);
            }
            else
            {
                apiResult.Code = response.Data.Code;
                apiResult.Message = response.Data.Message;

                if (response.Data.Code != 0)
                {
                    apiResult.ExceptionType = ApiExceptionType.ApiInternalEx;
                    apiResult.Error = new Exception(response.Data.Message);
                    apiResult.Data = default(T);
                    return apiResult;
                }

                if (response.Data.DataCollection == null ||
                    response.Data.DataCollection.Count == 0)
                {
                    apiResult.Data = default(T);
                    apiResult.List = new List<T>();
                }
                else
                {
                    apiResult.Data = response.Data.DataCollection[0];
                    apiResult.List = response.Data.DataCollection;
                }
            }

            return apiResult;
        }

        public static ApiResult<T> GetResult<T>(ResponseData<T> response,string detailCode)
        {
            ApiResult<T> apiResult = new ApiResult<T>();

            int code = (int)response.StatusCode; ;
            if (code == 200 || response.Error == null)
            {
                if (response.Error != null)
                {
                    apiResult.Code = 906;
                    apiResult.ExceptionType = ApiExceptionType.DeserialEx;
                }
                else
                {
                    if (response.Data == null)
                    {
                        apiResult.Code = 901;
                        apiResult.ExceptionType = ApiExceptionType.ApiInternalEx;
                    }
                    else
                    {
                        apiResult.Code = 0;
                    };

                }
                apiResult.Message = null;
                apiResult.Data = response.Data;
            }
            //else if (code == 404)
            //{
            //    apiResult.Code = code404;
            //    apiResult.Message = message;
            //    apiResult.Error = response.Error;
            //    apiResult.ExceptionType = ApiExceptionType.ApiInternalEx;
            //    apiResult.Data = default(T);
            //}
            else
            {
                apiResult.Code = Convert.ToInt32("903" + detailCode);
                apiResult.ExceptionType = ApiExceptionType.InvokeApiEx;
                apiResult.Error = response.Error;
                apiResult.Message = string.Empty;
                apiResult.Data = default(T);
            }

            return apiResult;
        }

        public static byte[] Execute(string url, Dictionary<string, string> header, string contentType)
        {
            HttpWebRequest httpRequest = null;

            System.IO.Stream dataStream = null;
            HttpWebResponse httpResponse = null;
            string fullUrl = url;


            try
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
                httpRequest.Method = "GET";

                if (header != null)
                {
                    foreach (string key in header.Keys)
                    {
                        httpRequest.Headers.Add(key, header[key]);
                    }
                }

                if (string.IsNullOrEmpty(contentType))
                    httpRequest.ContentType = "application/json";
                else
                    httpRequest.ContentType = contentType;

                httpRequest.Timeout = 120000;
                httpRequest.ContentLength = 0;

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                dataStream = httpResponse.GetResponseStream();
                long cl = httpResponse.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                MemoryStream outputStream = new MemoryStream();
                readCount = dataStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = dataStream.Read(buffer, 0, bufferSize);
                }
                dataStream.Close();
                byte[] btys = outputStream.ToArray();
                outputStream.Close();
                return btys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (httpResponse != null)
                    httpResponse.Close();
            }

        }

    }
}
