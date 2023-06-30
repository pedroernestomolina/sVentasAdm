using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Buscar.Cliente
{
    public class ImpCtrlBusqueda: CtrlMetodoBusq.IComp
    {
        private LibUtilitis.CtrlCB.ICtrl _ctrl;


        public LibUtilitis.CtrlCB.ICtrl Ctrl { get { return _ctrl; } }


        public ImpCtrlBusqueda()
        {
            _ctrl = new LibUtilitis.CtrlCB.ImpCB();
        }


        public void Inicializa()
        {
            _ctrl.Inicializa();
        }
        public void CargarData()
        {
            var _lst = new List<Utils.dataFiltro>();
            _lst.Add(new dataFiltro() { id = "01", codigo = "", desc = "CODIGO" });
            _lst.Add(new dataFiltro() { id = "02", codigo = "", desc = "NOMBRE/RAZON SOCIAL" });
            _lst.Add(new dataFiltro() { id = "03", codigo = "", desc = "CI/RIF" });
            _ctrl.CargarData(_lst);
            _ctrl.setFichaById("02");
        }
        public void Limpiar()
        {
            _ctrl.LimpiarOpcion();
            _ctrl.setFichaById("02");
        }
    }
}
