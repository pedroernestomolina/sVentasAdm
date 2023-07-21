using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public class data
    {
        private OOB.Transporte.Documento.Remision.Lista.Ficha _ficha;


        public OOB.Transporte.Documento.Remision.Lista.Ficha Ficha { get { return _ficha; } }
        public string DocId { get { return _ficha.docId; } }
        public string DocNumero { get { return _ficha.docNumero; } }
        public DateTime DocFecha { get { return _ficha.docFechaEmision; } }
        public string DocTipo { get { return _ficha.docNombre; } }
        public string NombreRazonSocial { get { return _ficha.clienteNombre; } }
        public string CiRif { get { return _ficha.clienteCiRif; } }
        public decimal Monto { get { return _ficha.docMontoMonedaDiv; } }
        public int CntRenglones { get { return _ficha.docCntRenglones; } }
        public string SolicitadoPor { get { return _ficha.docSolicitadoPor; } }
        public string ModuloCargar { get { return _ficha.docModuloCargar; } }


        public data(OOB.Transporte.Documento.Remision.Lista.Ficha ficha)
        {
            _ficha = ficha;
        }
    }
}