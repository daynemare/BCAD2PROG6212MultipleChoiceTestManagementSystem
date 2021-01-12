using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTestManagementApp
{
    abstract class Test
    {
        public string Name { get; set; }
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public int TotalQuestions { get; set; }
        public int StudentMark { get; set; }

        public virtual int NextQuestion()
        {
            return QuestionNumber++;
        }

        public virtual int PreviousQuestion()
        {
            return QuestionNumber--;
        }

        public virtual int QuestionCounter()
        {
            return TotalQuestions++;
        }

    }
}
