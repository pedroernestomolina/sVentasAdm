using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface ICxC
    {

        DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Lista.Ficha>
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Lista.Filtro filtro);
        DtoLib.Resultado
            CxC_Agregar(DtoLibPos.CxC.Agregar.Ficha ficha);
        DtoLib.Resultado
            CxC_AgregarNotaCreditoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha);
        DtoLib.Resultado
            CxC_AgregarNotaDebitoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha);
        DtoLib.ResultadoLista<DtoLibPos.CxC.DocumentosPend.Ficha>
            CxC_DocumentosPend_GetLista(DtoLibPos.CxC.DocumentosPend.Filtro filtro);


        DtoLib.Resultado
            CxC_Agregar_Verificar_ClienteCredito(string idCliente, decimal monto);
        DtoLib.ResultadoEntidad<int>
            CxC_Get_ContadorNotaCreditoAdm();
        DtoLib.ResultadoEntidad<int>
            CxC_Get_ContadorNotaDebitoAdm();


        DtoLib.Resultado
            CxC_GestionCobro_Agregar(DtoLibPos.CxC.GestionCobro.Ficha ficha);
        DtoLib.Resultado
            CxC_GestionCobro_Verificar_Agregar(DtoLibPos.CxC.GestionCobro.Ficha ficha);

    }

}