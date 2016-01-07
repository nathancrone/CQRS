using System;
using System.Threading;

namespace CQRS.AWS.Activity1Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Activity1Worker().Start(new CancellationTokenSource().Token);

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}