using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueModel
{
    public class CliqueTweetModel
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public string PostedBy { get; set; }
        public string PostedAt { get; set; }
        public Nullable<double> Score { get; set; }
        public System.DateTime AddedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public string ProfileImageURL { get; set; }
        public string TweetIdStr { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }

    }
}
