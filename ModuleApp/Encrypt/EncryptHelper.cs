using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    public class EncryptHelper : IEncryptHelper
    {
        private IMyLogger myLogger;
        public EncryptHelper(IMyLogger myLogger)
        {
            this.myLogger = myLogger;
        }
        public string EncryptData(string source)
        {
            myLogger.Write(source);
            return string.Format("<-{0}->", source);
        }
    }
}
