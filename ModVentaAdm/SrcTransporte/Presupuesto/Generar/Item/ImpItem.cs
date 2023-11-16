using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item
{
    abstract public class ImpItem : IItem
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _data;
        private Aliado.IAliado _aliado;
        private LibUtilitis.CtrlCB.ICtrl _alicuota;
        private LibUtilitis.CtrlCB.ICtrl _tipoServ;
        protected List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal;


        public data Item { get { return _data; } }
        public Aliado.IAliado MiAliado { get { return _aliado; } }
        public LibUtilitis.CtrlCB.ICtrl Alicuota { get { return _alicuota; } }
        public LibUtilitis.CtrlCB.ICtrl TipoServ { get { return _tipoServ; } }
        //
        public string ServItemMostrar { get { return _data.Get_Descripcion; } }
        public string AliadoItemMostrar { get { return _data.Get_Aliado_ItemMostrar ; } }
        public decimal PrecioItemMostrar { get { return _data.Get_PrecioDivisa; } }
        public int CantDiasItemMostrar { get { return _data.Get_CntDias; } }
        public int CantVehicItemMostrar { get { return _data.Get_CntUnidades; } }
        public decimal ImporteItemMostrar { get { return _data.Get_Importe; } }
        public string TurnoMostrar { get { return _data.Get_TipoTurno != null ? _data.Get_TipoTurno.desc : ""; } }
        public string TurnoDescMostrar { get { return _data.Get_Descripcion; } }
        public decimal AliadoMontoMostrar { get { return _data.Get_ImporteAliadosLLamados; } }


        public ImpItem()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data = new data();
            _aliado = new Aliado.ImpAliado();
            _alicuota = new LibUtilitis.CtrlCB.ImpCB();
            _tipoServ = new LibUtilitis.CtrlCB.ImpCB();
            _tipoTurno = new LibUtilitis.CtrlCB.ImpCB();
            _tasasFiscal = null;
        }


        public void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data.Inicializa();
            _aliado.Inicializa();
            _alicuota.Inicializa();
            _tipoServ.Inicializa();
            _tipoTurno.Inicializa();
            _tasasFiscal = null;
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public void Aliados()
        {
            _aliado.Inicializa();
            _aliado.Inicia();
            if (_aliado.ItemSeleccionadoIsOk) 
            {
                var id = _aliado.AliadoSeleccionado_GetId;
                try
                {
                    var r01 = Sistema.MyData.TransporteAliado_GetById(id);
                    Item.setAliado(r01.Entidad);
                }
                catch (Exception e)
                {
                    Helpers.Msg.Error(e.Message);
                }
            }
        }


        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        abstract public void Procesar();

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }


        private bool CargarData()
        {
            try
            {
                var filtroOOB = new OOB.Transporte.ServPrest.Busqueda.Filtro();
                var r01 = Sistema.MyData.TransporteServPrest_GetLista(filtroOOB);
                var lst = r01.ListaD.Select(s =>
                {
                    var nr = new tipoServicio() { id = s.id.ToString(), codigo = "", desc = s.descripcion };
                    return nr;
                }).ToList();
                _tipoServ.CargarData(lst.OrderBy(o => o.desc).ToList());
                //
                var _lstTurno = new List<tipoTurno>();
                _lstTurno.Add(new tipoTurno() { id = "1", codigo = "", desc = "Turno 1" });
                _lstTurno.Add(new tipoTurno() { id = "2", codigo = "", desc = "Turno 2" });
                _lstTurno.Add(new tipoTurno() { id = "3", codigo = "", desc = "Turno 3" });
                _lstTurno.Add(new tipoTurno() { id = "4", codigo = "", desc = "Turno Normal (Administrativo)" });
                _tipoTurno.CargarData(_lstTurno);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }

        private int _id=-1;
        public int id { get { return _id; } }
        public void setId(int id)
        {
            _id = id;
        }

        public void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> list)
        {
            _tasasFiscal = list;
            var lst = list.Select(s =>
            {
                var nr = new alicuota() { id = s.id, codigo = "", desc = s.ToString(), tasa = s.tasa };
                return nr;
            }).ToList();
            _alicuota.CargarData(lst.OrderBy(o => o.desc).ToList());
            AlicuotaSetFichaById("0000000004");
        }
        public void AlicuotaSetFichaById(string id)
        {
            _alicuota.setFichaById(id);
            _data.setAlicuota((alicuota)_alicuota.GetItem);
        }
        public void TipoServSetFichaById(string id)
        {
            _tipoServ.setFichaById(id);
            if (id.Trim() == "")
            {
                _data.setTipoServicio(null);
                _data.setDescripcion("");
                return;
            }

            try
            {
                var _id= int.Parse(id);
                var r01 = Sistema.MyData.TransporteServPrest_GetById(_id);
                _data.setTipoServicio(r01.Entidad);
                _data.setDescripcion(r01.Entidad.detalle);
            }
            catch (Exception e)
            {
                Helpers.Msg.Alerta(e.Message);
            }
        }

        //TURNO
        private LibUtilitis.CtrlCB.ICtrl _tipoTurno;
        public LibUtilitis.CtrlCB.ICtrl TipoTurno { get { return _tipoTurno; } }
        public void TipoTurnoSetFichaById(string id)
        {
            _tipoTurno.setFichaById(id);
            _data.setTipoTurno(_tipoTurno.GetItem);
        }
    }
}