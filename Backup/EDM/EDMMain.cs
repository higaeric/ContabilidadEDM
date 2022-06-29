using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDM
{
    public class EDM
    {
        public static string programPath;
        public PlanDeCuentas PDC;

        public EDM(string programPath_)
        {
            programPath = programPath_;
            PDC = new PlanDeCuentas();
        }
    }
}
