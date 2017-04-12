using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using Module.Utils;

namespace HospitalBookWebSite.Home.Login
{
    public partial class EditPassword : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Sys_AdminUser adminUser = Sys_AdminUser.SingleOrDefault((object)CurrentUser.Id);
            if(adminUser!=null)
            {
                if(adminUser.PassWord== this.txtpwdold.Text.Trim())
                {
                    if(this.txtpwdnew.Text.Trim() == this.txtpwdnewsure.Text.Trim())
                    {
                        //update password
                        adminUser.PassWord = this.txtpwdnew.Text.Trim();
                       int r = adminUser.Update();
                        if(r>0)
                        {
                            MessageBox.Show(Page, "更改密码成功");
                        }else
                        {
                            MessageBox.Show(Page, "更改密码失败");
                        }
                    }else
                    {
                        MessageBox.Show(Page, "新密码输入不一致，请重新输入");
                    }
                }else
                {
                    MessageBox.Show(Page, "旧密码不正确，请重新输入");
                }
            }
        }
    }
}