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
        public DtoLib.ResultadoId 
            TransporteAliado_Agregar(DtoTransporte.Aliado.Agregar.Ficha ficha)
        {
            return ServiceProv.TransporteAliado_Agregar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetById(int idAliado)
        {
            return ServiceProv.TransporteAliado_GetById(idAliado);
        }
        public DtoLib.ResultadoLista<DtoTransporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetLista(DtoTransporte.Aliado.Busqueda.Filtro filtro)
        {
            return ServiceProv.TransporteAliado_GetLista(filtro);
        }
    }
}
