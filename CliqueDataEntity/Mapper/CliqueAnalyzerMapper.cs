using CliqueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueDataEntity.Mapper
{

    public class CliqueAnalyzerMapper
    {

        public CliqueTagRequestModel MapHashTagEntityToModel(CliqueTagRequest response)
        {
            var model = new CliqueTagRequestModel();

            model.AddedAt = response.AddedAt==null?string.Empty:response.AddedAt.Value.ToShortDateString();
            model.FromDate = response.FromDate;
            model.Id = response.Id;
            model.Status = response.Status;
            model.Tag = response.Tag;
            model.ToDate = response.ToDate;
            model.Location = response.Location;
            model.Latitude = response.Latitude;
            model.Longitude = response.Longitude;
            model.StatusName = ((CliqueStatus)response.Status).ToString();
          
            return model;
        }

        public CliqueTagRequest MapHashTagResponseModelToEntity(CliqueTagRequestModel model)
        {
            var request = new CliqueTagRequest();

            request.AddedAt = DateTime.Now.ToUniversalTime();
            request.FromDate = model.FromDate;

            request.Status = (int)CliqueStatus.New;
            request.Tag = model.Tag;
            request.ToDate = model.ToDate;
            request.Location = model.Location;
            request.Latitude = model.Latitude;
            request.Longitude = model.Longitude;

            return request;
        }

        public CliqueTweetModel MapTweetEntityToModel(CliqueTweet response)
        {
          
            var model = new CliqueTweetModel();

            model.AddedAt = response.AddedAt;
            model.Id = response.Id;
            model.PostedAt = response.PostedAt;
            model.PostedBy = response.PostedBy;
            model.ProfileImageURL = response.ProfileImageURL;
            model.Score = response.Score;
            model.Text = response.Text;
            model.TweetIdStr = response.TweetIdStr;
            model.Latitude = response.Latitude;
            model.Longitude = response.Longitude;
            model.Location = response.Location;

            return model;
        }

        public CliqueTweet MapTweetModelToEntity(CliqueTweetModel model)
        {
            var request = new CliqueTweet();

            request.AddedAt = model.AddedAt;
            request.Id = model.Id;
            request.PostedAt = model.PostedAt;
            request.PostedBy = model.PostedBy;
            request.ProfileImageURL = model.ProfileImageURL;
            request.Score = model.Score;
            request.Text = model.Text;
            request.TweetIdStr = model.TweetIdStr;
            request.Latitude = model.Latitude;
            request.Longitude = model.Longitude;
            request.AddedAt = DateTime.Now;
            request.ModifiedAt = DateTime.Now;
            request.Location = model.Location;

            return request;
        }

        public CliqueLocationRequestModel MapLocationRequestEntityToModel(CliqueLocationRequest response)
        {
            var model = new CliqueLocationRequestModel();

            model.Address = response.Address;
            model.City = response.City;
            model.Id = response.Id;
            model.State = response.State;
            model.Status = response.Status;
            model.Street = response.Street;
            model.Pincode = response.Pincode;
            model.StatusName = ((CliqueStatus)response.Status).ToString();
            model.Latitude = response.Latitude;
            model.Longitude = response.Longitude;

            return model;
        }

        public CliqueLocationRequest MapLocationRequestModelToEntity(CliqueLocationRequestModel model)
        {
            var request = new CliqueLocationRequest();
            request.Address = model.Address;
            request.City = model.City;
            request.State = model.State;
            request.Status = model.Status;
            request.Street = model.Street;
            request.Latitude = model.Latitude;
            request.Longitude = model.Longitude;
            request.Pincode = model.Pincode;
            return request;
        }

        public CliqueEvent MapEventModelToEntity(CliqueEventModel model)
        {
            var request = new CliqueEvent();

            request.CreatedAt = DateTime.Now;
            request.Id = model.Id;
            request.Description = model.Description;
            request.EndDate = model.EndDate;
            request.EventId = model.EventId;
            request.Score = model.Score;
            request.Name = model.Name;
            request.StartDate = model.StartDate;
            request.Venue = model.Venue;
            request.ModifiedAt = DateTime.Now;

            return request;
        }

        public CliqueEventModel MapEventEntityToModel(CliqueEvent request)
        {
            var model = new CliqueEventModel();

            model.CreatedAt = request.CreatedAt;
            model.Id = request.Id;
            model.Description = request.Description;
            model.EndDate = request.EndDate;
            model.EventId = request.EventId;
            model.Score = request.Score;
            model.Name = request.Name;
            model.StartDate = request.StartDate;
            model.Venue = request.Venue;
            model.ModifiedAt = request.ModifiedAt;

            return model;
        }
    }
}
