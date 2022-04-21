using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.ReportesCliente.Filtro
{
    
    public class Gestion
    {

        private IFiltro _filtro;
        private data _data;
        private bool _isOk;
        private bool _procesarIsOk;
        //
        private List<general> _lGrupo;
        private List<general> _lEstado;
        private List<general> _lZona;
        private List<general> _lVendedor;
        private List<general> _lCobrador;
        private List<general> _lCategoria;
        private List<general> _lNivel;
        private List<general> _lTarifa;
        private List<general> _lEstatus;
        private List<general> _lCredito;
        //
        private BindingSource _bsGrupo;
        private BindingSource _bsEstado;
        private BindingSource _bsZona;
        private BindingSource _bsVendedor;
        private BindingSource _bsCobrador;
        private BindingSource _bsCategoria;
        private BindingSource _bsNivel;
        private BindingSource _bsTarifa;
        private BindingSource _bsEstatus;
        private BindingSource _bsCredito;


        //source
        public BindingSource SourceGrupo { get { return _bsGrupo; } }
        public BindingSource SourceEstado { get { return _bsEstado; } }
        public BindingSource SourceZona { get { return _bsZona; } }
        public BindingSource SourceVendedor { get { return _bsVendedor; } }
        public BindingSource SourceCobrador { get { return _bsCobrador; } }
        public BindingSource SourceCategoria { get { return _bsCategoria; } }
        public BindingSource SourceNivel { get { return _bsNivel; } }
        public BindingSource SourceTarifa { get { return _bsTarifa; } }
        public BindingSource SourceEstatus { get { return _bsEstatus; } }
        public BindingSource SourceCredito { get { return _bsCredito; } }

        //
        public bool ActivarGrupo { get { return _filtro.ActivarGrupo; } }
        public bool ActivarZona { get { return _filtro.ActivarZona; } }
        public bool ActivarEstado { get { return _filtro.ActivarEstado; } }
        public bool ActivarVendedor { get { return _filtro.ActivarVendedor; } }
        public bool ActivarCobrador { get { return _filtro.ActivarCobrador; } }
        public bool ActivarCategoria { get { return _filtro.ActivarCategoria; } }
        public bool ActivarNivel { get { return _filtro.ActivarNivel; } }
        public bool ActivarCredito { get { return _filtro.ActivarCredito; } }
        public bool ActivarEstatus { get { return _filtro.ActivarEstatus; } }
        public bool ActivarTarifa { get { return _filtro.ActivarTarifa; } }
        //
        public bool IsOk { get { return _isOk; } }
        public data Data { get { return _data; } }
        public bool ProcesarIsOk { get { return _procesarIsOk; } }


        public Gestion()
        {
            _data = new data();
            _lGrupo = new List<general>();
            _lEstado = new List<general>();
            _lZona = new List<general>();
            _lVendedor = new List<general>();
            _lCobrador = new List<general>();
            _lCategoria= new List<general>();
            _lNivel= new List<general>();
            _lTarifa= new List<general>();
            _lEstatus = new List<general>();
            _lCredito = new List<general>();
            //
            _bsGrupo = new BindingSource();
            _bsEstado = new BindingSource();
            _bsZona = new BindingSource();
            _bsVendedor= new BindingSource();
            _bsCobrador= new BindingSource();
            _bsCategoria= new BindingSource();
            _bsNivel = new BindingSource();
            _bsTarifa= new BindingSource();
            _bsEstatus = new BindingSource();
            _bsCredito= new BindingSource();
            //
            _bsGrupo.DataSource = _lGrupo;
            _bsEstado.DataSource = _lEstado;
            _bsZona.DataSource = _lZona;
            _bsVendedor.DataSource = _lVendedor;
            _bsCobrador.DataSource = _lCobrador;
            _bsCategoria.DataSource = _lCategoria;
            _bsNivel.DataSource = _lNivel;
            _bsTarifa.DataSource = _lTarifa;
            _bsEstatus.DataSource = _lEstatus;
            _bsCredito.DataSource = _lCredito;
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

            var rt1 = Sistema.MyData.ClienteGrupo_GetLista(new OOB.Maestro.Grupo.Lista.Filtro());
            if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt1.Mensaje);
                return false;
            }
            _lGrupo.Clear();
            foreach (var it in rt1.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lGrupo.Add(new general(it.auto, it.nombre));
            }
            _bsGrupo.CurrencyManager.Refresh();

            var rt2 = Sistema.MyData.ClienteZona_GetLista (new OOB.Maestro.Zona.Lista.Filtro());
            if (rt2.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt2.Mensaje);
                return false;
            }
            _lZona.Clear();
            foreach (var it in rt2.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lZona.Add(new general(it.auto, it.nombre));
            }
            _bsZona.CurrencyManager.Refresh();

            var rt3 = Sistema.MyData.Sistema_Estado_GetLista();
            if (rt3.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt3.Mensaje);
                return false;
            }
            _lEstado.Clear();
            foreach (var it in rt3.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lEstado.Add(new general(it.auto, it.nombre));
            }
            _bsEstado.CurrencyManager.Refresh();

            var rt4 = Sistema.MyData.Sistema_Vendedor_GetLista();
            if (rt4.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt4.Mensaje);
                return false;
            }
            _lVendedor.Clear();
            foreach (var it in rt4.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lVendedor.Add(new general(it.id, it.nombre));
            }
            _bsVendedor.CurrencyManager.Refresh();

            var rt5 = Sistema.MyData.Sistema_Cobrador_GetLista();
            if (rt5.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt5.Mensaje);
                return false;
            }
            _lCobrador.Clear();
            foreach (var it in rt5.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lCobrador.Add(new general(it.id, it.nombre));
            }
            _bsCobrador.CurrencyManager.Refresh();

            _lCategoria.Clear();
            _lCategoria.Add(new general("01", "Administrativo"));
            _lCategoria.Add(new general("02", "Eventual"));
            _bsCategoria.CurrencyManager.Refresh();

            _lNivel.Clear();
            _lNivel.Add(new general("00", "Sin Definir"));
            _lNivel.Add(new general("01", "A"));
            _lNivel.Add(new general("02", "B"));
            _lNivel.Add(new general("03", "C"));
            _bsNivel.CurrencyManager.Refresh();

            _lTarifa.Clear();
            _lTarifa.Add(new general("00", "Sin Definir"));
            _lTarifa.Add(new general("01", "1"));
            _lTarifa.Add(new general("02", "2"));
            _lTarifa.Add(new general("03", "3"));
            _lTarifa.Add(new general("04", "4"));
            _lTarifa.Add(new general("05", "5"));
            _bsTarifa.CurrencyManager.Refresh();

            _lEstatus.Clear();
            _lEstatus.Add(new general("01", "Activo"));
            _lEstatus.Add(new general("02", "Inactivo"));
            _bsEstatus.CurrencyManager.Refresh();

            _lCredito.Clear();
            _lCredito.Add(new general("01", "Activo"));
            _lCredito.Add(new general("02", "Inactivo"));
            _bsCredito.CurrencyManager.Refresh();

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

        public void Filtrar()
        {
            _isOk = false;
            _procesarIsOk = false;
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

        public void setGrupo(string p)
        {
            _data.setGrupo(_lGrupo.FirstOrDefault(f=>f.Id==p));
        }

        public void setEstado(string p)
        {
            _data.setEstado(_lEstado.FirstOrDefault(f => f.Id == p));
        }

        public void setZona(string p)
        {
            _data.setZona(_lZona.FirstOrDefault(f => f.Id == p));
        }

        public void setVendedor(string p)
        {
            _data.setVendedor(_lVendedor.FirstOrDefault(f => f.Id == p));
        }

        public void setCobrador(string p)
        {
            _data.setCobrador(_lCobrador.FirstOrDefault(f => f.Id == p));
        }

        public void setCategoria(string p)
        {
            _data.setCategoria(_lCategoria.FirstOrDefault(f => f.Id == p));
        }

        public void setNivel(string p)
        {
            _data.setNivel(_lNivel.FirstOrDefault(f => f.Id == p));
        }

        public void setCredito(string p)
        {
            _data.setCredito(_lCredito.FirstOrDefault(f => f.Id == p));
        }

        public void setEstatus(string p)
        {
            _data.setEstatus(_lEstatus.FirstOrDefault(f => f.Id == p));
        }

        public void setTarifa(string p)
        {
            _data.setTarifa(_lTarifa.FirstOrDefault(f => f.Id == p));
        }

    }

}