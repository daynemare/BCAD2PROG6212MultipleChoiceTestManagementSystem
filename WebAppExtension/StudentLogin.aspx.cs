using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Models;
using DataAccessLayer.ConnectedLayer;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace WebAppExtension
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        public static string connectionString;
        public RegisterLogin cl = new RegisterLogin();

        public void StoreCookie()
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie["StudentNumber"] = tbUsername.Text;
            cookie["Password"] = tbPassword.Text;

            Response.Cookies.Add(cookie);

        }

        public void LoginStudent()
        {
            ModelRegisterLogin stud = new ModelRegisterLogin();

            List<ModelRegisterLogin> list = cl.GetAllFromStudentLogin(connectionString);

            try
            {
                stud = list.Find((ModelRegisterLogin x) => x.STUD_USERNAME == Int32.Parse(tbUsername.Text));

                if (stud.STUD_USERNAME.ToString().Equals(tbUsername.Text) && stud.STUD_PASSWORD.ToString().Equals(tbPassword.Text))
                {
                    Response.Redirect("~/PortalHome.aspx");
                    return;
                }

            }
            catch (System.Exception)
            {
                lbNotify.Text = "Incorrect username or password. Please recheck your credentials and try again";
                tbUsername.Text = "";
                tbPassword.Text = "";
            }
            lbNotify.Text = "Incorrect username or password. Please recheck your credentials and try again";
            tbUsername.Text = "";
            tbPassword.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string su = HttpRuntime.AppDomainAppPath;
            string c = Path.GetFullPath(Path.Combine(su,@"..\MultipleChoiceTestManagementApp\bin\Debug\TestApplicationDb.db"));
            AppDomain.CurrentDomain.SetData("DataDirectory", c);
           connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
           
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            StoreCookie();
            LoginStudent();
      
        }

        protected void btClearAll_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "";
            tbPassword.Text = "";
        }
    }
    
}