using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueModel
{
    public class TweetRequest
    {
        public string Text { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MaxId { get; set; }
    }
}
