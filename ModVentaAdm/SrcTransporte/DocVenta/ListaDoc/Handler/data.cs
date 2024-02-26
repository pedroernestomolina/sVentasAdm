using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Handler
{
    public class data: Vista.Idata
    {
        private OOB.Documento.Lista.Ficha _ficha;
        //
        public OOB.Documento.Lista.Ficha Ficha { get { return _ficha; } }
        public string DocumentoNro { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal MontoMonAct { get; set; }
        public decimal MontoMonDiv { get; set; }
        public string EntidadNombre { get; set; }
        public string EntidadCiRif { get; set; }
        public string TipoDocumento { get; set; }
        //
        public data(object rg)
        {
            _ficha = (OOB.Documento.Lista.Ficha)rg;
            DocumentoNro = _ficha.DocNumero;
            FechaEmision = _ficha.FechaEmision;
            MontoMonAct = _ficha.Monto;
            MontoMonDiv = _ficha.MontoDivisa;
            EntidadNombre = _ficha.NombreRazonSocial;
            EntidadCiRif = _ficha.CiRif;
            TipoDocumento = "";
            switch (_ficha.DocCodigo) 
            { 
                case "01":
                    TipoDocumento = "FACTURA";
                    break;
                case "02":
                    TipoDocumento = "NOTA/DEBITO";
                    break;
                case "03":
                    TipoDocumento = "NOTA/CREDITO";
                    break;
                case "04":
                    TipoDocumento = "HOJA/SERVICIO";
                    break;
                case "05":
                    TipoDocumento = "PRESUPUESTO";
                    break;
            }
        }
    }
}