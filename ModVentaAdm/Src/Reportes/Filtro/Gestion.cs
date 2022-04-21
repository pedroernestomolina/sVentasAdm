using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Filtro
{
    
    public class Gestion
    {

        private IFiltro _filtro;
        private data _data;
        private bool _isOk;
        private bool _procesarIsOk;
        private List<general> _lTipoDoc;
        private List<general> _lSucursal;
        private List<general> _lEstatus;
        private BindingSource _bsSucursal;
        private BindingSource _bsEstatus;
        private BindingSource _bsTipoDoc;
        private Cliente.Lista.Gestion _gestionClienteLista;
        private Producto.Lista.Gestion _gProductoLista;


        public bool ActivarPalabraClave { get { return _filtro.ActivarPalabreClave; } }
        public string PalabraClave { get { return _data.PalabraClave; } }
        public BindingSource SourceSucursal { get { return _bsSucursal; } }
        public BindingSource SourceEstatus { get { return _bsEstatus; } }
        public BindingSource SourceTipoDoc { get { return _bsTipoDoc; } }
        public bool ActivarEstatus { get { return _filtro.ActivarEstatus; } }
        public bool ActivarDesdeHasta { get { return _filtro.ActivarDesdeHasta; } }
        public bool ActivarSucursal { get { return _filtro.ActivarSucursal; } }
        public bool ActivarMesAnoRelacion { get { return _filtro.ActivarMesAnoRelacion; } }
        public bool ActivarCliente { get { return _filtro.ActivarCliente; } }
        public bool ActivarProducto { get { return _filtro.ActivarProducto; } }
        public bool ActivarTipoDocumento { get { return _filtro.ActivarTipoDocumento; } }
        public bool IsOk { get { return _isOk; } }
        public data Data { get { return _data; } }
        public bool ProcesarIsOk { get { return _procesarIsOk; } }
        //
        public DateTime GetDesde { get { return _data.GetDesde; } }
        public DateTime GetHasta { get { return _data.GetHasta; } }
        public string GetIdSucursal { get { return _data.GetIdSucursal; } }
        public string GetCodigoSucursal { get { return _data.GetCodigoSucursal; } }
        public bool ClienteSeleccionadoIsOK { get { return _data.Cliente != null ? true : false; } }
        public string GetNombreCliente { get { return _data.ClienteNombre; } }
        public string GetIdCliente { get { return _data.ClienteId; } }
        //
        public bool ProductoSeleccionadoIsOK { get { return _data.Producto!= null ? true : false; } }
        public string GetNombreProducto { get { return _data.GetNombreProducto; } }
        public string GetIdProducto { get { return _data.GetIdProducto; } }
        //
        public string GetCodigoTipoDoc { get { return _data.GetCodigoTipoDoc; } }
        public string GetIdTipoDoc { get { return _data.GetIdTipoDoc; } }
        //
        public string GetEstatus { get { return _data.GetEstatus; } }
        public object GetIdEstatus { get { return _data.GetIdEstatus; } }
        //
        public int GetMesRelacion { get { return _data.GetMesRelacion; } }
        public int GetAnoRelacion { get { return _data.GetAnoRelacion; } }
        

        public Gestion()
        {
            _data = new data();
            _lSucursal = new List<general>();
            _lEstatus = new List<general>();
            _lTipoDoc= new List<general>();
            _bsSucursal = new BindingSource();
            _bsSucursal.DataSource = _lSucursal;
            _bsEstatus = new BindingSource();
            _bsEstatus.DataSource = _lEstatus;
            _bsTipoDoc= new BindingSource();
            _bsTipoDoc.DataSource = _lTipoDoc;
            _gestionClienteLista = new Cliente.Lista.Gestion();
            _gProductoLista = new Producto.Lista.Gestion();
        }
        

        public void Inicializa()
        {
            _isOk = false;
            _procesarIsOk = false;
            _data.Inicializa();
        }

        public bool CargarData()
        {
            var rt = true;

            var rt1 = Sistema.MyData.Sucursal_GetLista();
            if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt1.Mensaje);
                return false;
            }
            _lSucursal.Clear();
            foreach (var r in rt1.ListaD.OrderBy(o => o.nombre).ToList())
            {
                var nr = new general(r.auto, r.nombre, r.codigo);
                _lSucursal.Add(nr);
            }
            _bsSucursal.CurrencyManager.Refresh();

            _lEstatus.Clear();
            _lEstatus.Add(new general("1", "Activo"));
            _lEstatus.Add(new general("2", "Anulado"));
            _bsEstatus.CurrencyManager.Refresh();

            _lTipoDoc.Clear();
            _lTipoDoc.Add(new general("1", "Factura", "01"));
            _lTipoDoc.Add(new general("2", "Nota Débito", "02"));
            _lTipoDoc.Add(new general("3", "Nota Crédito", "03"));
            _lTipoDoc.Add(new general("4", "Nota Entrega", "04"));
            _lTipoDoc.Add(new general("5", "Presupuesto", "05"));
            _lTipoDoc.Add(new general("6", "Pedido", "06"));
            _bsTipoDoc.CurrencyManager.Refresh();

            return rt;
        }

        FiltroFrm frm;
        public void Inicia()
        {
            if (frm == null) 
            {
                frm = new FiltroFrm();
                frm.setControlador(this);
            }
            frm.ShowDialog();
        }

        public void setFiltros(IFiltro filtro)
        {
            _filtro = filtro;
        }

        public void setSucursal(string p)
        {
            _data.setSucursal(_lSucursal.FirstOrDefault(f => f.auto == p));
        }

        public void setEstatus(string p)
        {
            _data.setEstatus(_lEstatus.FirstOrDefault(f => f.auto == p));
        }

        public void setFechaDesde(DateTime desde)
        {
            _data.setFechaDesde(desde);
        }

        public void setFechaHasta(DateTime hasta)
        {
            _data.setFechaHasta(hasta);
        }

        public void setMesRelacion(int p)
        {
            _data.setMesRelacion(p);
        }

        public void setAnoRelacion(int p)
        {
            _data.setAnoRelacion(p);
        }

        public void Filtrar()
        {
            _isOk = false;
            _procesarIsOk = false;
            _data.setValidarTipoDocumento(_filtro.ValidarTipoDocumento);
            if (_data.IsOk())
            {
                _isOk = true;
                _procesarIsOk = true;
            }
        }

        public void Salir()
        {
            _isOk = true;
        }

        public void setTipoDoc(string id)
        {
            _data.setTipoDoc(_lTipoDoc.FirstOrDefault(f => f.auto == id));
        }

        public void setTipoDocFactura(bool p)
        {
            _data.setTipoDocFactura(p);
        }

        public void setTipoDocNtDebito(bool p)
        {
            _data.setTipoDocNtDebito(p);
        }

        public void setTipoDocNtCredito(bool p)
        {
            _data.setTipoDocNtCredito(p);
        }

        public void setTipoDocNtEntrega(bool p)
        {
            _data.setTipoDocNtEntrega(p);
        }

        private string _cliente;
        public void setCliente(string p)
        {
            _cliente = p.Trim();
        }

        public void BuscarCliente()
        {
            if (_cliente != "")
            {
                _gestionClienteLista.Inicializa();
                _gestionClienteLista.setBuscar(_cliente);
                _gestionClienteLista.Inicia();
                if (_gestionClienteLista.ItemSeleccionadoIsOk)
                {
                    _data.setCliente(_gestionClienteLista.ItemSeleccionado.auto, _gestionClienteLista.ItemSeleccionado.razonSocial);
                }
            }
        }

        public void LimpiarCliente()
        {
            _cliente = "";
            _data.LimpiarCliente();
        }

        public void LimpiarProducto()
        {
            _producto= "";
            _data.LimpiarProducto();
        }

        private string _producto ;
        public void setProducto(string p)
        {
            _producto = p.Trim();
        }

        public void BuscarProducto()
        {
            if (_producto!= "")
            {
                _gProductoLista.Inicializa();
                _gProductoLista.setBuscar(_producto);
                _gProductoLista.Inicia();
                if (_gProductoLista.ItemSeleccionadoIsOk)
                {
                    _data.setProducto(_gProductoLista.ItemSeleccionado.auto, _gProductoLista.ItemSeleccionado.descripcion);
                }
            }
        }

        public string GetFiltros()
        {
            return _data.GetFiltros();
        }

        public void LimpiarFiltros()
        {
            _cliente = "";
            _producto = "";
            _data.Inicializa();
        }

        public void setPalabraClave(string p)
        {
            _data.setPalabraClave(p);
        }

    }

}