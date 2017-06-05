using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinYiShengCollege.Business.Common.Models
{
    /// <summary>
    /// 药方字段表，供json序列化和反序列化使用
    /// </summary>
    public class Medicine
    {
        //public string 病症 { get;set;}
        //public string 辩证 { get; set; }
        public string 症候 { get; set; }
        private InternalMethod _内治法 = new InternalMethod();

        public InternalMethod 内治法
        {
            get { return _内治法; }
            set { _内治法 = value; }
        }


        private ExternalMethod _外治法 = new ExternalMethod();

        public ExternalMethod 外治法
        {
            get { return _外治法; }
            set { _外治法 = value; }
        }
        
        public string 禁忌 { get; set; }
       
    }
    public class InternalMethod
    {
        public string 中成药{get;set;}
        public string 经验方{get;set;}
        
    }
    public class ExternalMethod
    {
        public string 穴位{get;set;}
        public string 脊柱{get;set;}
        public string 耳穴{get;set;}
        public string 食疗{get;set;}
        public string 运动{get;set;}
    }
}
