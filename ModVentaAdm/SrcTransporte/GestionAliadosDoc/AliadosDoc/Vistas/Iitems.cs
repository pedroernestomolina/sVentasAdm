using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Vistas
{
    public interface Iitems
    {
        BindingSource Get_Source { get; }
        int Get_CntItems { get; }
        object Get_ItemActual { get; }
        //
        void Inicializa();
        void setAliadosInv(object aliadosInv);
        void EliminarAliadoInv(object it);
    }
}