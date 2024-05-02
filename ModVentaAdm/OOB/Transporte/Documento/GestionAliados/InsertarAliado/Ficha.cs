using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.GestionAliados.InsertarAliado
{
    public class Ficha
    {
        public int idAliado { get; set; }
        public decimal montoDiv { get; set; }
        //DOCUMENTO
        public string docNumero { get; set; }
        public string docNombre { get; set; }
        public string docCodigo { get; set; }
        public DateTime docFecha { get; set; }
        public string docId { get; set; }
        public string docIdCliente { get; set; }
        // SERVICIO
        public int idServ { get; set; }
        public string codigoServ { get; set; }
        public string descServ { get; set; }
        public string detalleServ { get; set; }
    }
}