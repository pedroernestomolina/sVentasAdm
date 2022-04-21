using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.Items
{

    public class data
    {

        private decimal _cant;
        private decimal _pneto;
        private decimal _pitem;
        private decimal _dscto;
        private decimal _importe;
        private decimal _tasaIva;
        private decimal _mDscto;
        private decimal _mIva;
        private decimal _tasaDivisa;
        private decimal _dsctoFinal;
        private decimal _cargoFinal;
        private decimal _pFinal;
        private OOB.Venta.Temporal.Item.Entidad.Ficha _ficha;


        public OOB.Venta.Temporal.Item.Entidad.Ficha DataItem { get { return _ficha; } }
        public string Notas { get { return _ficha.notas; } }
        public int Id { get { return _ficha.id; } }
        public string IdProducto { get { return _ficha.autoProducto; } }
        public bool MercanciaIsEnReserva { get { return _ficha.estatusReservaMerc == "1"; } }
        public string CodigoPrd { get { return _ficha.codigoProducto; } }
        public string DescripcionPrd { get { return _ficha.nombreProducto; } }
        public decimal Cant { get { return _ficha.cantidad; } }
        public decimal CantUnd { get { return _ficha.cantidad * _ficha.empaqueCont; } }
        public decimal PNeto { get { return _ficha.precioNeto; } }
        public decimal PItem{ get { return _pitem; } }
        public decimal Dscto { get { return _dscto; } }
        public decimal Importe { get { return _importe; } }
        public decimal Total { get { return Cant * PNeto; } }
        public decimal TasaIva { get { return _ficha.tasaIva; } }
        public decimal MIva { get { return _mIva; } }
        public string Empaque { get { return _ficha.empaqueDesc.Trim() + "/" + _ficha.empaqueCont.ToString(); } }
        public decimal mTotal { get { return _importe + MIva; } }
        public string idTasaIva { get { return _ficha.autoTasaIva; } }
        public decimal PrecioItem { get { return _pitem; } }
        public decimal PrecioFinal { get { return _pFinal; } }
        public decimal Utilidad { get { return (PrecioFinal - DataItem.costoUnd) * CantUnd; } }
        public decimal UtilidadP { get { return (1-(DataItem.costoUnd/ PrecioFinal)) * 100; } } 



        public string TasaIvaDesc 
        {
            get 
            {
                var rt = "Ex";
                if (_tasaIva > 0) rt = _tasaIva.ToString("n2") + "%";
                return rt;
            }
        }
        public decimal mTotalDivisa 
        {
            get
            {
                var rt = 0m;
                if (_tasaDivisa != 0m)
                {
                    rt = mTotal / _tasaDivisa;
                }
                return rt;
            }
        }


        public data(OOB.Venta.Temporal.Item.Entidad.Ficha ficha)
        {
            _tasaDivisa = 0m;
            this._ficha = ficha;
            //
            _pitem = 0m;
            _mDscto = 0m;
            _importe = 0m;
            _mIva = 0m;
            //
            _cant = ficha.cantidad;
            _pneto = ficha.precioNeto;
            _dscto = ficha.dsctoPorct;
            _tasaIva = ficha.tasaIva;
            //
            _pFinal = 0m;
            //
            Calcula();
        }


        private void Calcula()
        {
            _pitem = _pneto;
            var xdscto = _pitem * _dscto / 100;
            _pitem -= xdscto;
            _pitem = Math.Round(_pitem, 2, MidpointRounding.AwayFromZero);
            _pFinal = _pitem;

            var m = _pneto*_cant;
            _mDscto = m * _dscto / 100;
            m -= _mDscto;
            _importe = m;
            _importe = Math.Round(m, 2, MidpointRounding.AwayFromZero);

            _mIva = m * _tasaIva / 100;
            _mIva = Math.Round(_mIva, 2, MidpointRounding.AwayFromZero);
        }

        public void setTasaDivisa(decimal tasa)
        {
            _tasaDivisa = tasa;
        }


        public decimal MontoExento 
        { 
            get 
            {
                if (_tasaIva == 0m)
                {
                    var rt = Importe;
                    _mDscto = Importe * _dsctoFinal / 100;
                    rt -= _mDscto;
                    rt = Math.Round(rt, 2, MidpointRounding.AwayFromZero);
                    return rt;
                }
                else { return 0m; }
            }
        }
        public decimal MontoBase
        {
            get
            {
                if (_tasaIva > 0m)
                {
                    var rt = Importe;
                    _mDscto = Importe * _dsctoFinal / 100;
                    rt -= _mDscto;
                    rt = Math.Round(rt, 2, MidpointRounding.AwayFromZero);
                    return rt;
                }
                else { return 0m; }
            }
        }

        public decimal MontoImpuesto
        {
            get
            {
                var rt = 0m;
                rt = MontoBase * _tasaIva /100 ;
                rt= Math.Round(rt, 2, MidpointRounding.AwayFromZero);
                return rt;
            }
        }


        public decimal CostoVenta
        {
            get
            {
                var rt = (_ficha.costoUnd * CantUnd);
                return rt;
            }
        }

        public decimal NetoVenta
        {
            get
            {
                var rt = _pFinal * Cant;
                return rt;
            }
        }

        public decimal DsctoMonto 
        {
            get
            {
                var rt = (_pneto * _dscto)/100 ;
                return rt;
            }
        }

        public decimal DsctoMontoTotal
        {
            get
            {
                var rt = DsctoMonto* Cant;
                return rt;
            }
        }

    }

}