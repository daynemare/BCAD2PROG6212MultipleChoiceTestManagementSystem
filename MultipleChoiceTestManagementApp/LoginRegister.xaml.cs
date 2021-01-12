using System.Windows;
using System.Configuration;

//DLL References declared
using DataAccessLayer.Models;
using DataAccessLayer.ConnectedLayer;

using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System;

namespace MultipleChoiceTestManagementApp
{
    
    
    public partial class LoginRegister : Window
    {   
        //Declaration of the Database connection string to be passed to the Data Access Layer DLL
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        //Declaration of a global RegisterLogin Class object - this class resides with in the Data Access Layer DLL
        private RegisterLogin cl = new RegisterLogin();
        
        public LoginRegister()
        {
            InitializeComponent();

        }

        //This method checks to see if password and username input from each textbox matches password and username data stored in the LecturerLogin table of the application database.  
        public void LoginLecturer()
        {
           
            ModelRegisterLogin lect = new ModelRegisterLogin();

            //Generic collections List <T> is used to hold all the lecturer login details for processing. The list is instantiated using a method that retrieves all the login details from the application database, LecturerLogin. 
            List<ModelRegisterLogin> list = cl.GetAllFromLecturerLogin(connectionString);

        //A lambda expression combined with a try catch is used to determines if the users username input taken from the login texbox matchs any username objects of the Model Class ModelRegisterLogin - this Model Class holds properties for both the lecturer and student user types.
            try
            {   
                lect = list.Find((ModelRegisterLogin x) => x.LECT_USERNAME == Int32.Parse(tbUsernameLog.Text));
                //The lambda expression above checks if any dataabase username data matches the user input from the username texbox.
                if (lect.LECT_USERNAME.ToString().Equals(tbUsernameLog.Text) && lect.LECT_PASSWORD.ToString().Equals(tbPasswordLog.Password))
                {
                    MessageBox.Show("Login Successful !\nWelcome LECTURER " + lect.LECT_USERNAME.ToString());
                    ;
                    MainMenuLecturer mml = new MainMenuLecturer(Int32.Parse(tbUsernameLog.Text));
                    mml.Show();
                    this.Close();
                }
                else
                {   //if a username is found but the password doesnt match then this message will be displayed to the user 
                    MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                    tbUsernameLog.Text = "";
                    tbPasswordLog.Password = "";
                }


            }
            catch (System.Exception)
            {   //If no username match is found the application will throw an error which indicates that no such data with in the database matches that of the user data.
                //If this is so then the application displays the following message. 
                MessageBox.Show("Incorrect username or password\nPlease recheck your credentials and try again", "Error");
                tbUsernameLog.Text = "";
                tbPasswordLog.Password = "";
            }
        }
        //Does the same as the above method but for the student user type
        public void LoginStudent()
        {
            ModelRegisterLogin stud = new ModelRegisterLogin();

            List<ModelRegisterLogin> list = cl.GetAllFromStudentLogin(connectionString);
        
            try
            {
                stud = list.Find((ModelRegisterLogin x) => x.STUD_USERNAME == Int32.Parse(tbUsernameLog.Text));

                if (stud.STUD_USERNAME.ToString().Equals(tbUsernameLog.Text) && stud.STUD_PASSWORD.ToString().Equals(tbPasswordLog.Password))
                {
                    MessageBox.Show("Login Successful !\nWelcome STUDENT " + stud.STUD_USERNAME.ToString());
                    MainMenuStudent ms = new MainMenuStudent(Int32.Parse(tbUsernameLog.Text));
                    ms.Show();
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
        //This method inputs new user data into the database.
        public void RegisterNewLecturer()
        {   //a try catch is used to determine if database username data matches the users input. If the pk constraint throws an error it signals that a username with the same name already exists and reflects this to the user in a message.

            try
            {
                    var login = Int32.Parse(tbUsername.Text);
                    var password = tbPassword.Password;
                    var fName = tbFName.Text;
                    var lName = tbLName.Text;

                    var c = new ModelRegisterLogin
                    {
                        LECT_USERNAME = login,
                        LECT_PASSWORD = password,
                        LECT_FNAME = fName,
                        LECT_LNAME = lName

                    };
                    //The InsertLecturerRegData method from thr data access layer ddl performs a sql query that inserts the user input into the LecturerLogin table within the application database.
                    cl.InsertLecturerRegData(c, connectionString);
                    MessageBox.Show($"Registration Successful!" + "\nYou may now Login LECTURER " + tbUsername.Text + " !");
                    tabLoginRegister.SelectedIndex = 1;
 
            }
            catch (System.Exception)
            {
                MessageBox.Show("User already exists or the input for LECTURER NUMBER is not a number\nPlease try again", "Error");
                tbUsername.Text = "";
                tbPassword.Password = "";

            }
        }
        //Does the same as the above method but for the student user type
        public void RegisterNewStudent()
        {
            try
            {
                var login = Int32.Parse(tbUsername.Text);
                var password = tbPassword.Password;
                var fName = tbFName.Text;
                var lName = tbLName.Text;

                var c = new ModelRegisterLogin
                {
                    STUD_USERNAME = login,
                    STUD_PASSWORD = password,
                    STUD_FNAME = fName,
                    STUD_LNAME = lName
                };

                cl.InsertStudentRegData(c, connectionString);
                       
                TestOp to = new TestOp();
                List<ModelTest> Tests = new List<ModelTest>();
                List<string> testToDo = new List<string>();
                Tests = to.GetAllTests(connectionString);

                foreach (var test in Tests)
                {
                    if(!testToDo.Contains(test.Test_Name))
                    {
                        testToDo.Add(test.Test_Name);
                    }
                }

                TestResult tr = new TestResult();
                foreach (var test in testToDo)
                {

                    var tName = test;

                    var tEntry = new ModelStudentResult
                    {
                        STUD_USERNAME = login,
                        STUD_FNAME = fName,
                        STUD_LNAME = lName,
                        TEST_NAME = test,
                        TEST_RESULT = "Test not done"

                    };

                    tr.InsertTestToBeTaken(tEntry, connectionString);
                }

                    MessageBox.Show($"Registration Successful!" +
                  "\nYou may now Login, STUDENT " + tbUsername.Text + " !");
                tabLoginRegister.SelectedIndex = 1;
            }
            catch (System.Exception)
            {
                MessageBox.Show("User already exists or the input for STUDENT NUMBER is not a number\nPlease try again", "Error");
                tbUsername.Text = "";
                tbPassword.Password = "";
            }
        }
        //This method ensures that the lecturer text box and password box data meets application constraints before calling the RegisterNewLecturer() method.
        public void ValidateAndInputNewLecturer()
        {
                if (tbUsername.Text.Length == 0 || tbPassword.Password.Length == 0 || tbFName.Text.Length == 0 || tbLName.Text.Length == 0)
                {
                    MessageBox.Show("Fields cannot be empty,\n Please fill in a username and password.", "Error");

                }
                else if (tbPassword.Password.Length < 5)
                {
                    MessageBox.Show("Password must be at least 5 characters in length,\n Please try again.", "Error");

                }
                else if(tbUsername.Text.Length != 8)
                {
                    MessageBox.Show("LECTURER NUMBER must be 8 characters in length,\n Please try again.", "Error");
                }
                else
                {
                         RegisterNewLecturer();
                   
                }
            
        }
        //Does the same as the above method but for the students input
        public void ValidateAndInputNewStudent()
        {
            if (tbUsername.Text.Length == 0 || tbPassword.Password.Length == 0 || tbFName.Text.Length == 0 || tbLName.Text.Length == 0)
            {
                MessageBox.Show("Fields can not be empty,\n Please fill in your student/lecturer number, password, first name and last name.", "Error");

            }
            else if (tbPassword.Password.Length < 5)
            {
                MessageBox.Show("Password must be at least 5 characters in length,\n Please try again.", "Error");

            }
            else if (tbUsername.Text.Length != 8)
            {
                MessageBox.Show("STUDENT NUMBER must be 8 characters in length,\n Please try again.", "Error");
            }
            else
            {
                    RegisterNewStudent();
             
            }
            

        }
        //Determines which user to validate based on the user type determined by check boxes.
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
        //The four following event methods simply change which user type check box is checked based on a checked event.
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

        //When the login button is clicked it determines which user login helper method to run based on which check box is checked.
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

        private void BtToWebApp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:63236/StudentLogin.aspx");
        }
    }
}
