using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado
{
    public class ImpMetBusqueda: Utils.CtrlMetodoBusq.IComp 
    {
        private string _metBusqPref;
        private LibUtilitis.CtrlCB.ICtrl _ctrl;


        public LibUtilitis.CtrlCB.ICtrl Ctrl { get { return _ctrl; } }


        public ImpMetBusqueda()
        {
            _metBusqPref = "";
            _ctrl = new LibUtilitis.CtrlCB.ImpCB();
        }


        public void Inicializa()
        {
            _ctrl.Inicializa();
        }
        public void CargarData()
        {
            var _lst = new List<LibUtilitis.Opcion.IData>();
            _lst.Add(new Utils.dataFiltro() { id = "01", codigo = "", desc = "Código" });
            _lst.Add(new Utils.dataFiltro() { id = "02", codigo = "", desc = "Descripción" });
            _lst.Add(new Utils.dataFiltro() { id = "03", codigo = "", desc = "CiRif" });
            _ctrl.CargarData(_lst);
            _metBusqPref = "02";
            Limpiar();
        }
        public void Limpiar()
        {
            _ctrl.setFichaById(_metBusqPref);
        }
    }
}