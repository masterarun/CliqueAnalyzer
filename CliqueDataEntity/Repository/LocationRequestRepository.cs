using CliqueDataEntity.Mapper;
using CliqueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueDataEntity.Repository
{
    public class LocationRequestRepository
    {
        private ipl_CliqueAnalyzerDB dataEntity;
        private CliqueAnalyzerMapper mapper;

        public LocationRequestRepository()
        {
            dataEntity = new ipl_CliqueAnalyzerDB();
            mapper = new CliqueAnalyzerMapper();
        }

        public CliqueLocationRequestModel AddLocationRequest(CliqueLocationRequestModel model)
        {
            var request = mapper.MapLocationRequestModelToEntity(model);
            dataEntity.CliqueLocationRequests.Add(request);
            dataEntity.SaveChanges();            
            model.Id = request.Id;
            return model;
        }

        public IList<CliqueLocationRequestModel> GetLocationRequest(int id)
        {
            return dataEntity.CliqueLocationRequests.Where(res => res.Id == id || id == 0).Select(mapper.MapLocationRequestEntityToModel).ToList();
        }

        public CliqueLocationRequestModel GetLocationRequestWithDetails(CliqueLocationRequestModel model)
        {
            CliqueLocationRequestModel response;
            var selectedItem = dataEntity.CliqueLocationRequests.FirstOrDefault(res => res.Address == model.Address);
            //&&   && res.Street == model.Street && res.FromDate == model.FromDate && res.ToDate == model.ToDate);
            if (selectedItem == null)
                return null;

            response = mapper.MapLocationRequestEntityToModel(selectedItem);

            response.CliqueTweetList = selectedItem.CliqueLocationTweets.Select(res => res.CliqueTweet).Select(mapper.MapTweetEntityToModel).ToList();
            return response;

        }

        public void AddTweetLocationRequest(IList<CliqueTweetModel> tweetList, int id)
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


                dataEntity.CliqueLocationTweets.Add(new CliqueLocationTweet { RequestId = id, TweetId = item.Id });
                dataEntity.SaveChanges();
            }

        }

        public void AddEventLocationRequest(IList<CliqueEventModel> eventList, int id)
        {
            foreach (var item in eventList)
            {
                var request = mapper.MapEventModelToEntity(item);
                var existingTweet = dataEntity.CliqueEvents.FirstOrDefault(res => res.EventId == item.EventId);
                if (existingTweet == null)
                {
                    dataEntity.CliqueEvents.Add(request);
                    dataEntity.SaveChanges();
                    item.Id = request.Id;
                }
                else
                    item.Id = existingTweet.Id;


                dataEntity.CliqueLocationEvents.Add(new CliqueLocationEvent { RequestId = id, EventId = item.Id });
                dataEntity.SaveChanges();
            }

        }

        public void UpdateLocationStatus(int id, CliqueStatus status)
        {
            var selectedTag = dataEntity.CliqueLocationRequests.FirstOrDefault(res => res.Id == id);
            //&& res.FromDate == model.FromDate && res.ToDate == model.ToDate);
            if (selectedTag == null)
                return;

            selectedTag.Status = (int)status;
            dataEntity.SaveChanges();
        }

        public void UpdateTweetScore(IEnumerable<SemantriaRequest> semantriaRequest)
        {
            foreach (var item in semantriaRequest)
            {
                var existingTweet = dataEntity.CliqueTweets.FirstOrDefault(res => res.TweetIdStr == item.Guid);
                if (existingTweet == null)
                    continue;
                existingTweet.Score = item.Score;
                dataEntity.SaveChanges();
            }
            
           
        }
    }
}
