using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado
{
    public class ImpBusqueda: Utils.Busqueda.IComp
    {
        private string _cadenaBusqueda;
        private Utils.CtrlMetodoBusq.IComp _ctrMetBusqueda;


        public BindingSource MetodoBusqueda_GetSource { get { return _ctrMetBusqueda.Ctrl.GetSource; } }
        public string MetodoBusqueda_GetId { get { return _ctrMetBusqueda.Ctrl.GetId; } }
        public string GetCadena { get { return _cadenaBusqueda; } }
        public bool HayParametrosBusqueda { get { return verificarHatyParametrosBusqueda(); } }


        public ImpBusqueda()
        {
            _cadenaBusqueda = "";
            _ctrMetBusqueda = new ImpMetBusqueda();
        }

        public void setCadenaBuscar(string cadBuscar)
        {
            _cadenaBusqueda = cadBuscar;
        }
        public void setMetodo(string id)
        {
            _ctrMetBusqueda.Ctrl.setFichaById(id);
        }

        public void Inicializa()
        {
            _cadenaBusqueda = "";
            _ctrMetBusqueda.Inicializa();
        }
        public void CargarData()
        {
            _ctrMetBusqueda.CargarData();
        }
        public void CargarData(bool forzarCargaData)
        {
            CargarData();
        }


        public void MostrarFiltros()
        {
        }
        public void Limpiar()
        {
            _cadenaBusqueda = "";
            _ctrMetBusqueda.Limpiar();
        }


        private bool verificarHatyParametrosBusqueda()
        {
            if (_cadenaBusqueda.Trim() != "") 
            {
                if (_ctrMetBusqueda.Ctrl.GetId != "") return true;
            }
            return false;
        }

        public void setFiltros(object filtrosActivar)
        {
        }

        public object DataExportar()
        {
            var _filtros = new OOB.Transporte.Aliado.Busqueda.Filtro ();
            if (_ctrMetBusqueda.Ctrl.GetItem == null)
            {
                return null;
            }
            _filtros.cadena = _cadenaBusqueda;
            switch (_ctrMetBusqueda.Ctrl.GetId)
            {
                case "01":
                    _filtros.metodoBusqueda = OOB.Transporte.Aliado.Tipos.MetodoBusqueda.PorCodigo ;
                    break;
                case "02":
                    _filtros.metodoBusqueda = OOB.Transporte.Aliado.Tipos.MetodoBusqueda.PorDescripcion ;
                    break;
                case "03":
                    _filtros.metodoBusqueda=  OOB.Transporte.Aliado.Tipos.MetodoBusqueda.PorCiRif ;
                    break;
                default:
                    _filtros.metodoBusqueda = OOB.Transporte.Aliado.Tipos.MetodoBusqueda.SinDEfnir ;
                    break;
            }
            return _filtros;
        }
    }
}