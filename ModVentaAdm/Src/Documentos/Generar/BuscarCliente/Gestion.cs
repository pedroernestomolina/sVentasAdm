using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarCliente
{

    public class Gestion
    {


        private Cliente.AgregarEditar.Gestion _gestionAgregarEditar;
        private Cliente.Buscar.Busqueda.Gestion _buscar;
        private Cliente.Buscar.Items.Gestion _items;
        private bool _seleccionatItemIsActivo;
        private Cliente.Buscar.Items.data _itemSeleccionado;
        private bool _itemSeleccionadoIsOk; 

        
        public int CntItem { get { return _items.Cnt; } }
        public BindingSource ItemsSource { get { return _items.ItemSource; } }
        public Cliente.Buscar.Busqueda.Enumerados.enumMetodoBusqueda MetodoBusqueda { get { return _buscar.MetodoBusqueda; } }
        public Cliente.Buscar.Items.data ItemSeleccionado { get { return _itemSeleccionado; } }
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }


        public Gestion()
        {
            _itemSeleccionado = null;
            _gestionAgregarEditar = new Cliente.AgregarEditar.Gestion();
            _buscar = new Cliente.Buscar.Busqueda.Gestion();
            _items = new Cliente.Buscar.Items.Gestion();
            _seleccionatItemIsActivo = false;
            _itemSeleccionadoIsOk = false;
        }


        public void Inicializa() 
        {
            _itemSeleccionado = null;
            _itemSeleccionadoIsOk = false;
            _seleccionatItemIsActivo = false;
            _buscar.Inicializa();
            _items.Inicializa();
        }

        BuscarClienteFrm _frm;
        public void Inicia() 
        {
            if (CargarData()) 
            {
                if (_frm == null) 
                {
                    _frm = new BuscarClienteFrm();
                    _frm.setControlador(this);
                }
                _frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return (_buscar.CargarData() && _items.CargarData());
        }

        public void setCadena(string p)
        {
            _buscar.setCadena(p);
        }

        public void ActivarBusqueda()
        {
            var filtroOOB = _buscar.GenerarFiltro();
            if (filtroOOB == null) { return; }
            var r01 = Sistema.MyData.Cliente_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _items.setLista(r01.ListaD);
        }

        public void setMetodoPorCodigo()
        {
            _buscar.setMetodoPorCodigo();
        }

        public void setMetodoPorNombre()
        {
            _buscar.setMetodoPorNombre();
        }

        public void setMetodoPorCiRif()
        {
            _buscar.setMetodoPorCiRif();
        }

        public void LimpiarBusqueda()
        {
            _buscar.LimpiarBusqueda();
            _items.LimpiarLista();
        }

        public void SeleccionarItem()
        {
            if (_seleccionatItemIsActivo)
            {
                if (_items.Item.Id !="")
                {
                    if (!_items.Item.IsActivo) 
                    {
                        Helpers.Msg.Error("CLIENTE SELECCIONADO EN ESTADO INACTIVO");
                        return;
                    }
                    _itemSeleccionado = _items.Item;
                    _itemSeleccionadoIsOk = true;
                }
            }
        }

        public void setActivarSeleccionItem(bool p)
        {
            _seleccionatItemIsActivo = p;
        }

        public void AgregarCliente()
        {
            var r00 = Sistema.MyData.Permiso_Cliente_Agregar(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionAgregarEditar.setGestion(new Cliente.AgregarEditar.Agregar.Gestion());
                _gestionAgregarEditar.Inicializa();
                _gestionAgregarEditar.Inicia();
                if (_gestionAgregarEditar.ProcesarIsoK)
                {
                    InsertarFichaLista(_gestionAgregarEditar.AutoClienteAgregado);
                }
            }
        }

        private void InsertarFichaLista(string autoId)
        {
            var r01 = Sistema.MyData.Cliente_GetFicha(autoId);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _items.AgregarFicha(r01.Entidad);
        }

    }

}