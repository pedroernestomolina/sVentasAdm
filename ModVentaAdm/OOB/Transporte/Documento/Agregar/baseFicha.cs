using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar
{
    abstract public class baseFicha
    {
        public string RazonSocial { get; set; }
        public string DirFiscal { get; set; }
        public string CiRif { get; set; }
        public string TipoDoc { get; set; }
        public decimal montoExento  { get; set; }
        public decimal montoBase1 { get; set; }
        public decimal montoBase2 { get; set; }
        public decimal montoBase3 { get; set; }
        public decimal montoImpuesto1 { get; set; }
        public decimal montoImpuesto2 { get; set; }
        public decimal montoImpuesto3 { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa3 { get; set; }
        public string idCliente { get; set; }
        public string codCliente { get; set; }
        public string control { get; set; }
        public int diasCredito { get; set; }
        public decimal descuento1 { get; set; }
        public decimal descuento2 { get; set; }
        public decimal cargos { get; set; }
        public decimal descuento1p { get; set; }
        public decimal descuento2p { get; set; }
        public decimal cargosp { get; set; }
        public decimal subTotalNeto { get; set; }
        public string telefono { get; set; }
        public decimal factorCambio { get; set; }
        public string codVendedor { get; set; }
        public string vendedor { get; set; }
        public string idVendedor { get; set; }
        public string condPago { get; set; }
        public string usuario { get; set; }
        public string codUsuario { get; set; }
        public string codSucursal { get; set; }
        public decimal montoDivisa { get; set; }
        public string estacion { get; set; }
        public int cntRenglones { get; set; }
        public int  diasValidez { get; set; }
        public string idUsuario { get; set; }
        public int signo { get; set; }
        public string tipoRemision { get; set; }
        public string docRemision { get; set; }
        public string idRemision { get; set; }
        public string docNombre { get; set; }
        public decimal subTotalImpuesto { get; set; }
        public decimal subTotal { get; set; }
        public decimal neto { get; set; }
        public string docCodigo { get; set; }
        public string nota { get; set; }
        public baseFicha()
        {
            RazonSocial = "";
            DirFiscal = "";
            CiRif = "";
            TipoDoc = "";
            montoExento = 0.0m;
            montoBase1 = 0.0m;
            montoBase2 = 0.0m;
            montoBase3 = 0.0m;
            montoImpuesto1 = 0.0m;
            montoImpuesto2 = 0.0m;
            montoImpuesto3 = 0.0m;
            montoBase = 0.0m;
            montoImpuesto = 0.0m;
            Total = 0.0m;
            Tasa1 = 0.0m;
            Tasa2 = 0.0m;
            Tasa3 = 0.0m;
            idCliente = "";
            codCliente = "";
            control = "";
            diasCredito = 0;
            descuento1 = 0.0m;
            descuento2 = 0.0m;
            cargos = 0.0m;
            descuento1p = 0.0m;
            descuento2p = 0.0m;
            cargosp = 0.0m;
            subTotalNeto = 0.0m;
            telefono = "";
            factorCambio = 0.0m;
            codVendedor = "";
            vendedor = "";
            idVendedor = "";
            condPago = "";
            usuario = "";
            codUsuario = "";
            codSucursal = "";
            montoDivisa = 0.0m;
            estacion = "";
            cntRenglones = 0;
            diasValidez = 0;
            idUsuario = "";
            signo = 1;
            tipoRemision = "";
            docRemision = "";
            idRemision = "";
            docNombre = "";
            subTotalImpuesto = 0.0m;
            subTotal=0.0m;
            neto = 0.0m;
            docCodigo= "";
            nota = "";
        }
    }
}