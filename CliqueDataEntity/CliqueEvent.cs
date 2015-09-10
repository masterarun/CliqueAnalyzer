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
    
    public partial class CliqueEvent
    {
        public CliqueEvent()
        {
            this.CliqueLocationEvents = new HashSet<CliqueLocationEvent>();
        }
    
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
    
        public virtual ICollection<CliqueLocationEvent> CliqueLocationEvents { get; set; }
    }
}
