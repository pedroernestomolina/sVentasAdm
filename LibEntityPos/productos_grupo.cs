//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityPos
{
    using System;
    using System.Collections.Generic;
    
    public partial class productos_grupo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public productos_grupo()
        {
            this.productos = new HashSet<productos>();
            this.ventas_detalle = new HashSet<ventas_detalle>();
        }
    
        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string estatus_catalogo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos> productos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventas_detalle> ventas_detalle { get; set; }
    }
}
