//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace firma_budowlana.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class materialy
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public double ilosc { get; set; }
        public double wartosc { get; set; }
        public int zarezerwowany_dla { get; set; }
        public Nullable<int> dostepny_w { get; set; }
    
        public virtual magazyny magazyny { get; set; }
        public virtual zlecenia zlecenia { get; set; }
    }
}
