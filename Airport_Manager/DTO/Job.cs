//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Job
    {
        public int JobID { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public int EmployeeID { get; set; }
        public int FlightID { get; set; }
        public string JobDescription { get; set; }
        public string JobState { get; set; }
        [Browsable(false)]    
        
        public virtual Employee Employee { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
