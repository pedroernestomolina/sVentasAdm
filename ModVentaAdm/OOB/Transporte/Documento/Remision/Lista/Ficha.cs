using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Remision.Lista
{
    public class Ficha
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
        public bool isAnulado { get { return estatusAnulado == "1"; } }
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public Ficha()
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