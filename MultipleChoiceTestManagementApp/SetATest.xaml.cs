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
//Declaration of the data access dll
using DataAccessLayer.Models;
using DataAccessLayer.ConnectedLayer;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace MultipleChoiceTestManagementApp
{
    
    public partial class SetATest : Window
    {
        //gets the connection string for the purpose of passing it through to the data access layer dll
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        private int currentUser;
        
        
        public SetATest(int user)
        {
            InitializeComponent();
            currentUser = user;
        }

        MultipleChoice test = new MultipleChoice();
        TestOp mt = new TestOp();
        TestResult tr = new TestResult();
        List<MultipleChoice> CurrentTestQuestions = new List<MultipleChoice>();
      
        RegisterLogin rl = new RegisterLogin();
        int QuestionCounter = 1;

        //This method clears all the fields 
        public void ClearAll()
        {
            rtQuestion.Document.Blocks.Clear();
            tbA.Text = "";
            tbB.Text = "";
            tbC.Text = "";
            tbD.Text = "";
            combCorrectAns.SelectedIndex = 0;

        }
        //this method uses a List <T> of the type MultipleChoice to load data into the Test table through a ModelTest object 
        public void SubmitTestToDb()
        {
            int i = 1;
            foreach (var mTest in CurrentTestQuestions)
            {
                var testName = mTest.Name;
                var questionNumber = i;
                var question = mTest.Question;
                var optionA = mTest.OptionA;
                var optionB = mTest.OptionB;
                var optionC = mTest.OptionC;
                var optionD = mTest.OptionD;
                var correctAnswer = mTest.CorrectAnswer;

                var newTest = new ModelTest
                {
                    Test_Name = testName,
                    QuestionNumber = questionNumber,
                    Test_Question = question,
                    Option_A = optionA,
                    Option_B = optionB,
                    Option_C = optionC,
                    Option_D = optionD,
                    Correct_Answer = correctAnswer

                };
               
                mt.InsertNewTest(newTest, connectionString);
                i++;

            }

            List<ModelRegisterLogin> students = rl.GetAllFromStudentLogin(connectionString);
            foreach (var student in students)
            {
                var username = student.STUD_USERNAME;
                var fName = student.STUD_FNAME;
                var lName = student.STUD_LNAME;
                var tName = lbShowTestName.Content.ToString();
               


                var tEntry = new ModelStudentResult
                {
                    STUD_USERNAME = username,
                    STUD_FNAME = fName,
                    STUD_LNAME = lName,
                    TEST_NAME = tName,
                    TEST_RESULT = "Test not done"

                };

                tr.InsertTestToBeTaken(tEntry,connectionString);

            }


            MessageBox.Show("Test " + lbShowTestName.Content + " was submitted successfully\nStudents may now take it!", "Test Saved Successfully");
            MainMenuLecturer mml = new MainMenuLecturer(currentUser);
            mml.Show();
            this.Hide();

        }

        //This button click event returns the user to the lecturer dashboard
        private void BtMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenuLecturer mml = new MainMenuLecturer(currentUser);
         
            mml.Show();
            this.Close();

        }
        //This button click event sets the test name and checks if a test with the same name already exists with in the db
        private void BtSetTestName_Click(object sender, RoutedEventArgs e)
        {
            TestOp tn = new TestOp();
           
            List<ModelTest> testData = tn.GetAllTests(connectionString);

            var mt = testData.Exists((ModelTest x) => x.Test_Name == tbTestName.Text);
            
            if (mt == true)
            {
                MessageBox.Show("Test with the same name already exists,\nPlease type in a different name");
                tbTestName.Text = "";
            }
            else if (tbTestName.Text.Equals(""))
            {
                MessageBox.Show("Test name field can not be empty/nPlease enter a name for the test");
            }
            else
            {
                MessageBoxResult confirm = MessageBox.Show("Are you sure you want to name the test\n" + tbTestName.Text + "?", "Confirm Test Name", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    test.Name = tbTestName.Text;
                    tbTestName.Visibility = Visibility.Hidden;
                    lbNotify.Visibility = Visibility.Hidden;
                    lbShowTestName.Content = test.Name;
                    lbShowTestName.Visibility = Visibility.Visible;
                    btSaveQuestion.IsEnabled = true;
                    btClearAll.IsEnabled = true;
                    tbA.IsEnabled = true;
                    tbB.IsEnabled = true;
                    tbC.IsEnabled = true;
                    tbD.IsEnabled = true;
                    btSubmitTest.IsEnabled = true;
                    combCorrectAns.IsEnabled = true;
                    rtQuestion.IsEnabled = true;
                    btSetTestName.Visibility = Visibility.Hidden;
                }
                else
                {
                    tbTestName.Text = "";
                }

            }
        }
        
        private void WinSetTest_Loaded(object sender, RoutedEventArgs e)
        {
            test.QuestionNumber = Convert.ToInt32(lbQuestionNumber.Content.ToString());

        }
        //This button click event adds or overwrites new question data from the current input fields using a List <T> of the type MultipleChoice
        private void BtSaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            MultipleChoice ques = new MultipleChoice();
            ques.Question = new TextRange(rtQuestion.Document.ContentStart, rtQuestion.Document.ContentEnd).Text;

            if (ques.Question.Equals("") || tbA.Text.Equals("") || tbB.Text.Equals("") || tbC.Text.Equals("") || tbD.Text.Equals("") || combCorrectAns.SelectedIndex == 0)
            {
                MessageBox.Show("Please ensure all question fields are filled before saving a question");
            }
            else
            {
                ques.Name = test.Name;
                ques.OptionA = tbA.Text;
                ques.OptionB = tbB.Text;
                ques.OptionC = tbC.Text;
                ques.OptionD = tbD.Text;

                ques.CorrectAnswer = combCorrectAns.Text;

                if (CurrentTestQuestions.Count >= QuestionCounter)
                {
                    CurrentTestQuestions.RemoveAt(Convert.ToInt32(lbQuestionNumber.Content) - 1);
                }
                try
                {
                    CurrentTestQuestions.Insert(Convert.ToInt32(lbQuestionNumber.Content) - 1, ques);
                }
                catch (Exception)
                {
                    CurrentTestQuestions.Add(ques);
                }

                MessageBox.Show("Question " + test.QuestionNumber + " Saved Successfully!\nClick [Add New Question] to add another question, or [Submit Test] if you are finished", "Success!");

                if (Convert.ToInt32(lbQuestionNumber.Content) == QuestionCounter)
                {
                    btNewQuestion.IsEnabled = true;
                }
               

            }
        }
        //This button click event allows the lecturer to add another question to the test
        private void BtNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            btPrevQuest.IsEnabled = true;
            lbQuestionNumber.Content = test.NextQuestion().ToString();
            test.QuestionNumber = Convert.ToInt32(lbQuestionNumber.Content);
            btNewQuestion.IsEnabled = false;
            test.QuestionCounter();
            QuestionCounter++;
        }
        //This button click event decrements the current question number value and populates the input fields with data from the previously saved question stored in the CurrentTestQuestions List <T> of the type MultipleChoice
        private void BtPrevQuest_Click(object sender, RoutedEventArgs e)
        {

            if(CurrentTestQuestions.Count < Convert.ToInt32(lbQuestionNumber.Content))
            {
                MessageBox.Show("Please save the current question before viewing other questions", "Error");
            }
            else
            {
                MultipleChoice ques = new MultipleChoice();
                ques.Question = new TextRange(rtQuestion.Document.ContentStart, rtQuestion.Document.ContentEnd).Text;


                btNextQuest.IsEnabled = true;

                lbQuestionNumber.Content = test.PreviousQuestion().ToString();
                test.QuestionNumber = Convert.ToInt32(lbQuestionNumber.Content);

                tbA.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionA;
                tbB.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionB;
                tbC.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionC;
                tbD.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionD;
                rtQuestion.Document.Blocks.Clear();
                rtQuestion.Document.Blocks.Add(new Paragraph(new Run(CurrentTestQuestions[test.QuestionNumber - 1].Question)));

                switch (CurrentTestQuestions[test.QuestionNumber - 1].CorrectAnswer)
                {
                    case "A":
                        combCorrectAns.SelectedIndex = 1;
                        break;
                    case "B":
                        combCorrectAns.SelectedIndex = 2;
                        break;
                    case "C":
                        combCorrectAns.SelectedIndex = 3;
                        break;
                    case "D":
                        combCorrectAns.SelectedIndex = 4;
                        break;

                }
            }

           

            if (Convert.ToInt32(lbQuestionNumber.Content) != QuestionCounter)
            {
                btNewQuestion.IsEnabled = false;
            }


            if (lbQuestionNumber.Content.ToString().Equals("1"))
            {
                btPrevQuest.IsEnabled = false;
                btNextQuest.IsEnabled = true;
            }



        }
        //This button click event increments the current question number value and populates the input fields with data from the next saved question stored in the CurrentTestQuestions List <T> of the type MultipleChoice
        private void BtNextQuest_Click(object sender, RoutedEventArgs e)
        {
            lbQuestionNumber.Content = test.NextQuestion().ToString();
            test.QuestionNumber = Convert.ToInt32(lbQuestionNumber.Content);
            btPrevQuest.IsEnabled = true;

            if (Convert.ToInt32(lbQuestionNumber.Content) == CurrentTestQuestions.Count)
            {
                btNewQuestion.IsEnabled = true;
            }

            if (test.QuestionNumber > CurrentTestQuestions.Count)
            {
                ClearAll();
                btNextQuest.IsEnabled = false;
            }
            else
            {
                tbA.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionA;
                tbB.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionB;
                tbC.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionC;
                tbD.Text = CurrentTestQuestions[test.QuestionNumber - 1].OptionD;
                rtQuestion.Document.Blocks.Clear();
                rtQuestion.Document.Blocks.Add(new Paragraph(new Run(CurrentTestQuestions[test.QuestionNumber - 1].Question)));

                switch (CurrentTestQuestions[test.QuestionNumber - 1].CorrectAnswer)
                {
                    case "A":
                        combCorrectAns.SelectedIndex = 1;
                        break;
                    case "B":
                        combCorrectAns.SelectedIndex = 2;
                        break;
                    case "C":
                        combCorrectAns.SelectedIndex = 3;
                        break;
                    case "D":
                        combCorrectAns.SelectedIndex = 4;
                        break;

                }

                if (test.QuestionNumber == QuestionCounter)
                {
                    btNextQuest.IsEnabled = false;
                }

            }

        }
        //Button click event that clears all the input fields
        private void BtClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }
        //Click event ensures there is at least 1 question saved before attempting to submit test entries to the Test table with in the database.
        private void BtSubmitTest_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTestQuestions.Count == 0)
            {
                MessageBox.Show("Please save a question before attempting to submit", "No Saved Questions Found");
            }
            else
            {
                SubmitTestToDb();
            
            }

        }


    }
}


