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
            return service.GetHashTagRequestDetails();
        }

        [HttpGet]
        public IList<CliqueTweetModel> GetHashTagTweets(string tag, string location, DateTime fromDate, DateTime toDate)
        {
            var model = new CliqueTagRequestModel
            {
                Tag = tag,
                Location = location,
                FromDate = fromDate,
                ToDate = toDate
            };

            HashTagService service = new HashTagService();
            return service.GetHashTagTweets(model);
        }

        [HttpPost]
        public void AddHashTagRequest(CliqueTagRequestModel model)
        {
            HashTagService service = new HashTagService();
            var responseModel = service.AddHashTagRequest(model);


        }

    }
}
