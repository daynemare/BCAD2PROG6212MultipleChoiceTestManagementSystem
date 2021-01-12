using System;
using System.Collections.Generic;
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
using System.IO;
using System.Configuration;

namespace WebAppExtension
{
    public partial class ViewResults : System.Web.UI.Page
    {
        public static string connectionString;
        RegisterLogin cl = new RegisterLogin();

        public void SetConnection()
        {
            string su = HttpRuntime.AppDomainAppPath;
            string c = Path.GetFullPath(Path.Combine(su, @"..\MultipleChoiceTestManagementApp\bin\Debug\TestApplicationDb.db"));
            AppDomain.CurrentDomain.SetData("DataDirectory", c);
            connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        }

        public void PopulateGridView()
        {
            TestResult tr = new TestResult();
            List<ModelStudentResult> list = tr.GetTestReport(connectionString, Convert.ToInt32(lblUser.Text.ToString()));

            DataTable dt = new DataTable();
            dt.Columns.Add("Test Name");
            dt.Columns.Add("Result(%)");

            for (int i = 0; i < list.Count; i++)
            {
                dt.Rows.Add(list[i].TEST_NAME, list[i].TEST_RESULT);

            }

            dgClassList.DataSource = dt;
            dgClassList.DataBind();

        }

        public void RequestCookies()
        {
            HttpCookie cookie = Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                lblUser.Text = cookie["StudentNumber"];

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetConnection();
            RequestCookies();
            PopulateGridView();
   
        }
    }
}