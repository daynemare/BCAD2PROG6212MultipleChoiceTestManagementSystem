using System.Windows;
using System.Configuration;
using DataAccessLayer.Models;
using DataAccessLayer.ConnectedLayer;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace MultipleChoiceTestManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        RegisterLogin cl = new RegisterLogin();

        public MainWindow()
        {
            InitializeComponent();

        }

        public void LoginLecturer()
        {
            ModelRegisterLogin lect = new ModelRegisterLogin();
            

            List<ModelRegisterLogin> list = cl.GetAllFromLecturerLogin(connectionString);

            try
            {
                lect = list.Find((ModelRegisterLogin x) => x.LECT_USERNAME == tbUsernameLog.Text);

                if (lect.LECT_USERNAME.ToString().Equals(tbUsernameLog.Text) && lect.LECT_PASSWORD.ToString().Equals(tbPasswordLog.Password))
                {
                    MessageBox.Show("Login Successful !,\n Welcome LECTURER " + lect.LECT_USERNAME.ToString());

                    MainMenuLecturer mml = new MainMenuLecturer();
                    mml.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                    tbUsernameLog.Text = "";
                    tbPasswordLog.Password = "";
                }


            }
            catch (System.Exception)
            {
                MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                tbUsernameLog.Text = "";
                tbPasswordLog.Password = "";
            }
        }


        public void LoginStudent()
        {
            ModelRegisterLogin stud = new ModelRegisterLogin();

            List<ModelRegisterLogin> list = cl.GetAllFromStudentLogin(connectionString);
        
            try
            {
                stud = list.Find((ModelRegisterLogin x) => x.STUD_USERNAME == tbUsernameLog.Text);

                if (stud.STUD_USERNAME.ToString().Equals(tbUsernameLog.Text) && stud.STUD_PASSWORD.ToString().Equals(tbPasswordLog.Password))
                {
                    MessageBox.Show("Login Successful !,\n Welcome LECTURER " + stud.STUD_USERNAME.ToString());
                }
                else
                {
                    MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                    tbUsernameLog.Text = "";
                    tbPasswordLog.Password = "";
                }


            }
            catch (System.Exception)
            {
                MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                tbUsernameLog.Text = "";
                tbPasswordLog.Password = "";
            }
        }

        public void RegisterNewLecturer()
        {
            try
            {
                var login = tbUsername.Text;
                var password = tbPassword.Password;

                var c = new ModelRegisterLogin
                {
                    LECT_USERNAME = login,
                    LECT_PASSWORD = password
                };


                cl.InsertLecturerRegData(c, connectionString);
                MessageBox.Show($"Registration Successful!" + "\nYou may now Login, LECTURER " + tbUsername.Text + " !");
                tabLoginRegister.SelectedIndex = 1;
            }
            catch (System.Exception)
            {
                MessageBox.Show("User already exists\nPlease try again", "Error");
                tbUsername.Text = "";
                tbPassword.Password = "";

            }
        }


        public void RegisterNewStudent()
        {
            try
            {
                var login = tbUsername.Text;
                var password = tbPassword.Password;

                var c = new ModelRegisterLogin
                {
                    STUD_USERNAME = login,
                    STUD_PASSWORD = password
                };

                cl.InsertStudentRegData(c, connectionString);
                MessageBox.Show($"Registration Successful!" +
                  "\nYou may now Login, STUDENT " + tbUsername.Text + " !");
                tabLoginRegister.SelectedIndex = 1;
            }
            catch (System.Exception)
            {
                MessageBox.Show("User already exists\nPlease try again", "Error");
                tbUsername.Text = "";
                tbPassword.Password = "";
            }
        }

        public void ValidateAndInputNewLecturer()
        {
                if (tbUsername.Text.Length == 0 || tbPassword.Password.Length == 0)
                {
                    MessageBox.Show("Fields can not be empty,\n Please fill in a username and password.", "Error");

                }
                else if (tbUsername.Text.Length < 5 || tbPassword.Password.Length < 5)
                {
                    MessageBox.Show("Username and password must be at least 5 characters in length,\n Please try again.", "Error");

                }               
                else
                {
                         RegisterNewLecturer();
                   
                }
            
        }

        public void ValidateAndInputNewStudent()
        {
            if (tbUsername.Text.Length == 0 || tbPassword.Password.Length == 0)
            {
                MessageBox.Show("Fields can not be empty,\n Please fill in a username and password.", "Error");

            }
            else if (tbUsername.Text.Length < 5 || tbPassword.Password.Length < 5)
            {
                MessageBox.Show("Username and password must be at least 5 characters in length,\n Please try again.", "Error");

            }
            else
            {
                    RegisterNewStudent();
             
            }
            

        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (cbLecturerReg.IsChecked.Value==true)
            {
                ValidateAndInputNewLecturer();
            }
            else 
            {
                ValidateAndInputNewStudent();
             }
    
        }

        private void CbLecturerReg_Checked(object sender, RoutedEventArgs e)
        {
            cbStudentReg.IsChecked = false;
        }

        private void CbStudentReg_Checked(object sender, RoutedEventArgs e)
        {
            cbLecturerReg.IsChecked = false;
        }

        private void CbLecturerLog_Checked(object sender, RoutedEventArgs e)
        {
            cbStudentLog.IsChecked = false;
        }

        private void CbStudentLog_Checked(object sender, RoutedEventArgs e)
        {
            cbLecturerLog.IsChecked = false;
        }

        private void BtSaveLog_Click(object sender, RoutedEventArgs e)
        {
            if (cbLecturerLog.IsChecked.Value == true)
            {
                LoginLecturer();
            }
            else
            {
                LoginStudent();
            }
        }
    }
}
