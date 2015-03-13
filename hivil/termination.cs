using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hivil
{
    class termination
    {
        public static void Terminate(string reason, int errorcode)
        {
            Console.WriteLine("FATAL ERROR ({0})", errorcode.ToString());
            Console.WriteLine(reason);
            quit();
        }
        public static void quit()
        {
            Console.WriteLine("PROGRAM EXECUTION COMPLETE");
            Console.WriteLine("Press any key to quit...");
        }
    }
}
