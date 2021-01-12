using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ModelStudentResult
    {
        public int STUD_USERNAME{ get; set; }
        public string STUD_FNAME { get; set; }
        public string STUD_LNAME { get; set; }
        public string TEST_NAME { get; set; }
        public string TEST_RESULT { get; set; }
    }
}
