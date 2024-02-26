using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.NotaCredito.Nueva
{
    public class Documento
    {
        public string RazonSocial { get; set; }
        public string DirFiscal { get; set; }
        public string CiRif { get; set; }
        public string TipoDoc { get; set; }
        public decimal montoExento { get; set; }
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
        public int cntRenglones { get; set; }
        public int diasValidez { get; set; }
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
        public string docSiglas { get; set; }
        public decimal subTotalMonDivisa { get; set; }
    }
}
