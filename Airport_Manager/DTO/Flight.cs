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

    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.Bill_Detail = new HashSet<Bill_Detail>();
            this.Jobs = new HashSet<Job>();
        }
    
        public int FlightID { get; set; }
        public int PlaneID { get; set; }
        public int Departure { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public int Destination { get; set; }
        public string Airline { get; set; }
        [Browsable(false)]    
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Detail> Bill_Detail { get; set; }
        [Browsable(false)]
        public virtual Plane Plane { get; set; }
        [Browsable(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
        [Browsable(false)]
        public virtual Location Location { get; set; }
        [Browsable(false)]
        public virtual Location Location1 { get; set; }
    }
}
