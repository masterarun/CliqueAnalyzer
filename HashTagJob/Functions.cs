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
        public static void ProcessQueueMessage([QueueTrigger("addhashtagrequest")] HashTagRequest request, TextWriter log)
        {
            log.WriteLine("JOB-Stared");
            var service = new HashTagService();
            try
            {              
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueTagRequestStatus.Queued);
                service.PullHashTagTweets(request.Id);
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueTagRequestStatus.Processed);
            }
            catch(Exception e)
            {
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueTagRequestStatus.Error);
                log.WriteLine(e.Message);
            }
        }
    }
}
