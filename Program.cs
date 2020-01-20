using System;
using System.Collections.Generic;

namespace SHARPCENA
{
    class Program
    {
#if NETFX_CORE
        [MTAThread]
#else
        [STAThread]
#endif
        static void Main()
        {
            using (var program = new SHARPCENA())
                program.Run();

        }
    }
}