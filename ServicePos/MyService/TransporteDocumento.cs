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
            if (ficha.idRemision == "")
            {
                return ServiceProv.TransporteDocumento_AgregarPresupuesto(ficha);
            }
            else 
            {
                return ServiceProv.TransporteDocumento_AgregarPresupuestoConRemision(ficha);
            }
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura(DtoTransporte.Documento.Agregar.Factura.Ficha ficha)
        {
            return ServiceProv.TransporteDocumento_AgregarFactura(ficha);
        }

        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc)
        {
            return ServiceProv.TransporteDocumento_EntidadPresupuesto_GetById(idDoc);
        }
        public DtoLib.ResultadoLista<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>
            TransporteDocumento_EntidadPresupuesto_GetAliadosById(string idDoc)
        {
            return ServiceProv.TransporteDocumento_EntidadPresupuesto_GetAliadosById(idDoc);
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha> 
            TransporteDocumento_Remision_ListaBy(DtoTransporte.Documento.Remision.Lista.Filtro filtro)
        {
            return ServiceProv.TransporteDocumento_Remision_ListaBy(filtro);
        }
    }
}
