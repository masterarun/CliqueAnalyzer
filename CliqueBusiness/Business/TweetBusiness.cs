using CliqueModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CliqueService.Business
{
    public class TweetBusiness
    {
        private const string TwitterConsumerKey = "yeztpKZcCqNBQLEWoondDcvH7";
        private const string TwitterConsumerSecret = "0kqgCZ1ZzJUHk7VO7XkYwonKVYVxIFX9n5xmgXbBlDHrvdZHVk";
        //private const string TwitterConsumerKey = "unZjcnD6BB0vbU5TfTiXPGnVe";
        //private const string TwitterConsumerSecret = "7VoiPTrbqaq1vnuu87U4CAbYDyfiqJwlSanN6LvzGkfQ43f1fj";

        private static string GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                      .GetBytes(TwitterConsumerKey + ":" + TwitterConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                                                      "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            string json = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject<object>(json);
            return item["access_token"];
        }

        public IList<CliqueTweetModel> GetTweetsFromAPI(CliqueTagRequestModel requestModel, string max_id = "")
        {
            string url = "";
            
            if (string.IsNullOrEmpty(max_id))
            {                
                url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={2}&geocode={0},{1},100mi&count=100", requestModel.Latitude, requestModel.Longitude, requestModel.Tag);                
            }
            else
                url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={2}&geocode={0},{1},100mi&count=100&max_id={3}", requestModel.Latitude, requestModel.Longitude, requestModel.Tag, max_id);                


            var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
            var accessToken = GetAccessToken();
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;

            dynamic jsonResponse = JsonConvert.DeserializeObject<object>(responseUserTimeLine.Content.ReadAsStringAsync().Result);

            var enumerableTweets = (jsonResponse.statuses as IEnumerable<dynamic>);

            List<CliqueTweetModel> modelList = new List<CliqueTweetModel>();
            foreach (var item in enumerableTweets)
            {
                var geoItem = (item.geo ?? item.retweeted_status.geo);
                modelList.Add(new CliqueTweetModel
                {
                    Text = item.text.Value,
                    PostedBy = item.user.name.Value,
                    PostedAt = item.created_at.Value,
                    ProfileImageURL = item.user.profile_image_url_https.Value,
                    TweetIdStr = item.id_str.Value,
                    Latitude = geoItem == null ? requestModel.Latitude: geoItem.coordinates[0],
                    Longitude = geoItem == null ? requestModel.Longitude : geoItem.coordinates[1],
                    Location = item.user.location
                });

            }


            return modelList;

        }
    }
}
