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
            var currentRequest = GetLocationRequest(requestId).First();
            var tweetRequest = new TweetRequest
                    {
                        Latitude = currentRequest.Latitude,
                        Longitude = currentRequest.Longitude
                    };

            
            var tweetList = tweetBusiness.GetTweetsFromAPI(tweetRequest);
            repository.AddTweetLocationRequest(tweetList, requestId);

            while (tweetList.Count() == 100)
            {
                tweetRequest.MaxId = tweetList.Last().TweetIdStr;

                tweetList = tweetBusiness.GetTweetsFromAPI(tweetRequest);
                repository.AddTweetLocationRequest(tweetList, requestId);
            }

            var semantriaRequest = tweetList.Select(res => new SemantriaRequest { Guid = res.TweetIdStr, Text = res.Text });
            semantriaBusiness.GetScore(semantriaRequest);
            repository.UpdateTweetScore(semantriaRequest);

            var eventList = eventBusiness.GetEvents(currentRequest.City);
            repository.AddEventLocationRequest(eventList, requestId);

            return true;
        }



    }
}
