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
    /// <summary>
    /// Interaction logic for ClassTestLists.xaml
    /// </summary>
    public partial class ClassTestLists : Window
    {
        private static int currentUser;
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        private string testName;
        TestResult tr = new TestResult();

        public ClassTestLists(int cu)
        {
            InitializeComponent();
            currentUser=cu;
        }

        public void PopulateComboBox()
        {
            TestOp to = new TestOp();

            List<ModelTest> Tests = new List<ModelTest>();
           
            Tests = to.GetAllTests(connectionString);

            foreach (var test in Tests)
            {
                if (!comboChooseTest.Items.Contains(test.Test_Name))
                {
                    comboChooseTest.Items.Add(test.Test_Name);
                }
            }
        }

        private void BtToMain_Click(object sender, RoutedEventArgs e)
        {
            MainMenuLecturer mml = new MainMenuLecturer(currentUser);
            mml.Show();
            this.Close();
        }

        private void FrmClassTest_Loaded(object sender, RoutedEventArgs e)
        {
            comboChooseTest.Items.Add("--Please Select--");
            PopulateComboBox();
        }

        private void BtChooseTest_Click(object sender, RoutedEventArgs e)
        {
            dgClassList.Items.Clear();
          
            testName = comboChooseTest.SelectedValue.ToString();

            List<ModelStudentResult> list = tr.GetClassTestList(connectionString,testName);

            lbNotify.Content = "";
            
            
            foreach (var rec in list)
            {
                DataGridBind dgb = new DataGridBind
                {
                    STUD_USERNAME = rec.STUD_USERNAME,
                    STUD_FNAME = rec.STUD_FNAME,
                    STUD_LNAME = rec.STUD_LNAME,
                    TEST_NAME = rec.TEST_NAME,
                    TEST_RESULT = rec.TEST_RESULT

                };


                dgClassList.Items.Add(dgb);
            }
        }

    }

        public class DataGridBind
    {
        public int STUD_USERNAME { get; set; }
        public string STUD_FNAME { get; set; }
        public string STUD_LNAME { get; set; }
        public string TEST_NAME { get; set; }
        public string TEST_RESULT { get; set; }

    }
}
