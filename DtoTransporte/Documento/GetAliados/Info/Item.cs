using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GetAliados.Info
{
    public class Item
    {
        public string docId { get; set; }
        public string docNumero { get; set; }
        public DateTime docFecha { get; set; }
        public string docNombre { get; set; }
        public string  docCodigoTipo  { get; set; }
        public decimal docMontoDiv { get; set; }
        public string entidadId { get; set; }
        public string entidadCiRif { get; set; }
        public string entidadNombre{ get; set; }
        public int aliadoId { get; set; }
        public string alidoNombre { get; set; }
        public string alidoCiRif { get; set; }
        public decimal alidoMontoDiv { get; set; }
        public string servCodigo { get; set; }
        public string servDescripcion { get; set; }
        public string servDetalle { get; set; }
        public decimal servImporteDiv { get; set; }
    }
}