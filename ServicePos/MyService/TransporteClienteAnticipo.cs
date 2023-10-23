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
            Transporte_Cliente_Anticipo_Agregar(DtoTransporte.ClienteAnticipo.Agregar.Ficha ficha)
        {
            return ServiceProv.Transporte_Cliente_Anticipo_Agregar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Obtener.Ficha> 
            Transporte_Cliente_Anticipo_Obtener_ById(string idCliente)
        {
            return ServiceProv.Transporte_Cliente_Anticipo_Obtener_ById(idCliente);
        }
        public DtoLib.ResultadoLista<DtoTransporte.ClienteAnticipo.ListaMov.Ficha> 
            Transporte_Cliente_Anticipo_GetLista(DtoTransporte.ClienteAnticipo.ListaMov.Filtro filtro)
        {
            return ServiceProv.Transporte_Cliente_Anticipo_GetLista(filtro);
        }
    }
}