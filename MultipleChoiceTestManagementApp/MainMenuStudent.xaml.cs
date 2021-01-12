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

    public partial class MainMenuStudent : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        private int currentUser;

        public MainMenuStudent(int user)
        {
            InitializeComponent();
            currentUser = user;
        }
        //This window load event displays the current using the lbCurrentUser label 
        private void WinMainMenuStudent_Loaded(object sender, RoutedEventArgs e)
        {
            lbCurrentUser.Content = currentUser;
            TestResult tr = new TestResult();
            List<ModelStudentResult> tests = tr.GetStudentTestsToDo(connectionString, currentUser);
            if(tests.Count == 0)
            {
                btTakeTest.IsEnabled = false;
                lblTestsDone.Visibility = Visibility.Visible;
                btTakeTest.Background = Brushes.Gray;
            }
        }
        //Calls the sign out method from the lecturer main menu class
        private void BtSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainMenuLecturer mml = new MainMenuLecturer(currentUser);
            mml.SignOut();
            this.Close();

        }
        //Opens the TakeATest window for the student 
        private void BtTakeTest_Click(object sender, RoutedEventArgs e)
        {
            TakeATest tt = new TakeATest(currentUser);
            tt.Show();
            this.Close();
        }
        
        private void BtTestReport_Click(object sender, RoutedEventArgs e)
        {
            TestReport tr = new TestReport(currentUser);
            tr.Show();
            this.Close();
        }
    }
}
