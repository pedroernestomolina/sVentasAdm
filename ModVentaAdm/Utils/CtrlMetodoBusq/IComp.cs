using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.CtrlMetodoBusq
{
    public interface IComp
    {
        LibUtilitis.CtrlCB.ICtrl Ctrl { get; }

        void Inicializa();
        void CargarData();
        void Limpiar();
    }
}