using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class MyLogger : IMyLogger
    {
        public void Write(string log)
        {
            Console.WriteLine(log);
        }
    }
}
