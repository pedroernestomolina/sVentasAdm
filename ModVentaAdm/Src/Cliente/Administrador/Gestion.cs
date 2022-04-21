using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Administrador
{

    public class Gestion
    {


        private OOB.Configuracion.BusquedaCliente.Entidad.Ficha _metodoBusqPred;
        private dataFiltro _filtrar;
        private GestionLista _gestionLista;
        private AgregarEditar.Gestion _gestionAgregarEditar;
        private Articulos.Gestion _gestionArticulos;
        private Documentos.Gestion _gestionDocumentos;
        private Estatus.Gestion _gestionEstatus;
        private Visualizar.Gestion _gestionVisualizar;


        public int cntItem { get { return _gestionLista.Items; } }
        public Enumerados.enumMetodoBusqueda MetodoBusqueda { get { return _filtrar.MetodoBusqueda; } }
        public BindingSource Source { get { return _gestionLista.Source; } }
        public string Cliente { get { return _gestionLista.Cliente; } }
        public data Item { get { return _gestionLista.Item; } }


        public Gestion()
        {
            _metodoBusqPred = null;
            _filtrar = new dataFiltro();
            _gestionLista = new GestionLista();
            _gestionLista.ItemChanged += _gestionLista_ItemChanged;
            _gestionAgregarEditar = new AgregarEditar.Gestion();
            _gestionArticulos = new Articulos.Gestion();
            _gestionDocumentos = new Documentos.Gestion();
            _gestionEstatus = new Estatus.Gestion();
            _gestionVisualizar = new Visualizar.Gestion();
        }


        private void _gestionLista_ItemChanged(object sender, EventArgs e)
        {
            frm.ActualizarFicha();
        }

        private AdmFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new AdmFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.Configuracion_BusquedaCliente();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _metodoBusqPred = r01.Entidad;
            asignaMetodoBusqueda(r01.Entidad);

            return rt;
        }

        private void asignaMetodoBusqueda(OOB.Configuracion.BusquedaCliente.Entidad.Ficha ficha)
        {
            switch (ficha.ModoBusqueda)
            {
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.Codigo:
                    _filtrar.setMetodoPorCodigo();
                    break;
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.Nombre:
                    _filtrar.setMetodoPorNombre();
                    break;
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.CiRif:
                    _filtrar.setMetodoPorCiRif();
                    break;
            }
        }

        public void ActivarBusqueda()
        {
            var filtroOOB = new OOB.Maestro.Cliente.Lista.Filtro()
            {
                cadena = _filtrar.cadena,
                metodoBusqueda = (OOB.Maestro.Cliente.Lista.Enumerados.enumMetodoBusqueda)_filtrar.MetodoBusqueda,
            };
            var r01 = Sistema.MyData.Cliente_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _gestionLista.setLista(r01.ListaD);
            _filtrar.Limpiar();
            asignaMetodoBusqueda(_metodoBusqPred);
        }

        public void Inicializa()
        {
            _filtrar.Limpiar();
        }

        public void setCadena(string p)
        {
            _filtrar.setCadena(p);
        }

        public void setMetodoPorCodigo()
        {
            _filtrar.setMetodoPorCodigo();
        }

        public void setMetodoPorNombre()
        {
            _filtrar.setMetodoPorNombre();
        }

        public void setMetodoPorCiRif()
        {
            _filtrar.setMetodoPorCiRif();
        }

        public void LimpiarBusqueda()
        {
            _gestionLista.LimpiarLista();
        }

        public void AgregarFicha()
        {
            var r00 = Sistema.MyData.Permiso_Cliente_Agregar(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionAgregarEditar.setGestion(new AgregarEditar.Agregar.Gestion());
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
            _gestionLista.AgregarFicha(r01.Entidad);
        }

        public void EditarFicha()
        {
            if (Item != null)
            {
                if (!Item.IsActivo) 
                {
                    Helpers.Msg.Error("CLIENTE EN ESTADO INACTIVO");
                    return;
                }

                var r00 = Sistema.MyData.Permiso_Cliente_Editar(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }

                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    _gestionAgregarEditar.setGestion(new AgregarEditar.Editar.Gestion());
                    _gestionAgregarEditar.Inicializa();
                    _gestionAgregarEditar.setFichaEditar(Item.Id);
                    _gestionAgregarEditar.Inicia();
                    if (_gestionAgregarEditar.ProcesarIsoK)
                    {
                        ActualizarFichaLista(Item.Id);
                    }
                }
            }
        }

        private void ActualizarFichaLista(string autoId)
        {
            var r01 = Sistema.MyData.Cliente_GetFicha(autoId);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _gestionLista.ActualizarFicha(r01.Entidad);
        }

        public void CompraArticulos()
        {
            if (Item != null)
            {
                _gestionArticulos.Inicializa();
                _gestionArticulos.setCliente(Item);
                _gestionArticulos.Inicia();
            }
        }

        public void Documentos()
        {
            if (Item != null)
            {
                _gestionDocumentos.Inicializa();
                _gestionDocumentos.setHabilitarSeleccionarDocumento(false);
                _gestionDocumentos.setHabilitarVisualizarDocumento(true);
                _gestionDocumentos.setCliente(Item);
                _gestionDocumentos.Inicia();
            }
        }

        public void ActualizarEstatus()
        {
            if (Item != null)
            {
                var r00 = Sistema.MyData.Permiso_Cliente_ActivarInactivar(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }

                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    _gestionEstatus.Inicializa();
                    _gestionEstatus.setCliente(Item.Id);
                    _gestionEstatus.Inicia();
                    if (_gestionEstatus.ProcesarIsOk)
                    {
                        ActualizarFichaLista(Item.Id);
                    }
                }
            }
        }

        public void VisualizarFicha()
        {
            if (Item != null)
            {
                _gestionVisualizar.Inicializa();
                _gestionVisualizar.setFicha(Item.Id);
                _gestionVisualizar.Inicia();
            }
        }

    }

}