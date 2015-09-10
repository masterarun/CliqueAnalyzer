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
    public class EventBusiness
    {
        private const string key = "qRg2vNsFsqChXjnK";
        private List<dynamic> eventList = new List<dynamic>();

        public List<CliqueEventModel> GetEvents(string location)
        {
            GetLocationEvents(location, 1);

            var returnObject = new List<CliqueEventModel>();

            foreach (var item in eventList)
            {
                returnObject.Add(
                    new CliqueEventModel
                    {
                        EventId = item.id,
                        Description = item.description.Value,
                        StartDate = DateTime.Parse(item.start_time.Value),
                        EndDate = DateTime.Parse(item.stop_time.Value ?? item.start_time.Value),
                        Name = item.title.Value,
                        Venue = item.venue_name.Value
                    }
                    );

            }

            return returnObject;
        }

        private void GetLocationEvents(string location, int pageNumber)
        {

            string categories = AppendCategories();

            string url = "";
            if (pageNumber == 1)
                url = string.Format("http://api.eventful.com/json/events/search?app_key={0}&{2}&l={1}&within=10&units=miles&page_size=100", key, location, categories);
            else
                url = string.Format("http://api.eventful.com/json/events/search?app_key={0}&{2}&l={1}&within=10&units=miles&page_number={3}&page_size=100", key, location, categories, pageNumber);

            var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);

            try
            {
                var httpClient = new HttpClient();
                HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;
                var response = responseUserTimeLine.Content.ReadAsStringAsync().Result;
                dynamic wrapper = JsonConvert.DeserializeObject<object>(response);
                if (wrapper.events != null || wrapper.total_items != 0)
                {
                    if (wrapper.total_items == 1)
                    {
                        eventList.Add(wrapper.events.@event as dynamic);
                    }
                    else
                        eventList.AddRange((wrapper.events.@event as IEnumerable<dynamic>).ToList());

                    if (wrapper.page_count > pageNumber)
                        GetLocationEvents(location, ++pageNumber);
                }
            }
            catch(Exception)
            {
                //Some time it throws eeror
            }




        }

        private string AppendCategories()
        {
            List<string> Categories = new List<string> { "sports" };
            StringBuilder catFilter = new StringBuilder(1000);
            //catFilter.Append("(");
            var lastCat = Categories.Last();
            foreach (var item in Categories)
            {
                if (item != lastCat)
                {
                    catFilter.Append("q=" + item + "||");
                }
                else
                {
                    catFilter.Append("q=" + item);
                }

            }
            return catFilter.ToString();
        }
    }
}
