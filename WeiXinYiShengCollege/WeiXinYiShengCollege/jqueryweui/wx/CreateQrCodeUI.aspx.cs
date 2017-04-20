using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class CreateQrCodeUI : System.Web.UI.Page
    {
        public string qrcodeurl = "";
        public string ImgUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string openId = RequestKeeper.GetFormString(Request["OpenId"]);
                Sys_User u = UserBusiness.GetUserInfo(openId);
                if (u != null && u.Id > 0 && u.QrCodeScene_id > 0 && u.QrCodeScene_id <100000)
                {//场景值 永久二维码微信api限定为10W个
                    //qrcodeurl = WeiXinBusiness.GetQrCodeImgUrl(u.QrCodeScene_id);
                    string m_fileName = string.Format(@"QrCodeScene_id_{0}.jpg", u.QrCodeScene_id);
                    string m_saveName = "../../images/qrcode/" + m_fileName;
                    string m_savePath = Server.MapPath(m_saveName);


                    if (!File.Exists(m_savePath))
                    {

                        MemoryStream ms = WeiXinBusiness.GetQrCodeImgStream(u.QrCodeScene_id);
                        System.Drawing.Image bm = System.Drawing.Image.FromStream(ms, true);
                        try
                        {
                            bm.Save(m_savePath);
                            //ltlTips.Text = "获取成功，并保存到服务器";
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLogError(typeof(CreateQrCodeUI), ex);
                            //ltlTips.Text = "获取失败";
                        }
                        finally
                        {
                            ms.Close();
                            ms.Dispose();
                        }
                    }
                    else
                    {
                        //ltlTips.Text = "已存在图片，直接读取服务器图片";
                    }
                    ImgUrl = m_saveName;



                }else
                {
                    Response.Write("二维码场景值不正确或者用户不存在,请联系管理员");
                }
            }
        }
    }
}