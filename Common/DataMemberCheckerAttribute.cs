using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    public class DataMemberCheckerAttribute : Attribute
    {
        public DataMemberCheckerAttribute()
        {
        }

        private int _Maxlength = 0;
        private int _Minlength = 0;
        /// <summary>
        /// the max length of string type
        /// </summary>
        public int MaxLength
        {
            get { return _Maxlength; }
            set { _Maxlength = value; }
        }
        public int MinLength
        {
            get { return _Minlength; }
            set { _Minlength = value; }        
        }

        private string _PropertyName = string.Empty;
        /// <summary>
        /// Name for warning
        /// </summary>
        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }

        private int _maxValue = 0;
        /// <summary>
        /// For int type
        /// </summary>
        public int MaxIntValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        private int _minValue = 0;
        public int MinIntValue 
        {
            get { return _minValue; }
            set { _minValue = value; } 
        }

        private float _maxFloatValue = 0.00f;
        /// <summary>
        /// For int type
        /// </summary>
        public float MaxFloatValue
        {
            get { return _maxFloatValue; }
            set { _maxFloatValue = value; }
        }

        private bool _isRequried = false;
        /// <summary>
        /// whether is requried
        /// </summary>
        public bool IsRequired
        {
            get { return _isRequried; }
            set { _isRequried = value; }
        }

        public int RequestCode { get; set; }

        public int InvalidCode { get; set; }

        private bool _isUrlDecode = false;

        /// <summary>
        /// whether url decode
        /// </summary>
        public bool IsUrldecode 
        {
            get { return _isUrlDecode; }
            set { _isUrlDecode = value; }
        }

        public bool IsBase64Decode { get; set; }

        public string ParentFiled { get; set; }

        public bool IsEmailAddress { get; set; }

        public bool IsIPv4 { get; set; }

        public bool IsMac { get; set; }
    }
}
