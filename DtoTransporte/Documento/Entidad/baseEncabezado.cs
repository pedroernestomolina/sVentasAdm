using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad
{
    abstract public class baseEncabezado
    {
        public string idDoc { get; set; }
        public string docNumero { get; set; }
        public DateTime docFechaEmision { get; set; }
        public DateTime docFechaVence { get; set; }
        public string clienteNombre { get; set; }
        public string clienteDirFiscal { get; set; }
        public string clienteCiRif { get; set; }
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
        public string notasObs { get; set; }
        public string clienteId { get; set; }
        public string clienteCodigo { get; set; }
        public string mesRelacion { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int diasCredito { get; set; }
        public decimal descuento1 { get; set; }
        public decimal descuento2 { get; set; }
        public decimal cargos { get; set; }
        public decimal descuento1p { get; set; }
        public decimal descuento2p { get; set; }
        public decimal cargosp { get; set; }
        public string estatusAnulado  { get; set; }
        public decimal subTotalNeto { get; set; }
        public string clienteTelefono { get; set; }
        public decimal factorCambio { get; set; }
        public string vendedorCodigo { get; set; }
        public string vendedorNombre { get; set; }
        public string vendedorId { get; set; }
        public string condPago { get; set; }
        public string usuarioNombre { get; set; }
        public string uUsuarioCodigo { get; set; }
        public string codSucursal { get; set; }
        public string horaRegistro { get; set; }
        public decimal montoDivisa { get; set; }
        public string estacion { get; set; }
        public int cntRenglones { get; set; }
        public string anoRelacion { get; set; }
        public int  diasValidez { get; set; }
        public string usuarioId { get; set; }
        public int docSigno { get; set; }
        public string docNombre { get; set; }
        public decimal subTotalImpuesto { get; set; }
        public decimal subTotal { get; set; }
        public decimal montoNeto { get; set; }
        public string docModulo{ get; set; }
        public baseEncabezado()
        {
            idDoc = "";
            docNumero = "";
            docFechaEmision = DateTime.Now.Date;
            docFechaVence = DateTime.Now.Date;
            clienteNombre= "";
            clienteDirFiscal = "";
            clienteCiRif = "";
            docCodigoTipo = "";
            montoExento = 0.0m;
            montoBase1 = 0.0m;
            montoBase2 = 0.0m;
            montoBase3 = 0.0m;
            montoImpuesto1 = 0.0m;
            montoImpuesto2 = 0.0m;
            montoImpuesto3 = 0.0m;
            montoBase = 0.0m;
            montoImpuesto = 0.0m;
            docTotal = 0.0m;
            tasa1 = 0.0m;
            tasa2 = 0.0m;
            tasa3 = 0.0m;
            notasObs = "";
            clienteId = "";
            clienteCodigo = "";
            mesRelacion = "";
            fechaRegistro = DateTime.Now.Date;
            diasCredito = 0;
            descuento1 = 0.0m;
            descuento2 = 0.0m;
            cargos = 0.0m;
            descuento1p = 0.0m;
            descuento2p = 0.0m;
            cargosp = 0.0m;
            estatusAnulado = "";
            subTotalNeto = 0.0m;
            clienteTelefono = "";
            factorCambio = 0.0m;
            vendedorCodigo = "";
            vendedorNombre = "";
            vendedorId = "";
            condPago = "";
            usuarioNombre = "";
            uUsuarioCodigo = "";
            codSucursal = "";
            horaRegistro = "";
            montoDivisa = 0.0m;
            estacion = "";
            cntRenglones = 0;
            anoRelacion = "";
            diasValidez = 0;
            usuarioId = "";
            docSigno = 1;
            docNombre = "";
            subTotalImpuesto = 0.0m;
            subTotal=0.0m;
            montoNeto= 0.0m;
            docModulo = "";
        }
    }
}