using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface IMyLogger:IDependService
    {
        void Write(string log);
    }
}
