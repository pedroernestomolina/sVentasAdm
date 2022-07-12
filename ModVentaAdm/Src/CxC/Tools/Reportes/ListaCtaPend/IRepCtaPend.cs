using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.Reportes.ListaCtaPend
{
    
    public interface IRepCtaPend: IReporte
    {

        void setListaDoc(List<PanelPrincipal.ListaCtasPend.data> list);

    }

}