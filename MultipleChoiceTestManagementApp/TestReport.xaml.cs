using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccessLayer.Models;
using DataAccessLayer.ConnectedLayer;
using System.Configuration;


namespace MultipleChoiceTestManagementApp
{
   
    public partial class TestReport : Window
    {
        private static int currentUser;
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        TestResult tr = new TestResult();

        public TestReport(int cu)
        {
            InitializeComponent();
            currentUser = cu;
        }

        private void TakeTestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<ModelStudentResult> list = tr.GetTestReport(connectionString,currentUser);

            foreach (var rec in list)
            {
                DataGridBind dgb = new DataGridBind
                {
                    TEST_NAME = rec.TEST_NAME,
                    TEST_RESULT = rec.TEST_RESULT
                };

                dgClassList.Items.Add(dgb);
            }
        }

        private void BtToMain_Click(object sender, RoutedEventArgs e)
        {
            MainMenuStudent mms = new MainMenuStudent(currentUser);
            mms.Show();
            this.Close();
        }
    }

}
