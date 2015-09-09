using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueModel
{
    public class CliqueTagRequestModel
    {

        public int Id { get; set; }
        public string Tag { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string AddedAt { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual IList<CliqueTweetModel> CliqueTweetList { get; set; }

    }
}
