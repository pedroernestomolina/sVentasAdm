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
            TransporteServPrest_Agregar(DtoTransporte.ServPrest.Agregar.Ficha ficha)
        {
            return ServiceProv.TransporteServPrest_Agregar(ficha);
        }
        public DtoLib.Resultado 
            TransporteServPrest_Editar(DtoTransporte.ServPrest.Editar.Ficha ficha)
        {
            return ServiceProv.TransporteServPrest_Editar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetById(int idFicha)
        {
            return ServiceProv.TransporteServPrest_GetById(idFicha);
        }
        public DtoLib.ResultadoLista<DtoTransporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetLista(DtoTransporte.ServPrest.Busqueda.Filtro filtro)
        {
            return ServiceProv.TransporteServPrest_GetLista(filtro);
        }
    }
}
