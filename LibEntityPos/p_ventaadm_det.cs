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
    
    public partial class p_ventaadm_det
    {
        public int id { get; set; }
        public int id_ventaAdm { get; set; }
        public string auto_producto { get; set; }
        public string auto_departamento { get; set; }
        public string auto_grupo { get; set; }
        public string auto_subGrupo { get; set; }
        public string auto_tasa { get; set; }
        public string codigo_producto { get; set; }
        public string nombre_producto { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_neto { get; set; }
        public decimal precio_neto_divisa { get; set; }
        public string tarifa_precio { get; set; }
        public decimal tasa_iva { get; set; }
        public string tipo_iva { get; set; }
        public string categoria_producto { get; set; }
        public string decimales { get; set; }
        public string empaque_desc { get; set; }
        public int empaque_cont { get; set; }
        public string estatus_pesado { get; set; }
        public decimal costo_und { get; set; }
        public decimal costo { get; set; }
        public decimal costo_promedio { get; set; }
        public decimal costo_promedio_und { get; set; }
        public decimal dscto_porct { get; set; }
        public string notas { get; set; }
        public string estatusReservaInv { get; set; }
        public string auto_deposito { get; set; }
        public decimal total { get; set; }
        public decimal totalDivisa { get; set; }
        public decimal cantidadUnd { get; set; }
        public string estatus_remision { get; set; }
        public string nombre_deposito { get; set; }
    
        public virtual p_ventaadm p_ventaadm { get; set; }
    }
}
