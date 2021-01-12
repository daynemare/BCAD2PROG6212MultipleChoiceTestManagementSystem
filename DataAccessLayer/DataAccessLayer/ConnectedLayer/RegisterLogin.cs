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
{//This class perfroms Sql operations relating to Login and Registration data
    public class RegisterLogin
     {
        public void InsertLecturerRegData(ModelRegisterLogin lecturer, string cs)
        {        
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var cmd = cnn.Query<ModelRegisterLogin>("Insert Into LECTURER_LOGIN(LECT_USERNAME, LECT_PASSWORD,LECT_FNAME,LECT_LNAME) Values" + $"('{lecturer.LECT_USERNAME}','{lecturer.LECT_PASSWORD}','{lecturer.LECT_FNAME}','{lecturer.LECT_LNAME}')", new DynamicParameters());
            }

        }

        public void InsertStudentRegData(ModelRegisterLogin student, string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var cmd = cnn.Query<ModelRegisterLogin>("Insert Into STUDENT_LOGIN(STUD_USERNAME, STUD_PASSWORD,STUD_FNAME,STUD_LNAME) Values" + $"('{student.STUD_USERNAME}','{student.STUD_PASSWORD}','{student.STUD_FNAME}','{student.STUD_LNAME}')", new DynamicParameters());
            }

        }
        
        public List<ModelRegisterLogin> GetAllFromLecturerLogin(string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelRegisterLogin>("SELECT * FROM LECTURER_LOGIN", new DynamicParameters());
                return list.ToList();
            }
        }

        public List<ModelRegisterLogin> GetAllFromStudentLogin(string cs)
        {
            using (IDbConnection cnn = new SQLiteConnection(cs))
            {
                var list = cnn.Query<ModelRegisterLogin>("SELECT * FROM STUDENT_LOGIN", new DynamicParameters());
                return list.ToList();
            }
        }

    }
}
