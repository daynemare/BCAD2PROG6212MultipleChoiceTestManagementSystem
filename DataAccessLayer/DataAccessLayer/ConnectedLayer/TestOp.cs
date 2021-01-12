using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using Dapper;

namespace DataAccessLayer.ConnectedLayer
{//This class perfroms Sql operations relating to test data 
    public class TestOp
    {
        public void InsertNewTest(ModelTest test, string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var cmd = cnn.Query<ModelTest>("Insert Into TEST(TEST_NAME,QUESTION_ID,TEST_QUESTION,OPTION_A,OPTION_B,OPTION_C,OPTION_D,CORRECT_ANSWER)Values" + $"('{test.Test_Name}','{test.QuestionNumber}','{test.Test_Question}','{test.Option_A}','{test.Option_B}','{test.Option_C}','{test.Option_D}','{test.Correct_Answer}')", new DynamicParameters());
            }

        }

        public List<ModelTest> GetAllTests(string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelTest>("SELECT * FROM TEST", new DynamicParameters());
                return list.ToList();
            }
        }

        public List<ModelTest> GetAllTestQuestions(string test,string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelTest>("SELECT * FROM TEST WHERE TEST_NAME =" + $"'{test}'", new DynamicParameters());
                return list.ToList();
            }
        }
    }
}
