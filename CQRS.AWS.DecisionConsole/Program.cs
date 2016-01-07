using System;
using System.Threading;

namespace CQRS.AWS.DecisionConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            new DecisionWorker().Start(new CancellationTokenSource().Token);

            StartWorkflowExecutionProcessor _startProcessor = new StartWorkflowExecutionProcessor();
            _startProcessor.StartWorkflowExecution(1);

            //Console.WriteLine("done");

            Console.ReadLine();
        }
    }
}