//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CliqueDataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CliqueLocationRequest
    {
        public CliqueLocationRequest()
        {
            this.CliqueLocationEvents = new HashSet<CliqueLocationEvent>();
            this.CliqueLocationTweets = new HashSet<CliqueLocationTweet>();
        }
    
        public int Id { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    
        public virtual ICollection<CliqueLocationEvent> CliqueLocationEvents { get; set; }
        public virtual ICollection<CliqueLocationTweet> CliqueLocationTweets { get; set; }
    }
}