using CliqueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueDataEntity.Mapper
{
    public class City
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class CliqueAnalyzerMapper
    {
        private List<City> cityList = new List<City> {
            new City{ Latitude = 21.215500, Longitude = 86.122200},
            new City{ Latitude = 28.771646, Longitude = 77.507561},
            new City{ Latitude = 9.512137, Longitude = 77.634087},
            new City{ Latitude = 9.548395, Longitude = 78.591637},
            new City{ Latitude = 26.937834, Longitude = 81.188324},
            new City{ Latitude = 25.454794, Longitude = 78.133957},
            new City{ Latitude = 25.424120, Longitude = 77.657990},
            new City{ Latitude = 15.460252, Longitude = 75.010284},
            new City{ Latitude = 20.385181, Longitude = 72.911453},
            new City{ Latitude = 23.673944, Longitude = 86.952393},
        };
        Random rnd = new Random();
        public CliqueAnalyzerMapper()
        {            
            
        }

        public CliqueTagRequestModel MapHashTagEntityToModel(CliqueTagRequest response)
        {
            var model = new CliqueTagRequestModel();

            model.AddedAt = response.AddedAt;
            model.FromDate = response.FromDate;
            model.Id = response.Id;
            model.Status = response.Status;
            model.Tag = response.Tag;
            model.ToDate = response.ToDate;
            model.Location = response.Location;
            model.Latitude = response.Latitude;
            model.Longitude = response.Longitude;
            model.StatusName = ((CliqueTagRequestStatus)response.Status).ToString();
          
            return model;
        }

        public CliqueTagRequest MapHashTagResponseModelToEntity(CliqueTagRequestModel model)
        {
            var request = new CliqueTagRequest();

            request.AddedAt = DateTime.Now.ToUniversalTime();
            request.FromDate = model.FromDate;

            request.Status = (int)CliqueTagRequestStatus.New;
            request.Tag = model.Tag;
            request.ToDate = model.ToDate;
            request.Location = model.Location;
            request.Latitude = model.Latitude;
            request.Longitude = model.Longitude;

            return request;
        }

        public CliqueTweetModel MapTweetEntityToModel(CliqueTweet response)
        {
            var cityId = rnd.Next(0, 9);
            var currentCity = cityList[cityId];

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

    }
}
