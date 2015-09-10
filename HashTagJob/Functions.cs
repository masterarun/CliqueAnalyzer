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
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueStatus.Queued);
                service.GenerateHashTagDetails(request.Id);
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueStatus.Processed);
            }
            catch(Exception e)
            {
                service.UpdateHashTagStatus(request.Id, CliqueModel.CliqueStatus.Error);
                log.WriteLine(e.Message);
            }
        }

        public static void ProcessQueueMessage([QueueTrigger("addlocationgrequest")] LocationRequest request, TextWriter log)
        {
            log.WriteLine("JOB-Stared");
            var service = new HashTagService();
            try
            {
                service.UpdateHashTagStatus(request.LocationId, CliqueModel.CliqueStatus.Queued);
                service.GenerateHashTagDetails(request.LocationId);
                service.UpdateHashTagStatus(request.LocationId, CliqueModel.CliqueStatus.Processed);
            }
            catch (Exception e)
            {
                service.UpdateHashTagStatus(request.LocationId, CliqueModel.CliqueStatus.Error);
                log.WriteLine(e.Message);
            }
        }
    }
}
