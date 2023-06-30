using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Buscar.Cliente
{
    public class ImpCompBusqueda: Busqueda.IComp
    {
        private string _cadenaBuscar;
        private CtrlMetodoBusq.IComp _ctrl;


        public BindingSource MetodoBusqueda_GetSource { get { return _ctrl.Ctrl.GetSource; } }
        public string MetodoBusqueda_GetId { get { return _ctrl.Ctrl.GetId; } }
        public string GetCadena { get { return _cadenaBuscar; } }
        public bool HayParametrosBusqueda { get { return hayParametrosBusqueda(); } }
        public object DataExportar() { return dataExportar(); }


        public ImpCompBusqueda()
        {
            _cadenaBuscar = "";
            _ctrl = new ImpCtrlBusqueda();
        }


        public void Inicializa()
        {
            _cadenaBuscar = "";
            _ctrl.Inicializa();
        }
        public void CargarData()
        {
            _ctrl.CargarData();
        }
        public void CargarData(bool fozarCargaData)
        {
        }
        public void MostrarFiltros()
        {
        }
        public void Limpiar()
        {
            _cadenaBuscar = "";
            _ctrl.Limpiar();
        }

        public void setCadenaBuscar(string cadBuscar)
        {
            _cadenaBuscar = cadBuscar;
        }
        public void setMetodo(string id)
        {
            _ctrl.Ctrl.setFichaById(id);
        }
        public void setFiltros(object filtrosActivar)
        {
        }


        private bool hayParametrosBusqueda()
        {
            if (_ctrl.Ctrl.GetItem != null)
            {
                if (_cadenaBuscar.Trim() != "")
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                Helpers.Msg.Alerta("NO SE HA DEFINIDO METODO DE BUSQUEDA");
                return false;
            }
        }
        private object dataExportar()
        {
            var rt = new OOB.Maestro.Cliente.Lista.Filtro();
            rt.cadena = _cadenaBuscar;
            switch (_ctrl.Ctrl.GetId)
            {
                case "01":
                    rt.metodoBusqueda = OOB.Maestro.Cliente.Lista.Enumerados.enumMetodoBusqueda.PorCodigo;
                    break;
                case "02":
                    rt.metodoBusqueda = OOB.Maestro.Cliente.Lista.Enumerados.enumMetodoBusqueda.PorNombre;
                    break;
                case "03":
                    rt.metodoBusqueda = OOB.Maestro.Cliente.Lista.Enumerados.enumMetodoBusqueda.PorRif;
                    break;
            }
            return  rt;
        }
    }
}