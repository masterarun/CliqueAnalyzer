﻿using CliqueDataEntity.Repository;
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
        private TweetBusiness business;

        public LocationRequestService()
        {
            repository = new LocationRequestRepository();
            business = new TweetBusiness();
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

        public CliqueLocationRequestModel GetHashTagRequestWithDetails(CliqueLocationRequestModel model)
        {
            return repository.GetLocationRequestWithDetails(model);
        }

        public bool GenerateLocationRequestDetails(int requestId)
        {
            var currentRequest = GetLocationRequest(requestId).First();
            var tweetRequest = new CliqueService.Business.TweetBusiness.TweetRequest
                    {
                        Latitude = currentRequest.Latitude,
                        Longitude = currentRequest.Longitude
                    };

            
            var tweetList = business.GetTweetsFromAPI(tweetRequest);
            repository.AddTweetLocationRequest(tweetList, requestId);

            while (tweetList.Count() == 100)
            {
                tweetRequest.MaxId = tweetList.Last().TweetIdStr;

                tweetList = business.GetTweetsFromAPI(tweetRequest);
                repository.AddTweetLocationRequest(tweetList, requestId);
            }
            return true;
        }



    }
}
