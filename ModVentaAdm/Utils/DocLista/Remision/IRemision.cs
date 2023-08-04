using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public interface IRemision: IDocLista, Src.Gestion.IAbandonar
    {
        bool ItemSeleccionadoIsOk { get; }
        void SeleccionarItem();
        void setDataCargar(List<OOB.Transporte.Documento.Remision.Lista.Ficha> list);
    }
}
