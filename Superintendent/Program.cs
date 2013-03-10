using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Superintendent
{
    class Program
    {
        private static readonly Assembly Assembly = System.Reflection.Assembly.GetExecutingAssembly();
        private static readonly Version Version = Assembly.GetName().Version;

        static void Main(string[] args)
        {
            Console.WriteLine("Superintendent v{0}.{1}.{2} Starting Up", Version.Major, Version.Minor, Version.Build);
            Console.WriteLine("Built at {0}", (new FileInfo(Assembly.Location)).CreationTime.ToLongTimeString());

            // Init
            var intendent = new Superintendent();

            if (intendent.Ready)
            {
                Console.WriteLine("{0} Online", intendent.Name);
                intendent.Execute();
            }
            else
            {
                Console.WriteLine("Superintendent initialization failed.");
            }

            Console.WriteLine("Giving Up");
            Environment.Exit(-1);
        }
    }
}
