using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Lista
{
    abstract public class baseLista
    {
        public string docId { get; set; }
        public string docNumero { get; set; }
        public string docNombre { get; set; }
        public string docCodigo { get; set; }
        public int docSigno { get; set; }
        public int docCntRenglones { get; set; }
        public DateTime docFechaEmision { get; set; }
        public string docHoraEmision { get; set; }
        public decimal docMontoMonedaAct { get; set; }
        public decimal docMontoMonedaDiv { get; set; }
        public string clienteNombre { get; set; }
        public string clienteCiRif { get; set; }
        public decimal factorCambio { get; set; }
        public string estatusAnulado { get; set; }
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public baseLista()
        {
            docId = "";
            docNumero = "";
            docNombre = "";
            docCodigo = "";
            docSigno = 1;
            docCntRenglones = 0;
            docFechaEmision = DateTime.Now.Date;
            docHoraEmision = "";
            docMontoMonedaAct = 0m;
            docMontoMonedaDiv = 0m;
            docSolicitadoPor = "";
            docModuloCargar = "";
            clienteCiRif = "";
            clienteNombre = "";
            factorCambio = 0m;
            estatusAnulado = "";
        }
    }
}