using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Comp.Handler
{
    public class Imp: Utils.Componente.Filtro.Handler.baseImp, Vista.IVista
    {
        private Opcion.Vista.IPorCombo _opcPorGrupo;
        private Opcion.Vista.IPorCombo _opcPorEstado;
        private Opcion.Vista.IPorCombo _opcPorZona;
        private Opcion.Vista.IPorCombo _opcPorVendedor;
        private Opcion.Vista.IPorCombo _opcPorCobrador;
        private Opcion.Vista.IPorCombo _opcPorCategoria;
        private Opcion.Vista.IPorCombo _opcPorNivel;
        private Opcion.Vista.IPorCombo _opcPorEstatus;
        private Opcion.Vista.IPorCombo _opcPorCredito;
        //
        public Opcion.Vista.IPorCombo OpcPorGrupo { get { return _opcPorGrupo; } }
        public Opcion.Vista.IPorCombo OpcPorEstado { get { return _opcPorEstado; } }
        public Opcion.Vista.IPorCombo OpcPorZona { get { return _opcPorZona; } }
        public Opcion.Vista.IPorCombo OpcPorVendedor { get { return _opcPorVendedor; } }
        public Opcion.Vista.IPorCombo OpcPorCobrador { get { return _opcPorCobrador; } }
        public Opcion.Vista.IPorCombo OpcPorCategoria { get { return _opcPorCategoria; } }
        public Opcion.Vista.IPorCombo OpcPorNivel { get { return _opcPorNivel; } }
        public Opcion.Vista.IPorCombo OpcPorEstatus { get { return _opcPorEstatus; } }
        public Opcion.Vista.IPorCombo OpcPorCredito { get { return _opcPorCredito; } }

        //
        public Imp()
            :base()
        {
            _opcPorGrupo = new Opcion.Handler.ImpPor();
            _opcPorEstado = new Opcion.Handler.ImpPor();
            _opcPorZona = new Opcion.Handler.ImpPor();
            _opcPorVendedor = new Opcion.Handler.ImpPor();
            _opcPorCobrador = new Opcion.Handler.ImpPor();
            _opcPorCategoria = new Opcion.Handler.ImpPor();
            _opcPorNivel = new Opcion.Handler.ImpPor();
            _opcPorEstatus = new Opcion.Handler.ImpPor();
            _opcPorCredito = new Opcion.Handler.ImpPor();
        }
        public override void Inicializa()
        {
            _opcPorGrupo.Inicializa();
            _opcPorEstado.Inicializa();
            _opcPorZona.Inicializa();
            _opcPorVendedor.Inicializa();
            _opcPorCobrador.Inicializa();
            _opcPorCategoria.Inicializa();
            _opcPorNivel.Inicializa();
            _opcPorEstatus.Inicializa();
            _opcPorCredito.Inicializa();
        }
        Vista.Frm frm;
        public override void Inicia()
        {
            if (cargarDataIsOk())
            {
                if (frm == null)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setOpcionesFiltrado(Opciones.IOpciones opciones)
        {
            _opcPorGrupo.Opcion.setHabilitarOpcion(opciones.ActivarGrupo);
            _opcPorEstado.Opcion.setHabilitarOpcion(opciones.ActivarEstado);
            _opcPorZona.Opcion.setHabilitarOpcion(opciones.ActivarZona);
            _opcPorVendedor.Opcion.setHabilitarOpcion(opciones.ActivarVendedor);
            _opcPorCobrador.Opcion.setHabilitarOpcion(opciones.ActivarCobrador);
            _opcPorCategoria.Opcion.setHabilitarOpcion(opciones.ActivarCategoria);
            _opcPorNivel.Opcion.setHabilitarOpcion(opciones.ActivarNivel);
            _opcPorEstatus.Opcion.setHabilitarOpcion(opciones.ActivarEstatus);
            _opcPorCredito.Opcion.setHabilitarOpcion(opciones.ActivarCredito);
        }
        //
        private bool cargarDataIsOk()
        {
            try
            {
                var lst = new List<LibUtilitis.Opcion.IData>();
                //
                var _lstGrupo = Sistema.Fabrica.DataCliente.Grupo_GetLista();
                foreach (var rg in _lstGrupo.ToList())
                {
                    var nr = new  Opcion.Handler.dataComun()
                    {
                        codigo = rg.codigo,
                        desc = rg.nombre,
                        id = rg.auto,
                    };
                    lst.Add(nr);
                }
                OpcPorGrupo.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstEstado = Sistema.Fabrica.DataCliente.Estados_GetLista();
                foreach (var rg in _lstEstado.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.nombre,
                        id = rg.auto,
                    };
                    lst.Add(nr);
                }
                OpcPorEstado.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstZonas = Sistema.Fabrica.DataCliente.Zonas_GetLista();
                foreach (var rg in _lstEstado.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.nombre,
                        id = rg.auto,
                    };
                    lst.Add(nr);
                }
                OpcPorZona.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstVend = Sistema.Fabrica.DataCliente.Vendedores_GetLista();
                foreach (var rg in _lstEstado.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.nombre,
                        id = rg.auto,
                    };
                    lst.Add(nr);
                }
                OpcPorVendedor.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstCob = Sistema.Fabrica.DataCliente.Cobradores_GetLista ();
                foreach (var rg in _lstCob.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.nombre,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                OpcPorCobrador.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstCat = Sistema.Fabrica.DataCliente.Categorias_GetLista();
                foreach (var rg in _lstCat.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.desc,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                OpcPorCategoria.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstNiv = Sistema.Fabrica.DataCliente.Nivel_GetLista();
                foreach (var rg in _lstNiv.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.desc,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                OpcPorNivel.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstEst = Sistema.Fabrica.DataCliente.Estatus_GetLista();
                foreach (var rg in _lstEst.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.desc,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                OpcPorEstatus.Opcion.CargarData(lst);
                //
                lst = new List<LibUtilitis.Opcion.IData>();
                var _lstCred = Sistema.Fabrica.DataCliente.Credito_GetLista();
                foreach (var rg in _lstCred.ToList())
                {
                    var nr = new Opcion.Handler.dataComun()
                    {
                        codigo = "",
                        desc = rg.desc,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                OpcPorCredito.Opcion.CargarData(lst);
                //
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}