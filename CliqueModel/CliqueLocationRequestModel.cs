using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueModel
{
    public class CliqueLocationRequestModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<CliqueEventModel> CliqueEventList { get; set; }
        public virtual ICollection<CliqueTweetModel> CliqueTweetList { get; set; }

    }
}
