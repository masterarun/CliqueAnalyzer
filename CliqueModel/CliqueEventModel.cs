using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueModel
{
    public class CliqueEventModel
    {
        public int Id { get; set; }
        public string EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<double> Score { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public string Venue { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }

    }
}
