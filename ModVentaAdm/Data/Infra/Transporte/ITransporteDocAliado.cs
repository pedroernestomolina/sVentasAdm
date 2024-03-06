using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITranspDocAliado
    {
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GetAliados.Info.Ficha >
            TransporteDocumento_Get_Documento_Aliados_ByIdDoc(string id);
    }
}