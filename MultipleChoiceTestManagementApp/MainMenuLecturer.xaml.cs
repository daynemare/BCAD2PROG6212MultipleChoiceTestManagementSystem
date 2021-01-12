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

namespace MultipleChoiceTestManagementApp
{
    /// <summary>
    /// Interaction logic for MainMenuLecturer.xaml
    /// </summary>
    public partial class MainMenuLecturer : Window
    {
        private static int currentUser;
        
        public MainMenuLecturer(int lect)
        {
            InitializeComponent();
            currentUser = lect;
        }
        //Method that ensures the user really wants to sign out. If they click yes it will show the LoginRegister window else it will return them to the previous menu
        public void SignOut()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to sign out?", "Confirmation", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                LoginRegister mw = new LoginRegister();
                mw.Show();
                this.Close();
            }
        }
        //button click that opens the SetATest window for the user lecturer
        private void BtSetTest_Click(object sender, RoutedEventArgs e)
        {

            SetATest st = new SetATest(currentUser);
            st.Show();
            this.Close();
        }
        //This windows load event populates the label lbCurrentUser with the current users username
        private void WinMainMenuLecturer_Loaded(object sender, RoutedEventArgs e)
        {
           
            lbCurrentUser.Content = currentUser;
        }
        //button click event that calls the SignOut method
        private void BtSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignOut();
        }

        private void BtViewList_Click(object sender, RoutedEventArgs e)
        {
            ClassTestLists ctl = new ClassTestLists(currentUser);
            ctl.Show();
            this.Close();
        }
    }
}
