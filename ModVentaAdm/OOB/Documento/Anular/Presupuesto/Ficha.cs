using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Documento.Anular.Presupuesto
{
    
    public class Ficha: BaseFicha
    {


        public Ficha()
        {
            autoDocumento = "";
            auditoria = new FichaAuditoria();
        }

    }

}