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
            var r01 = ServiceProv.TransporteDocumento_AgregarFactura_Vericar(ficha);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError) 
            {
                var rt = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>()
                {
                    Entidad = null,
                    Mensaje = r01.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError
                };
                return rt;
            }
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

        public DtoLib.Resultado 
            TransporteDocumento_AnularPresupuesto(DtoTransporte.Documento.Anular.Presupuesto.Ficha ficha)
        {
            return ServiceProv.TransporteDocumento_AnularPresupuesto(ficha);
        }
        public DtoLib.Resultado 
            TransporteDocumento_AnularNotaEntrega(DtoTransporte.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var r01 = ServiceProv.TransporteDocumento_AnularNotaEntrega_GetDataAnular(ficha.idDocVenta);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError) 
            {
                var rt = new DtoLib.Resultado();
                rt.Mensaje = r01.Mensaje;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            };
            ficha.idCliente = r01.Entidad.idCliente;
            ficha.montoDivisa = r01.Entidad.montoDivisa;
            ficha.idDocCxC  = r01.Entidad.idDocCxC;
            ficha.aliadosInv = r01.Entidad.aliadosInv;
            ficha.aliadosDoc = r01.Entidad.aliadosDoc;
            ficha.itemsServicio = r01.Entidad.itemsServ;
            ficha.docRef = r01.Entidad.docRef;

            var r00 = ServiceProv.TransporteDocumento_AnularNotaEntrega_Verifica(ficha);
            if (r00.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado();
                rt.Mensaje = r00.Mensaje;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            }

            return ServiceProv.TransporteDocumento_AnularNotaEntrega(ficha);
        }

        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Venta.Ficha> 
            TransporteDocumento_EntidadVenta_GetById(string idDoc)
        {
            return ServiceProv.TransporteDocumento_EntidadVenta_GetById(idDoc);
        }

        public DtoLib.ResultadoEntidad<int> 
            TransporteDocumento_Presupuesto_Pendiente_Cnt()
        {
            return ServiceProv.TransporteDocumento_Presupuesto_Pendiente_Cnt();
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.Lista.Pendiente.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_Pendiente()
        {
            return ServiceProv.TransporteDocumento_Presupuesto_Pendiente();
        }
        public DtoLib.Resultado 
            TransporteDocumento_AnularPresupuesto_Pendiente(string idDoc)
        {
            return ServiceProv.TransporteDocumento_AnularPresupuesto_Pendiente(idDoc);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            TransporteDocumento_GetLista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            return ServiceProv.TransporteDocumento_GetLista(filtro);
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetAliados.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetAliados(string idDoc)
        {
            return ServiceProv.TransporteDocumento_Presupuesto_GetAliados(idDoc);
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetServicios.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetServicios(string idDoc)
        {
            return ServiceProv.TransporteDocumento_Presupuesto_GetServicios(idDoc);
        }
    }
}