using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module.Utils
{
    public class ReturnJsonType
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ReturnJsonType<T>
    {
        public int code { get; set; }

        public T m { get; set; }
    }

}