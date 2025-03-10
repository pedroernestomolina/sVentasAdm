using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public abstract class baseHndCtas: PanelPrincipal.Pago.ICtasPend
    {
        private string _idCliente;
        private DateTime _fechaServ;
        private PanelPrincipal.Pago.IListaCtaPend _listaCtasPend;
        //
        public object GetIdEntidad { get { return _idCliente; } }
        public PanelPrincipal.Pago.IListaCtaPend ListaCtas { get { return _listaCtasPend; } }
        //
        public object ItemActual { get { return _listaCtasPend.ItemActual; } }
        public decimal GetMontoPagar { get { return _listaCtasPend.GetTotalMontoAbon; } }
        public int GetCntCtasPagar { get { return _listaCtasPend.GetCntDocAbon; } }
        //
        public Object DataSource { get { return _listaCtasPend.Source; } }
        public string GetTotalMontoPend { get { return decToStr(_listaCtasPend.GetTotalMontoPend); } }
        public string GetCntDocPend { get { return intToStr(_listaCtasPend.GetCntDocPend); } }
        public string GetCntDocAbon { get { return intToStr(_listaCtasPend.GetCntDocAbon); } }
        public string GetTotalMontoAbon { get { return decToStr(_listaCtasPend.GetTotalMontoAbon); } }
        public string GetNotas { get { return notas(); } }
        //
        public baseHndCtas(PanelPrincipal.Pago.IListaCtaPend listaCtasPend)
        {
            _idCliente = "";
            _fechaServ = DateTime.Now.Date;
            _listaCtasPend = listaCtasPend;
        }
        public void Inicializa()
        {
            _idCliente = "";
            _fechaServ = DateTime.Now.Date;
            _listaCtasPend.Inicializa();
        }
        PanelPrincipal.Pago.vistas.vCtasPend frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vCtasPend();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public bool _cargarData;
        public abstract bool CargarData();
        private PanelPrincipal.Pago.IMontoAbonar _montAbonar;
        public void AbonarCta()
        {
            if (ItemActual != null) 
            {
                var it = (PanelPrincipal.Pago.IItemCtaPend)ItemActual;
                if (_montAbonar == null) 
                {
                    _montAbonar = new MontoAbonar();
                }
                _montAbonar.Inicializa();
                _montAbonar.setMontoPend(it.montoResta);
                _montAbonar.setDetalle(it.detalleAbono);
                if (it.montoAbonar>0)
                    _montAbonar.setMontoAbonar(it.montoAbonar);
                else
                    _montAbonar.setMontoAbonar(it.montoResta);
                _montAbonar.Inicia();
                if (_montAbonar.ProcesarIsOK) 
                {
                   _listaCtasPend.ItemActualSetMontoAbonar(_montAbonar.MontoAbonado);
                   _listaCtasPend.ItemActualSetDetalleAbono(_montAbonar.GetDetalle);
                }
            }
        }
        public void LimpiarAbonos()
        {
            var rt = Helpers.Msg.ProcesarGuardar("Estas Seguro de Eliminar/Limpiar Abono ?");
            if (rt) 
            {
                _listaCtasPend.LimpiarAbonos();
            }
        }
        public void setIdEntidad(object id)
        {
            _cargarData = true;
            if (((string)id == _idCliente))
            {
                _cargarData = false;
                return;
            }
            _listaCtasPend.Inicializa();
            _idCliente = (string)id;
        }
        public void setFechaServidor(DateTime fecha)
        {
            _fechaServ = fecha;
        }
        private string intToStr(int p)
        {
            return p.ToString();
        }
        private string decToStr(decimal p)
        {
            return p.ToString("n2");
        }
        private string notas()
        {
            if (ItemActual == null) return "";
            return ((PanelPrincipal.Pago.IItemCtaPend)ItemActual).detalleAbono;
        }
    }
}