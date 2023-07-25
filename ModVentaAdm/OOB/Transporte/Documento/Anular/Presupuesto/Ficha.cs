using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Anular.Presupuesto
{
    public class Ficha
    {
        public string idDoc { get; set; }
        public FichaAuditoria auditoria { get; set; }
        public Ficha()
        {
            idDoc = "";
            auditoria = new FichaAuditoria();
        }
    }
}