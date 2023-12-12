using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doot_gen.util
{
    internal class DootExeption : Exception
    {
        public DootExeption(string message) : base(message)
        {
            Logger.Error(message);
        }
    }
}
