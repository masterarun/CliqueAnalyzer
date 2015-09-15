using CliqueDataEntity.Repository;
using CliqueModel;
using CliqueService.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueService
{
    public class LocationRequestService
    {
        private LocationRequestRepository repository;
        private TweetBusiness tweetBusiness;
        private SemantriaBusiness semantriaBusiness;
        private EventBusiness eventBusiness;

        public LocationRequestService()
        {
            repository = new LocationRequestRepository();
            tweetBusiness = new TweetBusiness();
            semantriaBusiness = new SemantriaBusiness();
            eventBusiness = new EventBusiness();
        }

        public CliqueLocationRequestModel AddLocationRequest(CliqueLocationRequestModel model)
        {
            return repository.AddLocationRequest(model);
        }

        public IList<CliqueLocationRequestModel> GetLocationRequest(int id = 0)
        {
            return repository.GetLocationRequest(id);
        }

        public void UpdateLocationRequestStatus(int id, CliqueStatus status)
        {
            repository.UpdateLocationStatus(id, status);
        }

        public CliqueLocationRequestModel GetLocationRequestWithDetails(CliqueLocationRequestModel model)
        {
            return repository.GetLocationRequestWithDetails(model);
        }

        public bool GenerateLocationRequestDetails(int requestId)
        {
            Console.WriteLine("GenerateLocationRequestDetails - Start");
            var currentRequest = GetLocationRequest(requestId).First();           

            GetTweets(currentRequest, requestId);
            GetEvents(currentRequest, requestId);
            Console.WriteLine("GenerateLocationRequestDetails - End");
            return true;
        }

        private void GetEvents(CliqueLocationRequestModel currentRequest, int requestId)
        {
            Console.WriteLine("GetEvents - Start");
            var eventList = eventBusiness.GetEvents(currentRequest.City);
            repository.AddEventLocationRequest(eventList, requestId);
            Console.WriteLine("GetEvents - End");
        }

        private void GetTweets(CliqueLocationRequestModel currentRequest, int requestId)
        {
            Console.WriteLine("GetTweets - Start");
            var tweetRequest = new TweetRequest
            {
                Latitude = currentRequest.Latitude,
                Longitude = currentRequest.Longitude
            };


            var tweetList = tweetBusiness.GetTweetsFromAPI(tweetRequest);
            repository.AddTweetLocationRequest(tweetList, requestId);
            int tweetCount = 1;
            while (tweetList.Count() == 100 && tweetCount <= 5)
            {
                tweetCount++;
                tweetRequest.MaxId = tweetList.Last().TweetIdStr;

                tweetList = tweetBusiness.GetTweetsFromAPI(tweetRequest);
                repository.AddTweetLocationRequest(tweetList, requestId);
            }

            //Get Score
            var semantriaRequest = tweetList.Select(res => new SemantriaRequest { Guid = res.TweetIdStr, Text = res.Text }).ToList();
            int semantriaCount = 1;
            foreach (var request in SplitList(semantriaRequest, 100))
            {
                if (semantriaCount == 5)
                    break;

                semantriaCount++;
                semantriaBusiness.GetScore(request);
                repository.UpdateTweetScore(semantriaRequest);
            }

            Console.WriteLine("GetTweets - End");
        }

        private List<List<SemantriaRequest>> SplitList(List<SemantriaRequest> locations, int size = 30)
        {
            List<List<SemantriaRequest>> list = new List<List<SemantriaRequest>>();

            for (int i = 0; i < locations.Count; i += size)
            {
                list.Add(locations.GetRange(i, Math.Min(size, locations.Count - i)));
            }

            return list;
        }




        public CliqueLocationRequestModel GetEventRequestDetails(CliqueLocationRequestModel model)
        {
            return repository.GetEventRequestDetails(model);
        }
    }
}
