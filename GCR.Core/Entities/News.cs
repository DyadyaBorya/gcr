//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GCR.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
    	public int NewsId { get; set; }
    	public string Title { get; set; }
    	public string Summary { get; set; }
    	public string Link { get; set; }
    	public string Article { get; set; }
    	public System.DateTime CreatedOn { get; set; }
    	public int CreatedBy { get; set; }
    	public System.DateTime ModifiedOn { get; set; }
    	public int ModifiedBy { get; set; }
    }
}