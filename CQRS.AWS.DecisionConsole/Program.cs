using System;

namespace CQRS.AWS.DecisionConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartWorkflowExecutionProcessor processor = new StartWorkflowExecutionProcessor();
            //processor.RequestInitialize(1);
            //processor.RequestActionComplete(1);
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}