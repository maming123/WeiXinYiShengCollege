using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace Senparc.Weixin.MP.Sample.CommonService.ModelForPageShow
{
    /// <summary>
    /// 图文素材仅仅为页面展示使用
    /// </summary>
    public class NewsList
    {
        public String media_id { get; set; }
        public String title { get; set; }
        public String author { get; set; }
        public String digest { get; set; }
        public String Json { get; set; }
    }


}
