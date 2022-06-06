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
    
    public partial class ventas_detalle
    {
        public string auto_documento { get; set; }
        public string auto_producto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string auto_departamento { get; set; }
        public string auto_grupo { get; set; }
        public string auto_subgrupo { get; set; }
        public string auto_deposito { get; set; }
        public decimal cantidad { get; set; }
        public string empaque { get; set; }
        public decimal precio_neto { get; set; }
        public decimal descuento1p { get; set; }
        public decimal descuento2p { get; set; }
        public decimal descuento3p { get; set; }
        public decimal descuento1 { get; set; }
        public decimal descuento2 { get; set; }
        public decimal descuento3 { get; set; }
        public decimal costo_venta { get; set; }
        public decimal total_neto { get; set; }
        public decimal tasa { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public string auto { get; set; }
        public string estatus_anulado { get; set; }
        public System.DateTime fecha { get; set; }
        public string tipo { get; set; }
        public string deposito { get; set; }
        public int signo { get; set; }
        public decimal precio_final { get; set; }
        public string auto_cliente { get; set; }
        public string decimales { get; set; }
        public int contenido_empaque { get; set; }
        public decimal cantidad_und { get; set; }
        public decimal precio_und { get; set; }
        public decimal costo_und { get; set; }
        public decimal utilidad { get; set; }
        public decimal utilidadp { get; set; }
        public decimal precio_item { get; set; }
        public string estatus_garantia { get; set; }
        public string estatus_serial { get; set; }
        public string codigo_deposito { get; set; }
        public int dias_garantia { get; set; }
        public string detalle { get; set; }
        public decimal precio_sugerido { get; set; }
        public string auto_tasa { get; set; }
        public string estatus_corte { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public decimal z { get; set; }
        public string corte { get; set; }
        public string categoria { get; set; }
        public decimal cobranzap { get; set; }
        public decimal ventasp { get; set; }
        public decimal cobranzap_vendedor { get; set; }
        public decimal ventasp_vendedor { get; set; }
        public decimal cobranza { get; set; }
        public decimal ventas { get; set; }
        public decimal cobranza_vendedor { get; set; }
        public decimal ventas_vendedor { get; set; }
        public decimal costo_promedio_und { get; set; }
        public decimal costo_compra { get; set; }
        public string estatus_checked { get; set; }
        public string tarifa { get; set; }
        public decimal total_descuento { get; set; }
        public string codigo_vendedor { get; set; }
        public string auto_vendedor { get; set; }
        public string hora { get; set; }
        public string cierre_ftp { get; set; }
    
        public virtual clientes clientes { get; set; }
        public virtual empresa_depositos empresa_depositos { get; set; }
        public virtual empresa_tasas empresa_tasas { get; set; }
        public virtual productos productos { get; set; }
        public virtual productos_grupo productos_grupo { get; set; }
        public virtual vendedores vendedores { get; set; }
        public virtual ventas ventas1 { get; set; }
    }
}
