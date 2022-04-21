using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Filtro
{
    
    public class data
    {

        private general _sucursal;
        private general _cliente;
        private general _producto;
        private general _estatus;
        private general _tipoDoc;
        private DateTime _desde;
        private DateTime _hasta;
        private int _mesRelacion;
        private int _anoRelacion;
        private bool _tipoDocFactura;
        private bool _tipoDocNtDebito;
        private bool _tipoDocNtCredito;
        private bool _tipoDocNtEntrega;
        private bool _validarTipoDocumento;
        private string _palabraClave;


        public string PalabraClave { get { return _palabraClave; } }
        public general Cliente { get { return _cliente; } }
        public general Producto { get { return _producto; } }
        public DateTime GetDesde { get { return _desde; } }
        public DateTime GetHasta { get { return _hasta; } }
        public bool GetTipoDocFactura { get { return _tipoDocFactura; } }
        public bool GetTipoDocNtDebito { get { return _tipoDocNtDebito; } }
        public bool GetTipoDocNtCredito { get { return _tipoDocNtCredito; } }
        public bool GetTipoDocNtEntrega { get { return _tipoDocNtEntrega; } }
        public int GetMesRelacion { get { return _mesRelacion; } }
        public int GetAnoRelacion { get { return _anoRelacion; } }
        public string ClienteNombre 
        {
            get 
            {
                var rt = "";
                if (_cliente != null) { rt = _cliente.descripcion; }
                return rt;
            }
        }
        public string ClienteId
        {
            get
            {
                var rt = "";
                if (_cliente != null) { rt = _cliente.auto; }
                return rt;
            }
        }
        public string GetIdSucursal
        {
            get
            {
                var rt = "";
                if (_sucursal != null) { rt = _sucursal.auto; }
                return rt;
            }
        }
        public string GetCodigoSucursal
        {
            get
            {
                var rt = "";
                if (_sucursal != null) { rt = _sucursal.codigo; }
                return rt;
            }
        }
        public string GetNombreProducto 
        {
            get 
            {
                var rt = "";
                if (_producto!= null) { rt = _producto.descripcion; }
                return rt;
            }
        }
        public string GetIdProducto
        {
            get
            {
                var rt = "";
                if (_producto != null) { rt = _producto.auto; }
                return rt;
            }
        }
        public string GetCodigoTipoDoc
        {
            get
            {
                var rt = "";
                if (_tipoDoc != null) { rt = _tipoDoc.codigo; }
                return rt;
            }
        }
        public string GetIdTipoDoc 
        {
            get
            {
                var rt = "";
                if (_tipoDoc != null) { rt = _tipoDoc.auto; }
                return rt;
            }
        }
        public string GetEstatus 
        {
            get
            {
                var rt = "";
                if (_estatus != null) { rt = _estatus.descripcion; }
                return rt;
            }
        }
        public string  GetIdEstatus 
        {
            get
            {
                var rt = "";
                if (_estatus != null) { rt = _estatus.auto; }
                return rt;
            }
        }


        public data()
        {
            limpiar();
        }


        private void limpiar()
        {
            _sucursal = null;
            _estatus = null;
            _cliente = null;
            _producto = null;
            _tipoDoc = null;
            _desde = DateTime.Now.Date;
            _hasta = DateTime.Now.Date;
            _mesRelacion = DateTime.Now.Date.Month;
            _anoRelacion = DateTime.Now.Date.Year;
            _tipoDocFactura = false;
            _tipoDocNtDebito = false;
            _tipoDocNtCredito = false;
            _tipoDocNtEntrega = false;
            _validarTipoDocumento = false;
            _palabraClave = "";
        }

        public void setSucursal(general ficha)
        {
            _sucursal = ficha;
        }

        public void setEstatus(general ficha)
        {
            _estatus = ficha;
        }

        public void setFechaDesde(DateTime desde)
        {
            _desde = desde.Date;
        }

        public void setFechaHasta(DateTime hasta)
        {
            _hasta = hasta.Date;
        }

        public void Inicializa()
        {
            limpiar();
        }

        public void setMesRelacion(int p)
        {
            _mesRelacion = p;
        }

        public void setAnoRelacion(int p)
        {
            _anoRelacion = p;
        }

        public bool IsOk()
        {
            var rt = true;

            if (_desde > _hasta)
            {
                Helpers.Msg.Error("PROBLEMAS CON FECHAS INCORRECTAS");
                return false;
            }
            if (_validarTipoDocumento) 
            {
                if (_tipoDocFactura == null && _tipoDocNtDebito == null && _tipoDocNtCredito == null && _tipoDocNtEntrega == null)
                {
                    Helpers.Msg.Error("PROBLEMAS CON TIPO DOCUMENTO");
                    return false;
                }
            }

            return rt;
        }

        public void setTipoDocFactura(bool p)
        {
            _tipoDocFactura = p;
        }

        public void setTipoDocNtDebito(bool p)
        {
            _tipoDocNtDebito= p;
        }

        public void setTipoDocNtCredito(bool p)
        {
            _tipoDocNtCredito= p;
        }

        public void setTipoDocNtEntrega(bool p)
        {
            _tipoDocNtEntrega= p;
        }

        public void setValidarTipoDocumento(bool p)
        {
            _validarTipoDocumento = p;
        }

        public void setCliente(string id, string desc)
        {
            if (_cliente ==null)
                _cliente=new general(id,desc); 
            else
            {
                _cliente.limpiar();
            }
            _cliente.setficha(id, desc);
        }

        public void LimpiarCliente()
        {
            _cliente = null;
        }

        public void LimpiarProducto()
        {
            _producto = null;
        }

        public void setProducto(string id, string desc)
        {
            if (_producto== null)
                _producto= new general(id, desc);
            else
            {
                _producto.limpiar();
            }
            _producto.setficha(id, desc);
        }

        public string GetFiltros()
        {
            var xt = "Filtrado Por: ";
            xt += "Desde: " + _desde.ToShortDateString();
            xt += ", Hasta: " + _hasta.ToShortDateString();
            if (_tipoDoc != null)
                xt += ", Tipo Documento: " + _tipoDoc.descripcion.Trim();
            if (_cliente!=null)
                xt += ", Cliente: " + _cliente.descripcion.Trim();
            if (_producto != null)
                xt += ", Producto: " + _producto.descripcion.Trim();
            if (_sucursal != null)
                xt += ", Sucursal: " + _sucursal.descripcion.Trim();
            if (_estatus != null)
                xt += ", Estatus: " + _estatus.descripcion.Trim();

            return xt;
        }

        public void setTipoDoc(general ficha)
        {
            _tipoDoc = ficha;
        }

        public void setPalabraClave(string p)
        {
            _palabraClave = p;
        }

    }

}