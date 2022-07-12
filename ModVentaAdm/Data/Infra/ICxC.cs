using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface ICxC
    {

        OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Lista.Ficha>
            CxC_Tool_CtasPendiente_GetLista(OOB.CxC.Tools.CtasPendiente.Lista.Filtro filtro);
        OOB.Resultado.Ficha
            CxC_Agregar(OOB.CxC.AgregarCta.Ficha ficha);
        OOB.Resultado.Ficha
            CxC_AgregarNotaCreditoAdm(OOB.CxC.AgregarNotaAdm.Ficha ficha);
        OOB.Resultado.Ficha
            CxC_AgregarNotaDebitoAdm(OOB.CxC.AgregarNotaAdm.Ficha ficha);
        OOB.Resultado.Lista<OOB.CxC.DocumentosPend.Ficha>
            CxC_DocumentosPend_GetLista(OOB.CxC.DocumentosPend.Filtro filtro);


        OOB.Resultado.FichaEntidad<int>
            CxC_Get_ContadorNotaCreditoAdm();
        OOB.Resultado.FichaEntidad<int>
            CxC_Get_ContadorNotaDebitoAdm();


        OOB.Resultado.Ficha
            CxC_GestionCobro_Agregar(OOB.CxC.GestionCobro.Ficha ficha);

    }

}