using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class CheckDataException : Exception
    {
        int code = -1;
        string message = string.Empty;
        string fullMessage = string.Empty;

        public CheckDataException()
        {
        }

        public CheckDataException(int code,string message)
        {
            this.code = code;
            this.message = message;
            this.fullMessage = message;
        }

        public CheckDataException(int code, string message,string fullMessage)
        {
            this.code = code;
            this.message = message;
            this.fullMessage = fullMessage;
        }

        public int Code
        {
            get { return code; }
        }

        public string Message 
        { 
            get { return message;}
        }

        public string PartMessage
        {
            get { return fullMessage; }
        }

        public string FullMessage 
        { 
            get 
            {
                if (string.IsNullOrEmpty(message))
                    return string.Format("Code:{0},Message:{1}", code, fullMessage);
                else
                    return string.Format("Code:{0},Message:{1},value:{2}", code, fullMessage,message);
            } 
        }
    }
}
