//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THELOAI
    {
        public THELOAI()
        {
            this.GAMEs = new HashSet<GAME>();
        }
    
        public int id { get; set; }
        public string ten { get; set; }
        public System.DateTime ngaytao { get; set; }
    
        public virtual ICollection<GAME> GAMEs { get; set; }
    }
}
