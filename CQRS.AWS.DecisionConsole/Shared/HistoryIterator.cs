using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;

namespace CQRS.AWS.DecisionConsole.Shared
{
    class HistoryIterator : IEnumerable<HistoryEvent>
    {
        DecisionTask lastResponse;
        IAmazonSimpleWorkflow swfClient;

        /// <summary>
        /// Create a new HistoryIterator to enumerate the history events in the DecisionTask passed in.
        /// </summary>
        /// <param name="client">SWF client to use for getting next page of history events.</param>
        /// <param name="response">The decision task returned from the PollForDecisionTask call.</param>
        public HistoryIterator(IAmazonSimpleWorkflow client, DecisionTask response)
        {
            this.swfClient = client;
            this.lastResponse = response;
        }

        /// <summary>
        /// Creates an enumerator for the history events. Automatically retrieves pages of
        /// history from SWF.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<HistoryEvent> GetEnumerator()
        {
            //Yield the history events in the current page of events
            foreach (HistoryEvent e in lastResponse.Events)
            {
                yield return e;
            }
            //If the NextPageToken is not null, get the next page of history events 
            while (!String.IsNullOrEmpty(lastResponse.NextPageToken))
            {
                List<HistoryEvent> events = GetNextPage();
                //Start yielding results from the next page of events
                foreach (HistoryEvent e in events)
                {
                    yield return e;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Helper method to call PollForDecisionTask with the NextPageToken
        /// to retrieve the next page of history events.
        /// </summary>
        /// <returns>List of history events received in the next page.</returns>
        List<HistoryEvent> GetNextPage()
        {
            PollForDecisionTaskRequest request = new PollForDecisionTaskRequest()
            {
                Domain = Constants.WFDomain,
                NextPageToken = lastResponse.NextPageToken,
                TaskList = new TaskList()
                {
                    Name = Constants.WFTaskList
                }
            };

            //AmazonSimpleWorkflow client does exponential back off and retries by default.
            //We want additional retries for robustness in case of transient failures like throttling.
            int retryCount = 10;
            int currentTry = 1;
            bool pollFailed;
            do
            {
                pollFailed = false;
                try
                {
                    this.lastResponse = swfClient.PollForDecisionTask(request).DecisionTask;
                }
                catch (Exception ex)
                {
                    //Swallow exception and keep polling
                    Console.Error.WriteLine("Poll request failed with exception :" + ex);
                    pollFailed = true;
                }
            }
            while (pollFailed && ++currentTry <= retryCount);
            return this.lastResponse.Events;
        }
    }
}
