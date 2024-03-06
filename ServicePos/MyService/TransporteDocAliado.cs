using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.GetAliados.Info.Ficha> 
            TransporteDocumento_Get_Documento_Aliados_ByIdDoc(string id)
        {
            return ServiceProv.TransporteDocumento_Get_Documento_Aliados_ByIdDoc(id);
        }
    }
}