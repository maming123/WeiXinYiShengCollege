using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Module.Utils;
using PetaPoco;
/******************
 * add by maming 20150912
 * 以下类 为自动生成的类的扩展类 ，里边包含扩展的属性 方法等
 * 
 * ************/
namespace Module.Models
{
 
    /// <summary>
    /// 带Str结尾的都为别名供页面显示
    /// </summary>
    public partial class Sys_User
    {
        
        

    }
    /// <summary>
    /// Sys_User的扩展 应对联合查询
    /// </summary>
    public class Sys_UserEx : Sys_User
    {
        
        public string ProvinceStr { get; set; }
        
        public string CityStr { get; set; }
        
        public string CustomerManagerMobile { get; set; }
       
        public string CustomerManagerName { get; set; }
        
        public string ApproveFlagStr { get; set; }
        
        public string UserTypeStr { get; set; }
        
        public string UserLevelStr { get; set; }
        public string ParentNickName { get; set; }
    }
    
}
