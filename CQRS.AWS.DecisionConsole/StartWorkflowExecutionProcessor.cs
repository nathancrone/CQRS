using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Amazon;
using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;

using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Repository;

using CQRS.AWS.DecisionConsole.Shared;

//using Amazon.S3;
//using Amazon.S3.Model;

namespace CQRS.AWS.DecisionConsole
{
    class StartWorkflowExecutionProcessor
    {
        //IAmazonSimpleWorkflow _swfClient = new AmazonSimpleWorkflowClient();

        //IAmazonS3 _s3Client = new AmazonS3Client();

        //VirtualConsole _console;

        public StartWorkflowExecutionProcessor() { }

        /// <summary>
        /// This method starts the workflow execution.
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="filepath"></param>
        public void StartWorkflowExecution(int RequestId)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //IAmazonS3 s3Client = new AmazonS3Client();
                    IAmazonSimpleWorkflow swfClient = new AmazonSimpleWorkflowClient();

                    //this._console.WriteLine("Create bucket if it doesn't exist");
                    // Make sure bucket exists
                    //s3Client.PutBucket(new PutBucketRequest
                    //{
                    //    BucketName = bucket,
                    //    UseClientRegion = true
                    //});

                    //this._console.WriteLine("Uploading image to S3");
                    // Upload the image to S3 before starting the execution
                    //PutObjectRequest putRequest = new PutObjectRequest
                    //{
                    //    BucketName = bucket,
                    //    FilePath = filepath,
                    //    Key = Path.GetFileName(filepath)
                    //};

                    //// Add upload progress callback to print every increment of 10 percent uploaded to the console.
                    //int currentPercent = -1;
                    //putRequest.StreamTransferProgress = new EventHandler<Amazon.Runtime.StreamTransferProgressArgs>((x, args) =>
                    //{
                    //    if (args.PercentDone == currentPercent)
                    //        return;

                    //    currentPercent = args.PercentDone;
                    //    if (currentPercent % 10 == 0)
                    //    {
                    //        this._console.WriteLine(string.Format("... Uploaded {0} %", currentPercent));
                    //    }
                    //});

                    //s3Client.PutObject(putRequest);

                    // Setup the input for the workflow execution that tells the execution what bukcet and object to use.
                    WorkflowExecutionStartedInput input = new WorkflowExecutionStartedInput
                    {
                        RequestId = RequestId
                    };

                    Console.WriteLine("Start workflow execution");
                    // Start the workflow execution
                    swfClient.StartWorkflowExecution(new StartWorkflowExecutionRequest()
                    {
                        //Serialize input to a string
                        Input = Utils.SerializeToJSON<WorkflowExecutionStartedInput>(input),
                        //Unique identifier for the execution
                        WorkflowId = DateTime.Now.Ticks.ToString(),
                        Domain = Constants.WFDomain,
                        WorkflowType = new WorkflowType()
                        {
                            Name = Constants.WFWorkflow,
                            Version = Constants.WFWorkflowVersion
                        }
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error starting workflow execution: " + e.Message);
                }
            });
        }
    }
}
