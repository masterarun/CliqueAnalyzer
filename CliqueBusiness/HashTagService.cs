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

        public IList<CliqueTagRequestModel> GetHashTagRequestDetails(int id = 0)
        {
            return repository.GetHashTagRequestDetails(id);
        }



        public IList<CliqueTweetModel> GetHashTagTweets(CliqueTagRequestModel model)
        {
            return repository.GetHashTagTweets(model);
        }

        public bool PullHashTagTweets(int requestId)
        {
            var currentRequest = GetHashTagRequestDetails(requestId).First();
            var tweetList = business.GetTweetsFromAPI(currentRequest);
            repository.AddTweetToHashTag(tweetList, requestId);

            while (tweetList.Count() == 100)
            {
                tweetList = business.GetTweetsFromAPI(currentRequest, tweetList.Last().TweetIdStr);
                repository.AddTweetToHashTag(tweetList, requestId);
            }
            return true;
        }
    }
}
