using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item
{
    public class fecha
    {
        public int id { get; set; }
        public DateTime fechaServ { get; set; }
        public DateTime horaServ { get; set; }
        public string desc { get { return fechaServ.ToShortDateString() + ", " + horaServ.ToShortTimeString(); } }
    }
    public class data
    {
        private string _desc;
        private string _solicitadoPor;
        private string _moduloCargar;
        private int _cntDias;
        private int _cntUnidades;
        private decimal _precioDivisa;
        private decimal _tasaIva;
        private decimal _importe;
        private OOB.Transporte.Aliado.Entidad.Ficha _aliado;
        private DateTime _fechaServ;
        private DateTime _horaServ;
        private List<fecha> _fechas;
        private BindingSource _bsFechas;
        private decimal _precioAliadoPautado;
        private int _cntAliadoPautado;
        private string _descripcionFull;
        private decimal _dscto;
        private decimal _precioDscto;
        private alicuota _alicuota;
        private AliadosLlamado.ILLamados _aliadosLlamados;


        public BindingSource Get_SourceFechas { get { return _bsFechas; } }
        public BindingSource Get_SourceAliadosLlamados { get { return _aliadosLlamados.Get_Source; } }
        public List<AliadosLlamado.data> Get_ListaAliadosLLamados { get { return _aliadosLlamados.GetLista; } }
        public decimal Get_ImporteAliadosLLamados { get { return _aliadosLlamados.Get_Importe; } }

        //
        public string Get_Descripcion { get { return _desc; } }
        public string Get_SolicitadoPor { get { return _solicitadoPor; } }
        public string Get_ModuloCargar { get { return _moduloCargar; } }
        public int Get_CntDias { get { return _cntDias; } }
        public int Get_CntUnidades { get { return _cntUnidades; } }
        public decimal Get_PrecioDivisa { get { return _precioDivisa; } }
        public decimal Get_Dscto { get { return _dscto; } }
        public decimal Get_Importe { get { return _importe; } }
        public decimal Get_Aliado_PrecioPautado { get { return _precioAliadoPautado; } }
        public int Get_Aliado_CntPautado { get { return _cntAliadoPautado; } }
        public string Get_DescripcionFull { get { return _descripcionFull; } }
        public List<fecha> Get_Fechas { get { return _fechas; } }
        public DateTime Get_Fecha { get { return _fechaServ; } }
        public DateTime Get_Hora { get { return _fechaServ; } }
        public OOB.Transporte.Aliado.Entidad.Ficha Get_Aliado { get { return _aliado; } }
        public bool AliadoIsOk { get { return (_aliadosLlamados != null && _aliadosLlamados.GetLista.Count > 0); } }
        public decimal Get_Iva
        {
            get
            {
                var rt = 0m;
                if (_tasaIva > 0m)
                {
                    rt = _importe * _tasaIva / 100;
                }
                return rt;
            }
        }
        public string Get_Aliado_ItemMostrar
        {
            get
            {
                var rt = "NO DEFINIDO";
                if (_aliado != null)
                {
                    rt = _aliado.ciRif.Trim() + "(" + _aliado.nombreRazonSocial.Trim() + ")";
                }
                return rt;
            }
        }
        public string Get_Aliado_Inf
        {
            get
            {
                var rt = "";
                if (_aliado != null)
                {
                    rt = _aliado.codigo + Environment.NewLine +
                        _aliado.ciRif + Environment.NewLine +
                        _aliado.nombreRazonSocial;
                }
                return rt;
            }
        }


        public data()
        {
            limpiar();
            _bsFechas = new BindingSource();
            _bsFechas.DataSource = _fechas;
            _bsFechas.CurrencyManager.Refresh();
            _aliadosLlamados = new AliadosLlamado.Imp();
        }

        public void Inicializa()
        {
            limpiar();
            _bsFechas.CurrencyManager.Refresh();
            _aliadosLlamados.Inicializa();
        }


        public void setDescripcion(string desc)
        {
            _desc = desc;
        }
        public void setSolicitadoPor(string desc)
        {
            _solicitadoPor = desc;
        }
        public void setModuloaCargar(string desc)
        {
            _moduloCargar = desc;
        }
        public void setCntDias(int cnt)
        {
            _cntDias = cnt;
            CalculaImporte();
        }
        public void setCntUnidades(int cnt)
        {
            _cntUnidades = cnt;
            CalculaImporte();
        }
        public void setPrecioDivisa(decimal mnto)
        {
            _precioDivisa = mnto;
            _precioDscto = mnto;
            CalculaImporte();
        }
        public void setAliado(OOB.Transporte.Aliado.Entidad.Ficha ficha)
        {
            _aliado = ficha;
        }
        public void setTasaIva(decimal tasa)
        {
            _tasaIva = tasa;
        }
        public void setDscto(decimal tasa)
        {
            _dscto = tasa;
            CalculaImporte();
        }

        private void CalcularDscto()
        {
            _precioDscto = _precioDivisa - (_precioDivisa * _dscto / 100);
        }
        private void CalculaImporte()
        {
            _precioDscto = _precioDivisa - (_precioDivisa * _dscto / 100);
            _importe = (_cntDias * _cntUnidades * _precioDscto);
        }
        private void limpiar()
        {
            _aliado = null;
            _alicuota = null;
            _desc = "";
            _solicitadoPor = "";
            _moduloCargar = "";
            _descripcionFull = "";
            _cntUnidades = 0;
            _cntDias = 0;
            _precioDivisa = 0m;
            _precioDscto = 0m;
            _tasaIva = 0m;
            _dscto = 0m;
            _importe = 0m;
            _fechaServ = DateTime.Now;
            _horaServ = DateTime.Now;
            _precioAliadoPautado = 0m;
            _cntAliadoPautado = 0;
            _fechas = new List<fecha>();
        }
        public void LimpiarAliado()
        {
            _aliado = null;
        }
        public void AgregarFecha()
        {
            var _id = 0;
            if (_fechas.Count == 0)
                _id = 1;
            else
                _id = _fechas.Max(m => m.id) + 2;
            var nr = new fecha()
            {
                fechaServ = _fechaServ,
                horaServ = _horaServ,
                id = _id,
            };
            _fechas.Add(nr);
            _bsFechas.DataSource = _fechas;
            _bsFechas.CurrencyManager.Refresh();
            setCntDias(_fechas.Count);
            CalculaImporte();
        }
        public void EliminarFecha()
        {
            if (_bsFechas.Current != null)
            {
                var _item = (fecha)_bsFechas.Current;
                _fechas.Remove(_item);
                _bsFechas.DataSource = _fechas;
                _bsFechas.CurrencyManager.Refresh();
                setCntDias(_fechas.Count);
                CalculaImporte();
            }
        }

        public void setFecha(DateTime fecha)
        {
            _fechaServ = fecha.Date;
        }
        public void setHora(DateTime hora)
        {
            _horaServ = hora;
        }
        public void setPrecioAliadoPautado(decimal _monto)
        {
            _precioAliadoPautado = _monto;
        }
        public void setCntAliadoPautado(int cnt)
        {
            _cntAliadoPautado = cnt;
        }
        public void setDescripcionFull(string desc)
        {
            _descripcionFull = desc;
        }
        public bool VerificarDatosIsOK()
        {
            if (_desc.Trim() == "")
            {
                Helpers.Msg.Alerta("Campo [ DESCRIPCION BREVE ] No puede estar vacio !!!");
                return false;
            }
            if (_solicitadoPor.Trim() == "")
            {
                Helpers.Msg.Alerta("Campo [ SOLICITADO POR ] No puede estar vacio !!!");
                return false;
            }
            if (_moduloCargar.Trim() == "")
            {
                Helpers.Msg.Alerta("Campo [ MODULO / CARGAR ] No puede estar vacio !!!");
                return false;
            }
            if (_fechas.Count == 0)
            {
                Helpers.Msg.Alerta("Campo [ FECHAS ] No puede estar vacio !!!");
                return false;
            }
            if (_cntUnidades == 0)
            {
                Helpers.Msg.Alerta("Campo [ CANT UNIDADES ] No puede estar vacio !!!");
                return false;
            }
            if (_precioDivisa == 0m)
            {
                Helpers.Msg.Alerta("Campo [ PRECIO ] No puede estar vacio !!!");
                return false;
            }
            if (_alicuota == null)
            {
                Helpers.Msg.Alerta("Campo [ ALICUOTA ] No puede estar vacio !!!");
                return false;
            }
            return true;
        }

        public string Get_Alicuota_ID { get { return _alicuota == null ? "" : _alicuota.id; } }
        public alicuota Get_Alicuota { get { return _alicuota; } }
        public void setAlicuota(alicuota ficha)
        {
            _alicuota = ficha;
        }


        public void GuardarAliado()
        {
            if (aliadoRegistrarIsOK())
            {
                var data = new AliadosLlamado.data() { aliado = _aliado, precio = _precioAliadoPautado, cnt = _cntAliadoPautado };
                _aliadosLlamados.Agregar(data);
                limpiarDataAliado();
            }
        }
        public void EliminarAliado()
        {
            if (_aliadosLlamados.ItemActual != null)
            {
                var _item = _aliadosLlamados.ItemActual;
                _aliadosLlamados.Eliminar(_item);
            }
        }


        private void limpiarDataAliado()
        {
            setAliado(null);
            setPrecioAliadoPautado(0m);
            setCntAliadoPautado(0);
        }
        private bool aliadoRegistrarIsOK()
        {
            var rt = true;
            if (_aliado == null)
            {
                Helpers.Msg.Alerta("ALIADO NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_precioAliadoPautado == 0m)
            {
                Helpers.Msg.Alerta("PRECIO NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_cntAliadoPautado == 0)
            {
                Helpers.Msg.Alerta("CANTIDAD NO PUEDE ESTAR VACIO");
                return false;
            }
            return rt;
        }

        public  void setListaAliadosLlamados(List<AliadosLlamado.data> lst)
        {
            _aliadosLlamados.setListaAliadosLlamados(lst);
        }
    }
}