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
    
    public partial class CliqueTagTweetMapping
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int TweetId { get; set; }
    
        public virtual CliqueTagRequest CliqueTagRequest { get; set; }
        public virtual CliqueTweet CliqueTweet { get; set; }
    }
}
