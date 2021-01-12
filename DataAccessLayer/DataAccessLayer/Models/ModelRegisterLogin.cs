using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ModelRegisterLogin
    {
        public int LECT_USERNAME { get; set; }
        public string LECT_PASSWORD { get; set; }
        public string LECT_FNAME { get; set; }
        public string LECT_LNAME { get; set; }

        public int STUD_USERNAME { get; set; }
        public string STUD_PASSWORD { get; set; }
        public string STUD_FNAME { get; set; }
        public string STUD_LNAME { get; set; }

    }
}
