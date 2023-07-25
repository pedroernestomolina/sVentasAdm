using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Anular
{
    public class baseAuditoria
    {
        public string idSistemaDocumento { get; set; }
        public string idUsuario { get; set; }
        public string usuario { get; set; }
        public string codigo { get; set; }
        public string estacion { get; set; }
        public string motivo { get; set; }
        public baseAuditoria()
        {
            idSistemaDocumento = "";
            idUsuario = "";
            usuario = "";
            codigo = "";
            estacion = "";
            motivo = "";
        }
    }
}