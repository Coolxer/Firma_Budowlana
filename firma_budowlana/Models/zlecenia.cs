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
    
    public partial class zlecenia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public zlecenia()
        {
            this.faktury = new HashSet<faktury>();
            this.materialy = new HashSet<materialy>();
        }
    
        public int id { get; set; }
        public int nr_zgloszenia { get; set; }
        public int kierownik { get; set; }
        public string etap { get; set; }
        public int postep { get; set; }
        public double szacunkowy_koszt { get; set; }
        public System.DateTime termin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<faktury> faktury { get; set; }
        public virtual kierownicy kierownicy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<materialy> materialy { get; set; }
        public virtual zgloszenia zgloszenia { get; set; }
    }
}
