using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    public interface IEncryptHelper:IDependService
    {
        string EncryptData(string source);
    }
}
