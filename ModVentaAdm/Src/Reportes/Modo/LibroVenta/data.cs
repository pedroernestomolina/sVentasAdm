using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.LibroVenta
{
    //public class data
    //{
    //    public string codigoSucursalDoc { get; set; }
    //    public DateTime fechaDoc { get; set; }
    //    public string ciRifDoc { get; set; }
    //    public string nombreRazonSocialDoc { get; set; }
    //    public string desdeNumDoc { get; set; }
    //    public string hastaNumDoc { get; set; }
    //    public string numControlDoc { get; set; }
    //    public string codigoDoc { get; set; }
    //    public string numAplicaDoc { get; set; }
    //    public decimal montoTotal { get; set; }
    //    public decimal montoExento { get; set; }
    //    public decimal montoBase1 { get; set; }
    //    public decimal montoBase2 { get; set; }
    //    public decimal montoImpuesto1 { get; set; }
    //    public decimal montoImpuesto2 { get; set; }
    //    public decimal tasaIva1 { get; set; }
    //    public decimal tasaIva2 { get; set; }
    //    public int signoDoc { get; set; }
    //    public decimal montoRetencionIva { get; set; }
    //    public decimal tasaRetencionIva { get; set; }
    //    public DateTime fechaRetencionIva { get; set; }
    //    public string comprobanteRetencionIva { get; set; }
    //    public string notaCredito { get; set; }
    //    public string notaDebito { get; set; }
    //    public bool isResumen { get; set; }
    //    public int trans { get; set; }


    //    public data()
    //    {
    //        codigoSucursalDoc = "";
    //        fechaDoc = DateTime.Now.Date;
    //        ciRifDoc = "";
    //        nombreRazonSocialDoc = "";
    //        desdeNumDoc = "";
    //        hastaNumDoc = "";
    //        numControlDoc = "";
    //        codigoDoc = "";
    //        numAplicaDoc = "";
    //        montoTotal = 0m;
    //        montoExento = 0m;
    //        montoBase1 = 0m;
    //        montoBase2 = 0m;
    //        montoImpuesto1 = 0m;
    //        montoImpuesto2 = 0m;
    //        tasaIva1 = 0m;
    //        tasaIva2 = 0m;
    //        signoDoc = 1;
    //        montoRetencionIva = 0m;
    //        tasaRetencionIva = 0m;
    //        fechaRetencionIva = DateTime.Now.Date;
    //        comprobanteRetencionIva = "";
    //        isResumen = true;
    //        notaCredito = "";
    //        notaDebito = "";
    //        trans = 0;
    //    }

    //    public data(OOB.Reportes.LibroVenta.Ficha rg)
    //        :this()
    //    {
    //        codigoSucursalDoc = rg.codigoSucursalDoc;
    //        fechaDoc = rg.fechaDoc;
    //        ciRifDoc = rg.isResumen?"":rg.ciRifDoc;
    //        nombreRazonSocialDoc = rg.isResumen ? "Ventas Resumen: Sucursal("+codigoSucursalDoc+")" : rg.nombreRazonSocialDoc;
    //        desdeNumDoc = rg.numDoc;
    //        hastaNumDoc = rg.numDoc;
    //        numControlDoc = "";
    //        codigoDoc = rg.codigoDoc;
    //        numAplicaDoc = rg.numAplicaDoc;
    //        montoTotal = rg.montoTotal;
    //        montoExento = rg.montoExento;
    //        montoBase1 = rg.montoBase1;
    //        montoBase2 = rg.montoBase2;
    //        montoImpuesto1 = rg.montoImpuesto1;
    //        montoImpuesto2 = rg.montoImpuesto2;
    //        tasaIva1 = rg.tasaIva1;
    //        tasaIva2 = rg.tasaIva2;
    //        signoDoc = rg.signoDoc;
    //        montoRetencionIva = rg.montoRetencionIva;
    //        tasaRetencionIva = rg.tasaRetencionIva;
    //        fechaRetencionIva = rg.fechaRetencionIva;
    //        comprobanteRetencionIva = rg.comprobanteRetencionIva;
    //        isResumen = rg.isResumen;
    //        if (rg.codigoDoc == "02") { notaDebito = rg.numDoc; desdeNumDoc = ""; hastaNumDoc = ""; }
    //        if (rg.codigoDoc == "03") { notaCredito = rg.numDoc; desdeNumDoc = ""; hastaNumDoc = ""; }
    //        trans = 1;
    //    }

    //    public void Agregar(OOB.Reportes.LibroVenta.Ficha rg)
    //    {
    //        hastaNumDoc = rg.numDoc;
    //        montoTotal += rg.montoTotal;
    //        montoExento += rg.montoExento;
    //        montoBase1 += rg.montoBase1;
    //        montoBase2 += rg.montoBase2;
    //        montoImpuesto1 += rg.montoImpuesto1;
    //        montoImpuesto2 += rg.montoImpuesto2;
    //        trans++;
    //    }
    //}
}