using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item
{
    public class data
    {
        private string _desc;
        private int _cnt;
        private decimal _precioDivisa;
        private decimal _tasaIva;
        private decimal _importe;
        private decimal _precioAliadoPautado;
        private decimal _dscto;
        private decimal _precioDscto;
        private decimal _dsctoMontoDivisa; 
        private Presupuesto.Generar.alicuota _alicuota;
        private Presupuesto.Generar.Item.IItem _itemServicio;
        private Utils.DocLista.Remision.data _itemPresupuesto;
        private Utils.DocLista.Remision.data _itemHojaServ;


        public string Get_Descripcion { get { return _desc; } }
        public int Get_Cnt { get { return _cnt; } }
        public decimal Get_PrecioDivisa { get { return _precioDivisa; } }
        public decimal Get_PrecioItemDivisa { get { return _precioDscto; } }
        public decimal Get_Dscto { get { return _dscto; } }
        public decimal Get_DsctoMontoDivisa { get { return _dsctoMontoDivisa; } }
        public decimal Get_Importe { get { return _importe; } }
        public Presupuesto.Generar.Item.IItem Get_ItemServicio { get { return _itemServicio; } }
        public bool IsItemPresupuesto { get { return _itemPresupuesto != null; } }
        public bool IsItemHojaServ { get { return _itemHojaServ != null; } }
        public string Get_PresupuestoNumero 
        {
            get 
            {
                var rt = "";
                if (_itemPresupuesto != null)
                {
                    rt = _itemPresupuesto.DocNumero;
                }
                if (_itemHojaServ != null)
                {
                    rt = _itemHojaServ.DocNumero;
                }
                return rt;
                //return _itemPresupuesto == null ? "" : _itemPresupuesto.DocNumero; 
            }
        }


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


        public data()
        {
            limpiar();
        }

        public void Inicializa()
        {
            limpiar();
        }


        public void setDescripcion(string desc)
        {
            _desc = desc;
        }
        public void setCnt(int cnt)
        {
            _cnt = cnt;
            CalculaImporte();
        }
        public void setPrecioDivisa(decimal mnto)
        {
            _precioDivisa = mnto;
            _precioDscto = mnto;
            CalculaImporte();
        }
        public void setTasaIva(decimal tasa)
        {
            _tasaIva  = tasa;
        }
        public void setDscto(decimal tasa)
        {
            _dscto = tasa;
            CalculaImporte();
        }

        private void CalcularDscto()
        {
            _dsctoMontoDivisa = _precioDivisa * _dscto / 100;
            _precioDscto = _precioDivisa - _dsctoMontoDivisa;
        }
        private void CalculaImporte()
        {
            _dsctoMontoDivisa = _precioDivisa * _dscto / 100;
            _precioDscto = _precioDivisa - _dsctoMontoDivisa;
            _importe = (_cnt * _precioDscto);
        }
        private void limpiar()
        {
            _alicuota = null;
            _desc = "";
            _cnt = 0;
            _precioDivisa = 0m;
            _precioDscto = 0m;
            _tasaIva = 0m;
            _dscto = 0m;
            _importe = 0m;
            _itemServicio=null;
            _dsctoMontoDivisa = 0m;
            _itemPresupuesto = null;
            _itemHojaServ = null;
        }

        public bool VerificarDatosIsOK()
        {
            if (_desc.Trim() == "") 
            {
                Helpers.Msg.Alerta("Campo [ DESCRIPCION BREVE ] No puede estar vacio !!!");
                return false;
            }
            if (_cnt == 0)
            {
                Helpers.Msg.Alerta("Campo [ CANTIDAD ] No puede estar vacio !!!");
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
        public Presupuesto.Generar.alicuota Get_Alicuota { get { return _alicuota; } }
        public void setAlicuota(Presupuesto.Generar.alicuota ficha)
        {
            _alicuota = ficha;
            setTasaIva(0m);
            if (ficha != null) 
            {
                setTasaIva(ficha.tasa);
            }
        }

        public void setItemServicio(Presupuesto.Generar.Item.IItem itemServ)
        {
            _itemServicio = itemServ;
        }

        public Utils.DocLista.Remision.data Get_ItemPresupuesto { get { return _itemPresupuesto; } }
        public void setItemPresupuesto(Utils.DocLista.Remision.data doc)
        {
            _itemPresupuesto = doc;
        }
        public Utils.DocLista.Remision.data Get_ItemHojaServ { get { return _itemHojaServ; } }
        public void setItemHojaServicio(Utils.DocLista.Remision.data doc)
        {
            _itemHojaServ = doc;
        }
    }
}