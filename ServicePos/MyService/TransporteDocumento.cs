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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado> 
            TransporteDocumento_AgregarPresupuesto(DtoTransporte.Documento.Agregar.Presupuesto.Ficha ficha)
        {
            return ServiceProv.TransporteDocumento_AgregarPresupuesto(ficha);
        }
        public DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha> 
            TransporteDocumento_Remision_ListaBy(DtoTransporte.Documento.Remision.Lista.Filtro filtro)
        {
            return ServiceProv.TransporteDocumento_Remision_ListaBy(filtro);
        }
    }
}
