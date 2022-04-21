using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.AgregarEditarItem
{
    
    public class data
    {

        private OOB.Producto.Entidad.Ficha _prd;
        private string _idPrecio;
        private decimal _cantidad;
        private decimal _pneto;
        private decimal _dscto;
        private string _detalle;
        private string _empaqueDesc; 
        private int _empaqueCont;
        private decimal _importe;
        private string _decimales;
        private decimal _tasaDivisa;
        private decimal _pItem;
        private string _idDeposito;
        private string _descDeposito;
        private bool _rupturaPorExistencia;


        public bool IsCantidad_IgualA_Cero { get { return _cantidad == 0m; } }
        public bool IsPrecio_IgualA_Cero { get { return _pneto == 0m; } }
        public bool IsImporte_Cero { get { return _importe == 0m; } }
        public bool IsPrecioPorDebajoDelCosto { get { return (_prd.CostoUnd * _empaqueCont) > _pItem; } }
        public OOB.Producto.Entidad.Ficha Producto { get { return _prd; } }
        public string Empaque { get { return _empaqueDesc.Trim()+"/"+_empaqueCont.ToString("n0"); } }
        public decimal TasaIva { get { return _prd.TasaImpuesto; } }
        public decimal PNeto { get { return _pneto; } }
        public decimal Importe { get { return _importe; } }
        public decimal GetCantidad { get { return _cantidad; } }
        public decimal GetCantidadUnd { get { return _cantidad * _empaqueCont; } }
        public decimal GetDsctoPorct { get { return _dscto; } }
        public string GetNotas { get { return _detalle; } }
        public decimal GetPrecioNeto { get { return _pneto; } }
        public string GetIdPrecio { get { return _idPrecio; } }
        public string GetEmpqDesc { get { return _empaqueDesc; } }
        public int GetEmpqCont { get { return _empaqueCont; } }
        public string GetDecimales { get { return _decimales; } }
        public decimal GetImporteFull { get { return CalculaFull(_importe); } }
        public decimal GetIva { get { return CalculaIva(_importe); } }
        public string GetIdDeposito { get { return _idDeposito; } }
        public string GetDescDeposito { get { return _descDeposito; } }
        public bool GetRupturaPorExistencia { get { return _rupturaPorExistencia; } }
        public decimal GetCostoUnd { get { return _prd.CostoUnd; } }
        public decimal GetCostoEmp { get { return _prd.CostoUnd*_empaqueCont; } }
        public decimal GetImporteDivisaFull 
        {
            get 
            {
                var xt = 0m;
                if (_tasaDivisa > 0m) 
                {
                    xt = GetImporteFull / _tasaDivisa;
                }
                return xt;
            } 
        }
        public decimal GetPrecioNetoDivisa 
        { 
            get 
            {
                var xt = 0m;
                if (_tasaDivisa > 0m) 
                {
                    xt = _pneto / _tasaDivisa; 
                }
                return xt; 
            } 
        }


        public data() 
        {
            Limpiar();
        }


        public void setProducto(OOB.Producto.Entidad.Ficha ficha)
        {
            _prd = ficha;
        }

        public void setPrecio(precio ficha)
        {
            _idPrecio= ficha.ID;
            _pneto = ficha.PNeto;
            _empaqueCont = ficha.EmpqCont;
            _empaqueDesc = ficha.EmpqDesc;
            _decimales = ficha.Decimales;
            Calcula();
        }

        public void setCantidad(decimal cnt)
        {
            _cantidad = cnt;
            Calcula();
        }

        private void Calcula()
        {
            _pItem = _pneto;
            var r1 = _pneto * _dscto / 100;
            _pItem -= r1;
            _importe= _cantidad * _pItem;
        }

        public void setDescuento(decimal dsct)
        {
            _dscto = dsct;
            Calcula();
        }

        public void setNotas(string p)
        {
            _detalle = p;
        }

        public void setTasaDivisa(decimal TasaDivisa)
        {
            _tasaDivisa = TasaDivisa;
        }

        private decimal CalculaFull(decimal monto)
        {
            var rt = monto;
            rt += (monto * TasaIva / 100);
            return rt;
        }

        private decimal CalculaIva(decimal monto)
        {
            var rt = (monto * TasaIva / 100);
            return rt;
        }

        public void Limpiar()
        {
            _prd = null;
            _idPrecio = "";
            _cantidad = 0m;
            _pneto = 0m;
            _dscto = 0m;
            _detalle = "";
            _importe = 0m;
            _empaqueCont = 0;
            _empaqueDesc = "";
            _decimales = "";
            _tasaDivisa = 0m;
            _pItem = 0m;
            _idDeposito = "";
            _descDeposito = "";
            _rupturaPorExistencia = false;
        }

        public void setIdDeposito(string id)
        {
            _idDeposito = id;
        }

        public void setRupturaPorExistencia(bool ruptura)
        {
            _rupturaPorExistencia = ruptura;
        }

        public void setPrecio(Items.data ficha)
        {
            _idPrecio = ficha.DataItem.tarifaPrecio;
            _pneto = ficha.PNeto;
            _empaqueCont = ficha.DataItem.empaqueCont;
            _empaqueDesc = ficha.DataItem.empaqueDesc;
            _decimales = ficha.DataItem.decimalesProducto;
            Calcula();
        }

        public void setPrecioNeto(decimal precio)
        {
            _pneto = precio;
            Calcula();
        }

        public void setNombreDeposito(string p)
        {
            _descDeposito = p;
        }

    }

}