using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace general_tree
{
    public static class Logger
    {
        public static void Log(Exception e)
        {
            Console.WriteLine(e.Message + "\n" + e.StackTrace +"\n" + e.InnerException);
        }
    }
}
