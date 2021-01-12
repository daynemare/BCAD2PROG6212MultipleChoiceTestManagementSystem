using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using System.Data.SQLite;
using System.Data;
using Dapper;


namespace DataAccessLayer.ConnectedLayer
{
    public class TestResult
    {
        public void InsertTestToBeTaken(ModelStudentResult sr, string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var cmd = cnn.Query<ModelStudentResult>("Insert Into STUDENT_RESULT(STUD_USERNAME,STUD_FNAME,STUD_LNAME,TEST_NAME,TEST_RESULT)Values" + $"('{sr.STUD_USERNAME}','{sr.STUD_FNAME}','{sr.STUD_LNAME}','{sr.TEST_NAME}','{sr.TEST_RESULT}')", new DynamicParameters());
            }

        }

        public void UpdateStudentResult(ModelStudentResult sr, string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var cmd = cnn.Query<ModelStudentResult>("Update STUDENT_RESULT set TEST_RESULT = '"+sr.TEST_RESULT+"' where STUD_USERNAME = '"+sr.STUD_USERNAME+ "' and TEST_NAME = '" + sr.TEST_NAME + "'", new DynamicParameters());
            }


        }

        public List<ModelStudentResult> GetClassTestList(string cs,string test)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelStudentResult>("SELECT * FROM STUDENT_RESULT WHERE TEST_NAME ='"+test+"'", new DynamicParameters());
                return list.ToList();
            }
        }

        public List<ModelStudentResult> GetStudentTestsToDo(string cs, int user)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelStudentResult>("SELECT TEST_NAME FROM STUDENT_RESULT WHERE STUD_USERNAME ='" + user + "' AND TEST_RESULT = 'Test not done'", new DynamicParameters());
                return list.ToList();
            }
        }

        public List<ModelStudentResult> GetTestReport(string cs, int user)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelStudentResult>("SELECT TEST_NAME,TEST_RESULT FROM STUDENT_RESULT WHERE STUD_USERNAME ='" + user + "'", new DynamicParameters());
                return list.ToList();
            }
        }
    }
}
