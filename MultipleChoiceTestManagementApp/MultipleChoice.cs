using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTestManagementApp
{
    class MultipleChoice:Test
    {
        //This child class inherits test properties from its parent class Test and presents polymorphism by overriding 2 methods displayed below
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        public override int NextQuestion()
        {
            
            return QuestionNumber+1;
        }

        public override int PreviousQuestion()
        {
            return QuestionNumber-1;
        }

       
    }
}
