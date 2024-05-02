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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.GestionAliados.ObtenerLista.Ficha> 
            TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(string id)
        {
            return ServiceProv.TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(id);
        }
        public DtoLib.Resultado 
            TransporteDocumento_GestionAliados_AnularAliado(DtoTransporte.Documento.GestionAliados.AnularAliado.Ficha ficha)
        {
            return ServiceProv.TransporteDocumento_GestionAliados_AnularAliado(ficha);
        }
        public DtoLib.ResultadoEntidad<int> 
            TransporteDocumento_GestionAliados_InsertarAliado(DtoTransporte.Documento.GestionAliados.InsertarAliado.Ficha ficha)
        {
            return ServiceProv.TransporteDocumento_GestionAliados_InsertarAliado(ficha);
        }
    }
}