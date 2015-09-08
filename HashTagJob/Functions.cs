using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using CliqueModel.QueueRequest;
using CliqueService;

namespace HashTagJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("addHashTagrequest")] HashTagRequest request, TextWriter log)
        {
            log.WriteLine("JOB-Stared");

            try
            {
                var service = new HashTagService();
                service.PullHashTagTweets(request.Id);
            }
            catch(Exception e)
            {
                log.WriteLine(e.Message);
            }
        }
    }
}
