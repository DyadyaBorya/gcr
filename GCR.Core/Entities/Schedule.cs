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
    
    public partial class Schedule
    {
    	public int ScheduleId { get; set; }
    	public Nullable<int> SeasonId { get; set; }
    	public Nullable<int> TeamId { get; set; }
    	public Nullable<System.DateTime> Date { get; set; }
    	public bool AtHome { get; set; }
    	public System.DateTime CreatedOn { get; set; }
    	public int CreatedBy { get; set; }
    	public System.DateTime ModifiedOn { get; set; }
    	public int Modifiedby { get; set; }
    
    	public virtual Season Season { get; set; }
    	public virtual Team Team { get; set; }
    }
}
