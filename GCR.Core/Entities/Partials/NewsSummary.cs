
namespace GCR.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class NewsSummary
    {
        public DateTime Date { get; set; }
        public SummaryType SummaryType { get; set; }
        public int Count { get; set; }
    }

    public enum SummaryType
    {
        Year,
        Month,
        Week,
    }
}
