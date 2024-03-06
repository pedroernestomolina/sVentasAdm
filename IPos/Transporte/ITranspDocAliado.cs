using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ITranspDocAliado
    {
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.GetAliados.Info.Ficha>
            TransporteDocumento_Get_Documento_Aliados_ByIdDoc(string id);
    }
}