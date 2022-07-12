using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.Reportes.ListaDocPend
{
    
    public interface IRepDocPend: IReporte
    {

        void setListaDoc(List<DocumentosPend.ListaDocPend.data> list);

    }

}