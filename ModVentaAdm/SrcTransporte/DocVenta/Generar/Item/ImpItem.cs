using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item
{
    abstract public class ImpItem : IItem
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        protected data _data;
        private LibUtilitis.CtrlCB.ICtrl _alicuota;
        protected List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal;


        public data Item { get { return _data; } }
        public LibUtilitis.CtrlCB.ICtrl Alicuota { get { return _alicuota; } }
        //
        public string ServItemMostrar { get { return _data.Get_Descripcion; } }
        public decimal PrecioItemMostrar { get { return _data.Get_PrecioDivisa; } }
        public int CantItemMostrar { get { return _data.Get_Cnt; } }
        public decimal ImporteItemMostrar { get { return _data.Get_Importe; } }
        public string PresupuestoMostrar { get { return _data.Get_PresupuestoNumero; } }


        public ImpItem()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data = new data();
            _alicuota = new LibUtilitis.CtrlCB.ImpCB();
            _tasasFiscal = null;
        }


        public void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data.Inicializa();
            _alicuota.Inicializa();
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
                var nr = new  Presupuesto.Generar.alicuota() { id = s.id, codigo = "", desc = s.ToString(), tasa = s.tasa };
                return nr;
            }).ToList();
            _alicuota.CargarData(lst.OrderBy(o => o.desc).ToList());
        }
        public void AlicuotaSetFichaById(string id)
        {
            _alicuota.setFichaById(id);
            _data.setAlicuota((Presupuesto.Generar.alicuota)_alicuota.GetItem);
        }


        abstract public void HabilitarPresupuesto();
        public void HabilitarServicio()
        {
            AgregarServicio();
        }



        private Presupuesto.Generar.Item.Agregar.IAgregar _itemAgregar;
        private void AgregarServicio()
        {
            _itemAgregar = new Presupuesto.Generar.Item.Agregar.Agregar();
            _itemAgregar.Inicializa();
            _itemAgregar.setTasaFiscal(_tasasFiscal);
            _itemAgregar.setValidarDatosCompletos(true);
            _itemAgregar.Inicia();
            if (_itemAgregar.ProcesarIsOK) 
            {
                var _desc = _itemAgregar.Item.Get_Descripcion;
                var _precio = _itemAgregar.Item.Get_Importe;
                _data.setDescripcion(_desc);
                _data.setCnt(1);
                _data.setDscto(0m);
                _data.setPrecioDivisa(_precio);
                _data.setItemServicio(_itemAgregar);
            }
        }
    }
}