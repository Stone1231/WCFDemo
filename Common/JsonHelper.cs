using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Reflection;
using System.Web;

namespace Common
{
    public class JsonHelper
    {

        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        public static Stream SerializeToJsonStream<T>(T response)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();

            serializer.WriteObject(ms, response);
            ms.Position = 0;

            return ms;
        }

        public static T DeserializeFromJsonStream<T>(Stream stream)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            if (stream.Position != 0)
            {
                if (!stream.CanSeek) return default(T);
                stream.Position = 0;
            }

            try
            {
                T instance = (T)serializer.ReadObject(stream);
                return instance;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
                return null;

            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == propertyName.ToLower())
                {
                    return prop.GetValue(obj, null);
                }
            }

            return null;
        }

        private static bool CheckIsRequired(object obj,DataMemberCheckerAttribute[] atts)
        {
            if (string.IsNullOrEmpty(atts[0].ParentFiled))
            {
                return atts[0].IsRequired;
            }
            else
            {
                string[] fields = atts[0].ParentFiled.Split(',');
                foreach (string fileld in fields)
                {
                    object parent = GetPropertyValue(obj, fileld);
                    if (parent != null)
                    {
                        string pValue = parent.ToString();
                        if (pValue.ToLower() == "false" ||
                            (pValue.ToLower() != "true" && pValue.ToLower() != "false" && string.IsNullOrEmpty(pValue.Trim())))
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }

                return true;
                //return false;
            }
        }

        public static void Check(object obj)
        {
            if (obj == null)
            {
                return;
            }
            PropertyInfo[] props = obj.GetType().GetProperties();
            bool isRequired = false;
            foreach (PropertyInfo prop in props)
            {
                isRequired = false;
                if (prop.PropertyType.FullName == typeof(String).ToString())
                {
                    DataMemberCheckerAttribute[] atts = prop.GetCustomAttributes(typeof(DataMemberCheckerAttribute), true)
                        as DataMemberCheckerAttribute[];

                    if (atts != null &&
                        atts.Length > 0)
                    {
                        object v = prop.GetValue(obj, null);
                        string value = v == null ? "" : v.ToString().Trim();
                        if (atts[0].IsUrldecode)
                            value = HttpUtility.UrlDecode(value, Encoding.UTF8);

                        prop.SetValue(obj, value, null);

                        isRequired = CheckIsRequired(obj, atts);

                        if (isRequired && string.IsNullOrEmpty(value))
                            throw new CheckDataException(atts[0].RequestCode, string.Format("Please fill the required item.({0})", atts[0].PropertyName));

                        if (atts[0].IsBase64Decode)
                        {
                            try
                            {
                                value = Encoding.UTF8.GetString(Convert.FromBase64String(value));
                                prop.SetValue(obj, value, null);
                            }
                            catch (Exception ex)
                            {
                                throw new CheckDataException(atts[0].InvalidCode, value,ex.Message);
                            }
                        }

                        if (value.Length > atts[0].MaxLength)
                            throw new CheckDataException(atts[0].InvalidCode, value,atts[0].PropertyName + " can not be larger than " + atts[0].MaxLength.ToString());
                        if (value.Length < atts[0].MinLength)
                            throw new CheckDataException(atts[0].InvalidCode, value, atts[0].PropertyName + " can not be less than " + atts[0].MinLength.ToString());

                        if (atts[0].IsEmailAddress)
                        { 
                            if( !RegexUtil.IsEmail(value))
                                throw new CheckDataException(atts[0].InvalidCode, value, atts[0].PropertyName + " is invalid");    
                        }

                        if (atts[0].IsIPv4)
                        {
                            if (!RegexUtil.IsIPv4(value))
                                throw new CheckDataException(atts[0].InvalidCode, value, atts[0].PropertyName + " is invalid");
                        }

                        if (atts[0].IsMac)
                        {
                            if (!RegexUtil.IsMac(value))
                                throw new CheckDataException(atts[0].InvalidCode, value, atts[0].PropertyName + " is invalid");
                        }
                    }

                }
                else if (prop.PropertyType.FullName == typeof(Int32).ToString())
                {

                    DataMemberCheckerAttribute[] atts = prop.GetCustomAttributes(typeof(DataMemberCheckerAttribute), true)
                       as DataMemberCheckerAttribute[];

                    if (atts != null && atts.Length > 0 && prop.GetValue(obj, null) != null)
                    {
                        int value = default(int);
                        if (int.TryParse(prop.GetValue(obj, null).ToString(), out value))
                        {
                            isRequired = CheckIsRequired(obj, atts);

                            if (isRequired)
                            {
                                if (value == default(int) && atts[0].MinIntValue != default(int))
                                {
                                    throw new CheckDataException(atts[0].RequestCode, string.Format("Please fill the required item.({0})", atts[0].PropertyName));
                                }

                                if (value > atts[0].MaxIntValue || value < atts[0].MinIntValue)
                                {
                                    throw new CheckDataException(atts[0].InvalidCode, value.ToString(), atts[0].PropertyName + " must between " + atts[0].MinIntValue + " and " + atts[0].MaxIntValue);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(atts[0].ParentFiled))
                                {
                                    if (value > atts[0].MaxIntValue || value < atts[0].MinIntValue)
                                    {
                                        throw new CheckDataException(atts[0].InvalidCode, value.ToString(), atts[0].PropertyName + " must between " + atts[0].MinIntValue + " and " + atts[0].MaxIntValue);
                                    }
                                }
                                else
                                {
                                    value = 0;
                                    prop.SetValue(obj, value, null);
                                }
                            }
                        }
                        else
                        {
                            throw new CheckDataException(atts[0].InvalidCode, value.ToString()," value of " + atts[0].PropertyName );
                        }
                    }
                }
                else if (prop.PropertyType.FullName == typeof(float).ToString())
                {

                    DataMemberCheckerAttribute[] atts = prop.GetCustomAttributes(typeof(DataMemberCheckerAttribute), true)
                       as DataMemberCheckerAttribute[];

                    if (atts != null && atts.Length > 0 &&
                          prop.GetValue(obj, null) != null)
                    {
                        float value = default(float);
                        if (float.TryParse(prop.GetValue(obj, null).ToString(), out value))
                        {
                            if (atts[0].IsRequired && value == default(float))
                            {
                                throw new CheckDataException(atts[0].RequestCode, string.Format("Please fill the required item.({0})", atts[0].PropertyName));
                            }
                            else if (value > atts[0].MaxFloatValue)
                            {
                                throw new CheckDataException(atts[0].InvalidCode, value.ToString());
                            }
                        }
                        else
                        {
                            throw new CheckDataException(atts[0].InvalidCode, value.ToString());
                        }
                    }
                }
                else
                {
                    if (prop.PropertyType.FullName.IndexOf("System.Collections.Generic.List`1") == 0)
                    {
                        DataMemberCheckerAttribute[] atts = prop.GetCustomAttributes(typeof(DataMemberCheckerAttribute), true) as DataMemberCheckerAttribute[];
                        if (atts != null && atts.Length > 0)
                        {
                            object v = prop.GetValue(obj, null);
                            if( (v == null) && atts[0].IsRequired )
                                throw new CheckDataException(atts[0].RequestCode, string.Format("Please fill the required item.({0})", atts[0].PropertyName));
                             
                        }
                    }
                    else
                    {
                        //if (prop.PropertyType.FullName.IndexOf("Newegg.OfficeWeb.Workflow.Interface.") == 0)
                        //{
                            object v = prop.GetValue(obj, null);
                            if (v != null)
                                Check(v);
                        //}
                    }
                }
                
            }
        }


        public static string DecodeUrl(string beforeCode)
        {
            return HttpUtility.UrlDecode(beforeCode, Encoding.UTF8);
        }

        public static string EncodeUrl(string beforeCode)
        {
            return HttpUtility.UrlEncode(beforeCode, Encoding.UTF8);
        }

        public static bool ValidataDomain(string emailAddres, string companyDomain)
        {
            if (string.IsNullOrEmpty(emailAddres) || string.IsNullOrEmpty(emailAddres.Trim()))
                return false;

            emailAddres = emailAddres.ToLower().Trim();
            int pos = emailAddres.IndexOf("@");
            if (pos <= 0)
                return false;

            string checkDn = emailAddres.Substring(pos + 1);
            string[] dns = companyDomain.Trim().ToLower().Split(',');
            foreach (string dn in dns)
            {
                if (dn.Trim() == checkDn)
                    return true;
            }

            return false;
        }
        public static string JsonSingle(string v) 
        {
            v = v.Replace("{", " ")
                 .Replace("}", " ")
                 .Replace("\"", " ");
            v = v.Trim();
            if (v != string.Empty) 
            {
                v = v.Split(':')[1].Trim();
            }
            return v;
        }

        public static string ConvertChar(string v)
        {
            v = v.Replace("'", @"\'");
            v = v.Replace("_", @"\_");
            v = v.Replace("%", @"\%");
            v = v.Replace("\"", "\\\"");
            v = v.Replace("`", @"\`");
            v = v.Replace(@"\", "\\");
            v = v.Replace(">", @"\>");
            v = v.Replace("<", @"\<");
            v = v.Replace("=", @"\=");
            return v;
        }
    }
}
