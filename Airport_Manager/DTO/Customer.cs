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

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Bill_Detail = new HashSet<Bill_Detail>();
        }
    
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public Nullable<bool> Sex { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string TeleNumber { get; set; }
        [Browsable(false)]            
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Detail> Bill_Detail { get; set; }
    }
}
