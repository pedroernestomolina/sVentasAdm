using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Vistas
{
    public interface ImainItems
    {
        BindingSource Get_Source { get; }
        object Get_ItemActual { get; }
        int Get_CntItems { get; }
        //
        void Inicializa();
        void setAliadosInv(object aliadosInv);
        void EliminarAliadoInv(object it);
    }
}