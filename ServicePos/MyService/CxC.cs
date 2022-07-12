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


        public DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Lista.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Lista.Filtro filtro)
        {
            return ServiceProv.CxC_Tool_CtasPendiente_GetLista(filtro);
        }
        public DtoLib.Resultado
            CxC_Agregar(DtoLibPos.CxC.Agregar.Ficha ficha)
        {
            var r01 = ServiceProv.CxC_Agregar_Verificar_ClienteCredito (ficha.autoCliente, ficha.montoDivisaDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Mensaje = r01.Mensaje,
                };
                return rt;
            }

            return ServiceProv.CxC_Agregar(ficha);
        }
        public DtoLib.Resultado 
            CxC_AgregarNotaCreditoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var r01 = ServiceProv.CxC_Agregar_Verificar_ClienteCredito(ficha.autoCliente, ficha.montoDivisaDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Mensaje = r01.Mensaje,
                };
                return rt;
            }

            return ServiceProv.CxC_AgregarNotaCreditoAdm(ficha);
        }
        public DtoLib.Resultado
            CxC_AgregarNotaDebitoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var r01 = ServiceProv.CxC_Agregar_Verificar_ClienteCredito(ficha.autoCliente, ficha.montoDivisaDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Mensaje = r01.Mensaje,
                };
                return rt;
            }

            return ServiceProv.CxC_AgregarNotaDebitoAdm(ficha);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CxC.DocumentosPend.Ficha> 
            CxC_DocumentosPend_GetLista(DtoLibPos.CxC.DocumentosPend.Filtro filtro)
        {
            return ServiceProv.CxC_DocumentosPend_GetLista(filtro);
        }


        public DtoLib.ResultadoEntidad<int> 
            CxC_Get_ContadorNotaCreditoAdm()
        {
            return ServiceProv.CxC_Get_ContadorNotaCreditoAdm();
        }
        public DtoLib.ResultadoEntidad<int> 
            CxC_Get_ContadorNotaDebitoAdm()
        {
            return ServiceProv.CxC_Get_ContadorNotaDebitoAdm();
        }


        public DtoLib.Resultado 
            CxC_GestionCobro_Agregar(DtoLibPos.CxC.GestionCobro.Ficha ficha)
        {
            var r01 = ServiceProv.CxC_GestionCobro_Verificar_Agregar(ficha);  
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;

            return ServiceProv.CxC_GestionCobro_Agregar(ficha);
        }

    }

}