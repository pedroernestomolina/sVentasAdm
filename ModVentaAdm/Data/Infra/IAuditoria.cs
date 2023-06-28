using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface IAuditoria
    {
        OOB.Resultado.FichaEntidad<OOB.Auditoria.Entidad.Ficha> 
            Auditoria_Documento_GetFichaBy(OOB.Auditoria.Buscar.Ficha ficha);
    }
}