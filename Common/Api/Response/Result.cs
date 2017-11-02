﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract(Name = "Result")]
    public class Result<T>
    {
        [DataMember]
        public T Value { get; set; }
    }
}
