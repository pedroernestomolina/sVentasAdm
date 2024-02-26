using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica
{
    public class Ficha
    {
        public string idDoc { get; set; }
        public string docNumero { get; set; }
        public DateTime docFechaEmision { get; set; }
        public string clienteNombre { get; set; }
        public string clienteDirFiscal { get; set; }
        public string clienteCiRif { get; set; }
        public string clienteId { get; set; }
        public string clienteCodigo { get; set; }
        public string clienteTelefono { get; set; }
        public string docCodigoTipo { get; set; }
        public decimal montoExento { get; set; }
        public decimal montoBase1 { get; set; }
        public decimal montoBase2 { get; set; }
        public decimal montoBase3 { get; set; }
        public decimal montoImpuesto1 { get; set; }
        public decimal montoImpuesto2 { get; set; }
        public decimal montoImpuesto3 { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal docTotal { get; set; }
        public decimal tasa1 { get; set; }
        public decimal tasa2 { get; set; }
        public decimal tasa3 { get; set; }
        public decimal factorCambio { get; set; }
        public string vendedorCodigo { get; set; }
        public string vendedorNombre { get; set; }
        public string vendedorId { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal montoNeto { get; set; }
        public string docModulo{ get; set; }
        public string codigoSucursal { get; set; }
    }
}