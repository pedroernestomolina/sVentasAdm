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
    
    public partial class p_venta
    {
        public int id { get; set; }
        public int id_p_operador { get; set; }
        public string auto_producto { get; set; }
        public string auto_departamento { get; set; }
        public string auto_grupo { get; set; }
        public string auto_subGrupo { get; set; }
        public string auto_tasa { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public decimal pneto { get; set; }
        public decimal pdivisaFull { get; set; }
        public string tarifaPrecio { get; set; }
        public decimal tasaIva { get; set; }
        public string tipoIva { get; set; }
        public string categoria { get; set; }
        public string decimales { get; set; }
        public string empaqueDescripcion { get; set; }
        public int empaqueContenido { get; set; }
        public string estatusPesado { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public decimal costoCompra { get; set; }
        public decimal costoPromedio { get; set; }
        public string auto_deposito { get; set; }
        public int id_p_pendiente { get; set; }
    
        public virtual p_operador p_operador { get; set; }
    }
}
