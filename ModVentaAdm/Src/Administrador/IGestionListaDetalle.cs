using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Administrador
{
    
    public interface IGestionListaDetalle
    {

        BindingSource ItemsSource { get; }
        string ItemsEncontrados { get; }


        void Inicializa();
        void LimpiarData();
        void setLista(List<OOB.Documento.Lista.Ficha> list);
        List<data> GetListaDoc { get; }
        data GetItemActual { get; }

    }

}