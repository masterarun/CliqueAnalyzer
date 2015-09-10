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
    public class HashTagService
    {
        HashTagRepository repository;
        TweetBusiness business;

        public HashTagService()
        {
            repository = new HashTagRepository();
            business = new TweetBusiness();
        }

        public CliqueTagRequestModel AddHashTagRequest(CliqueTagRequestModel model)
        {
            return repository.AddHashTagRequest(model);
        }

        public IList<CliqueTagRequestModel> GetHashTagRequest(int id = 0)
        {
            return repository.GetHashTagRequest(id);
        }

        public void UpdateHashTagStatus(int id, CliqueStatus status)
        {
            repository.UpdateHashTagStatus(id, status);
        }

        public CliqueTagRequestModel GetHashTagRequestWithDetails(CliqueTagRequestModel model)
        {
            return repository.GetHashTagRequestWithDetails(model);
        }

        public bool GenerateHashTagDetails(int requestId)
        {
            var currentRequest = GetHashTagRequest(requestId).First();
            var tweetRequest = new TweetRequest
            {
                Text = currentRequest.Tag,
                Latitude = currentRequest.Latitude,
                Longitude = currentRequest.Longitude
            };
            var tweetList = business.GetTweetsFromAPI(tweetRequest);
            repository.AddTweetToHashTag(tweetList, requestId);

            while (tweetList.Count() == 100)
            {
                tweetRequest.MaxId = tweetList.Last().TweetIdStr;
                tweetList = business.GetTweetsFromAPI(tweetRequest);
                repository.AddTweetToHashTag(tweetList, requestId);
            }
            return true;
        }
    }
}
