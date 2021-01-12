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
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace MultipleChoiceTestManagementApp
{
    public partial class TakeATest : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DataAccessLayer"].ConnectionString;
        private TestOp to = new TestOp();
        private List<ModelTest> testQuestions = new List<ModelTest>();
        private List<string> studentAnswers = new List<string>();
        private int questionNumber = 1 ;
        private List<int> savedQuestions = new List<int>();
        //Declaration of the delegate called PerformCalculations
        delegate double PerformCalculations(double x, double y);
        private TestResult tr = new TestResult();
        private string nameTest;
        
       
        private bool testComplete = false;
        private int currentUser;
        
        public void SaveResult(int user, string test, double mark)
        {
            var username = user;
            var result = mark.ToString();
            var testName = test;

            try
            {
                var r = new ModelStudentResult
                {
                    STUD_USERNAME = user,
                    TEST_NAME = testName,
                    TEST_RESULT = result

                };

                tr.UpdateStudentResult(r, connectionString);
            }
            catch (Exception)
            {
                MessageBox.Show("Update Failed");
            }
          
        }

        public TakeATest(int user)
        {
            InitializeComponent();
            currentUser = user;
        }
        //this method sets the comparison data side to side in labels of color. A green label if the users answer matches the correct answer and red if not.
        public void SetMemo()
        {

            if (testComplete == true)
            {
                if (testQuestions[questionNumber-1].Correct_Answer.Equals(studentAnswers[questionNumber-1]))
                {
                    lbCorrectAnswer.Content = testQuestions[questionNumber-1].Correct_Answer;
                    lbStudentAnswer.Content = studentAnswers[questionNumber-1];
                    lbStudentAnswer.Foreground = Brushes.Lime;
                    lbStudAnsHeader.Foreground = Brushes.Lime;

                }
                else
                {
                    lbCorrectAnswer.Content = testQuestions[questionNumber-1].Correct_Answer;
                    lbStudentAnswer.Content = studentAnswers[questionNumber-1];
                    lbStudentAnswer.Foreground = Brushes.Red;
                    lbStudAnsHeader.Foreground = Brushes.Red;
                }
            }
            
        }
        //this method is used by the delegate declared above to work out the students mark as a percentage
        public double CalculateMarkPercentage(double x, double y)
        {
            double result = Math.Round( x / y * 100);
            return result;
        }
        //This method uses a switch case to identify and populate the studentAnswers list <T> with the answer declared by the student. 
        public void SetStudentAnswers()
        {
            switch (combCorrectAns.SelectedIndex)
            {
                case 1:
                    if (savedQuestions.Contains(questionNumber) && testComplete==false)
                    {
                        studentAnswers.RemoveAt(questionNumber - 1);
                        studentAnswers.Insert(questionNumber - 1, "A");
                    }
                    else
                    {
                        studentAnswers.Add("A");
                        savedQuestions.Add(questionNumber);
                    }
                    break;
                case 2:
                    if (savedQuestions.Contains(questionNumber) && testComplete == false)
                    {
                        studentAnswers.RemoveAt(questionNumber - 1);
                        studentAnswers.Insert(questionNumber - 1, "B");
                    }
                    else
                    {
                        studentAnswers.Add("B");
                        savedQuestions.Add(questionNumber);
                    }
                    break;
                case 3:
                    if (savedQuestions.Contains(questionNumber) && testComplete == false)
                    {
                        studentAnswers.RemoveAt(questionNumber - 1);
                        studentAnswers.Insert(questionNumber - 1, "C");
                    }
                    else
                    {
                        studentAnswers.Add("C");
                        savedQuestions.Add(questionNumber);
                    }
                    break;
                case 4:
                    if (savedQuestions.Contains(questionNumber) && testComplete == false)
                    {
                        studentAnswers.RemoveAt(questionNumber - 1);
                        studentAnswers.Insert(questionNumber - 1, "D");
                    }
                    else
                    {
                        studentAnswers.Add("D");
                        savedQuestions.Add(questionNumber);
                    }
                    break;
            }
        }
        //This method displays displays a question and its options to a student based on the current question number
        public void DisplayCurrentQuestion(int i)
        {
            SetATest st = new SetATest(currentUser);
            st.ClearAll();
            rtQuestion.Document.Blocks.Clear();
            rtQuestion.Document.Blocks.Add(new Paragraph(new Run(testQuestions[i-1].Test_Question)));
            tbA.Text = testQuestions[i-1].Option_A;
            tbB.Text = testQuestions[i-1].Option_B;
            tbC.Text = testQuestions[i-1].Option_C;
            tbD.Text = testQuestions[i-1].Option_D;
   
        }

        public void DetermineCurrentAnswerState(int i)
        {
            if (studentAnswers.Count < questionNumber)
            {
                combCorrectAns.SelectedIndex = 0;
            }
            else
            {
                switch (studentAnswers[i-1])
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
        }

        private void WinTakeATest_Loaded(object sender, RoutedEventArgs e)
        {
            comboChooseTest.Items.Add("--Please Select--");
            PopulateComboBox();
        }
        //populates the choose a test combo box with all the tests that exist with in the database
        public void PopulateComboBox()
        {
            TestResult tr = new TestResult();
            List<ModelStudentResult> tests = tr.GetStudentTestsToDo(connectionString,currentUser);

            foreach (var test in tests)
            {
                if (!comboChooseTest.Items.Contains(test.TEST_NAME))
                {
                    comboChooseTest.Items.Add(test.TEST_NAME);
                }
            }
        

        }
        //this button click event sets the current test to be done, enables disabled components and displays the first question of the selected test to the student
        private void BtChooseTest_Click(object sender, RoutedEventArgs e)
        {
            if (comboChooseTest.SelectedIndex == 0)
            {
                MessageBox.Show("Please choose a test","Error");
            }
            else
            {

                lbShowTestName.Content = comboChooseTest.SelectedValue.ToString();
                nameTest = comboChooseTest.SelectedValue.ToString();
                comboChooseTest.Visibility = Visibility.Hidden;
                lbShowTestName.Visibility = Visibility.Visible;
                lbNotify.Visibility = Visibility.Hidden;
                rtQuestion.IsEnabled = true;
                tbA.IsEnabled = true;
                tbB.IsEnabled = true;
                tbC.IsEnabled = true;
                tbD.IsEnabled = true;
                combCorrectAns.IsEnabled = true;
               
                btChooseTest.Visibility = Visibility.Hidden;
                testQuestions = to.GetAllTestQuestions(lbShowTestName.Content.ToString(), connectionString);
                lbQuestionTotal.Content = " / " + testQuestions.Count.ToString();


                DisplayCurrentQuestion(questionNumber);
                if (testQuestions.Count != 1)
                {
                    btNextQuest.IsEnabled = true;
                }
                else
                {
                    btSubmitTest.IsEnabled = true;
                }
                
               
            }
           
        }
        //Displays the next answered question of the student and its related question
        private void BtNextQuest_Click(object sender, RoutedEventArgs e)
        {

            if (combCorrectAns.SelectedIndex == 0)
            {
                MessageBox.Show("Please pick an answer before proceeding to the next question", "Error");
            }
            else
            {
                SetStudentAnswers();
                questionNumber++;
                DisplayCurrentQuestion(questionNumber);
                DetermineCurrentAnswerState(questionNumber);
                btPrevQuest.IsEnabled = true;
                lbQuestionNumber.Content = questionNumber.ToString();

                if (questionNumber == testQuestions.Count)
                {
                    btNextQuest.IsEnabled = false;
                }
            }

            SetMemo();
            
        }
        //Displays the previously answered question of the student and its related question
        private void BtPrevQuest_Click(object sender, RoutedEventArgs e)
        {
            
            if (combCorrectAns.SelectedIndex == 0)
            {
                MessageBox.Show("Please pick an answer first", "Error");
            }
            else
            {
                SetStudentAnswers();
                questionNumber--;
                DisplayCurrentQuestion(questionNumber);
                DetermineCurrentAnswerState(questionNumber);
                btNextQuest.IsEnabled = true;
                lbQuestionNumber.Content = questionNumber.ToString();

                if (questionNumber == 1)
                {
                    btPrevQuest.IsEnabled = false;
                }
            }

            SetMemo();


        }
        //returns the user to the student dashboard
        private void BtMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenuStudent mms = new MainMenuStudent(currentUser);
            mms.Show();
            this.Close();

        }
        
        private void CombCorrectAns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combCorrectAns.SelectedIndex != 0 && savedQuestions.Count == testQuestions.Count-1)
            {
                btSubmitTest.Foreground = Brushes.White;
                btSubmitTest.IsEnabled = true;
            }
        }
        //calculates the students final results and shows them their final mark as well as the memorandum for the test they just took
        private void BtSubmitTest_Click(object sender, RoutedEventArgs e)
        {
            if (combCorrectAns.SelectedIndex == 0)
            {
                MessageBox.Show("Please select your final answer before clicking the Submit Test button","Error");
            }
            else
            {
                btSubmitTest.Visibility = Visibility.Hidden;
                int studentMark = 0;
                btPrevQuest.IsEnabled = false;
                if (testQuestions.Count != 1)
                {
                    btNextQuest.IsEnabled = true;
                }
              
                brdStudAnswer.Visibility = Visibility.Visible;
                brdCorrAnswer.Visibility = Visibility.Visible;
                lbMark.Visibility = Visibility.Visible;
                tbMark.Visibility = Visibility.Visible;
                combCorrectAns.Visibility = Visibility.Hidden;
                lblCorrAnswer.Visibility = Visibility.Visible;
                lbStudentAnswer.Visibility = Visibility.Visible;
                lbCorrectAnswer.Visibility = Visibility.Visible;

                SetStudentAnswers();

                for (int i = 0; i < testQuestions.Count; i++)
                {
                    if (testQuestions[i].Correct_Answer.Equals(studentAnswers[i]))
                    {
                        studentMark++;
                    }
                }
                //Presents the use of a delegate to calculate the students mark as a percentage
                PerformCalculations calc = new PerformCalculations(CalculateMarkPercentage);
                double result = calc(studentMark, testQuestions.Count);
                tbMark.Text = studentMark + " / " + testQuestions.Count + " (" + result + " %)";
                testComplete = true;

                questionNumber = 1;
                lbQuestionNumber.Content = "1";
                DisplayCurrentQuestion(questionNumber);
                SaveResult(currentUser, nameTest, result);
                SetMemo();
                MessageBox.Show("Test Submitted Successfully!\nYou can now view the memorandum and your result.");
            }
        }

           
    }
}
