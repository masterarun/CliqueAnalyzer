using CliqueModel;
using CliqueModel.QueueRequest;
using CliqueService;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueWebClient.Controllers
{
    public class CliqueAPIController : ApiController
    {

        [HttpGet]
        public IList<CliqueTagRequestModel> GetAllHashTag()
        {
            HashTagService service = new HashTagService();
            return service.GetHashTagRequest();
        }

        [HttpGet]
        public CliqueTagRequestModel GetHashTagDetails(string tag, string location)//, DateTime fromDate, DateTime toDate)
        {
            var model = new CliqueTagRequestModel
            {
                Tag = tag,
                Location = location,
                //FromDate = fromDate,
                //ToDate = toDate
            };

            HashTagService service = new HashTagService();
            return service.GetHashTagRequestWithDetails(model);
        }

        [HttpPost]
        public void AddHashTagRequest(CliqueTagRequestModel model)
        {
            HashTagService service = new HashTagService();
            var responseModel = service.AddHashTagRequest(model);


            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue thumbnailRequestQueue = queueClient.GetQueueReference("addhashtagrequest");
            thumbnailRequestQueue.CreateIfNotExists();
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new HashTagRequest { Id = model.Id }));
            thumbnailRequestQueue.AddMessage(queueMessage);

        }

        [HttpGet]
        public IList<CliqueLocationRequestModel> GetAllLocationRequest()
        {
            LocationRequestService service = new LocationRequestService();
            return service.GetLocationRequest();
        }

        [HttpGet]
        public CliqueLocationRequestModel GetLocationRequestDetails(string address, DateTime fromDate, DateTime toDate)
        {
            var model = new CliqueLocationRequestModel
            {
                Address = address,               
                FromDate = fromDate,
                ToDate = toDate
            };

            LocationRequestService service = new LocationRequestService();
            return service.GetLocationRequestWithDetails(model);
        }

        [HttpPost]
        public void AddLocationRequest(CliqueLocationRequestModel model)
        {
            LocationRequestService service = new LocationRequestService();
            var responseModel = service.AddLocationRequest(model);


            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue thumbnailRequestQueue = queueClient.GetQueueReference("addlocationgrequest");
            thumbnailRequestQueue.CreateIfNotExists();
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new LocationRequest { LocationId = model.Id }));
            thumbnailRequestQueue.AddMessage(queueMessage);

        }

    }
}
