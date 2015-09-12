using CliqueDataEntity.Mapper;
using CliqueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueDataEntity.Repository
{
    public class HashTagRepository
    {
        private ipl_CliqueAnalyzerDB dataEntity;
        private CliqueAnalyzerMapper mapper;
        public HashTagRepository()
        {
            dataEntity = new ipl_CliqueAnalyzerDB();
            mapper = new CliqueAnalyzerMapper();
        }

        public CliqueTagRequestModel AddHashTagRequest(CliqueTagRequestModel model)
        {
            var request = mapper.MapHashTagResponseModelToEntity(model);
            dataEntity.CliqueTagRequests.Add(request);
            dataEntity.SaveChanges();
            model.Id = request.Id;
            return model;
        }

        public IList<CliqueTagRequestModel> GetHashTagRequest(int id)
        {
            return dataEntity.CliqueTagRequests.Where(res => res.Id == id || id == 0).Select(mapper.MapHashTagEntityToModel).ToList();
        }

        public CliqueTagRequestModel GetHashTagRequestWithDetails(CliqueTagRequestModel model)
        {
            CliqueTagRequestModel response;
            var selectedTag = dataEntity.CliqueTagRequests.FirstOrDefault(res => (res.Tag == model.Tag) && (res.Location == (model.Location == null ? "" : model.Location)));
                //&& res.FromDate == model.FromDate && res.ToDate == model.ToDate);
            if (selectedTag == null)
                return null;

            response = mapper.MapHashTagEntityToModel(selectedTag);

            response.CliqueTweetList =  selectedTag.CliqueTagTweetMappings.Select(res => res.CliqueTweet).Select(mapper.MapTweetEntityToModel).ToList();
            return response;

        }

        public void AddTweetToHashTag(IList<CliqueTweetModel> tweetList, int id)
        {
            foreach (var item in tweetList)
            {
                var request = mapper.MapTweetModelToEntity(item);
                var existingTweet = dataEntity.CliqueTweets.FirstOrDefault(res => res.TweetIdStr == item.TweetIdStr);
                if (existingTweet == null)
                {
                    dataEntity.CliqueTweets.Add(request);
                    dataEntity.SaveChanges();
                    item.Id = request.Id;
                }
                else
                    item.Id = existingTweet.Id;


                var existingMappingItem = dataEntity.CliqueTagTweetMappings.FirstOrDefault(res => res.TagId == id && res.TweetId == item.Id);
                if (existingMappingItem == null)
                {
                    dataEntity.CliqueTagTweetMappings.Add(new CliqueTagTweetMapping { TagId = id, TweetId = item.Id });
                    dataEntity.SaveChanges();
                }
            }

        }

        public void UpdateHashTagStatus(int id, CliqueStatus status)
        {
            var selectedTag = dataEntity.CliqueTagRequests.FirstOrDefault(res => res.Id == id);
            //&& res.FromDate == model.FromDate && res.ToDate == model.ToDate);
            if (selectedTag == null)
                return;
            selectedTag.Status = (int)status;
            dataEntity.SaveChanges();
        }
    }
}
