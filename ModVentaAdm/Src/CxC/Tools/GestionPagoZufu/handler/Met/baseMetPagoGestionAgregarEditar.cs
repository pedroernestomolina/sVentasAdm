using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public abstract class baseMetPagoGestionAgregarEditar: PanelPrincipal.Pago.IMetodoPagoGestion
    {
        private bool _procesarIsOk;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonarFicha;
        private Utils.Control.Boton.Procesar.IProcesar _procesarFicha;
        private PanelPrincipal.Pago.IItemMetPagoAgregar _item;
        private Utils.FiltrosCB.ICtrlSinBusqueda _listMetPago;
        //
        public bool AbandonarIsOK { get { return _abandonarFicha.OpcionIsOK; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        //
        abstract public string GetTituloFicha { get; }
        //
        public object GetMetCobroSource { get { return _listMetPago.GetSource; } }
        public string GetMetCobroID { get { return _listMetPago.GetId; } }
        public decimal GetMonto { get { return _item.GetMonto; } }
        public decimal GetFactor { get { return _item.GetFactorCambio; } }
        public string GetBanco { get { return _item.GetBanco; } }
        public string GetNroCta { get { return _item.GetNroCta; } }
        public string GetCheqRefTrans { get { return _item.GetCheqRefTranf; } }
        public DateTime GetFechaOp { get { return _item.GetFechaOp; } }
        public string GetDetalleOp { get { return _item.GetDetalleOp; } }
        public bool GetAplicaFactor { get { return _item.GetAplicaFactor; } }
        public string GetReferencia { get { return _item.GetReferencia; } }
        public string GetLote { get { return _item.GetLote; } }
        //
        public baseMetPagoGestionAgregarEditar()
        {
            _procesarIsOk = false;
            _abandonarFicha = new Utils.Control.Boton.Abandonar.Imp();
            _procesarFicha = new Utils.Control.Boton.Procesar.Imp();
            _listMetPago = new Utils.FiltrosCB.SinBusqueda.MetodosPago.Imp();
            _item = new itemMetPagoAgregar();
        }
        public virtual void Inicializa()
        {
            _procesarIsOk = false;
            _procesarFicha.Inicializa();
            _abandonarFicha.Inicializa();
            _listMetPago.Inicializa();
            _item.Inicializa();
        }
        abstract public void Inicia();
        //
        public void setMetCobro(string id)
        {
            _listMetPago.setFichaById(id);
            _item.setMetCobro(_listMetPago.GetItem);
        }
        public void setMonto(decimal monto)
        {
            _item.setMonto(monto);
        }
        public void setFactor(decimal factor)
        {
            _item.setFactor(factor);
        }
        public void setBanco(string banco)
        {
            _item.setBanco(banco);
        }
        public void setCtaNro(string cta)
        {
            _item.setCtaNro(cta);
        }
        public void setChequeRefTranf(string cheqRefTranf)
        {
            _item.setChequeRefTranf(cheqRefTranf);
        }
        public void setFechaOperacion(DateTime fecha)
        {
            _item.setFechaOperacion(fecha);
        }
        public void setDetalleOperacion(string detalleOp)
        {
            _item.setDetalleOperacion(detalleOp);
        }
        public void setAplicaFactor(bool p)
        {
            _item.setAplicaFactor(p);
        }
        public void setLote(string lote)
        {
            _item.setLote(lote);
        }
        public void setReferencia(string referenc)
        {
            _item.setReferencia(referenc);
        }
        public void AbandonarFicha()
        {
            _abandonarFicha.Opcion();
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_item.IsValido())
            {
                _procesarFicha.Opcion();
                _procesarIsOk = _procesarFicha.OpcionIsOK;
            }
        }
        //
        public virtual bool CargarDta()
        {
            try
            {
                _listMetPago.ObtenerData();
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        public virtual PanelPrincipal.Pago.IItemMetPago nuevoItem()
        {
            return new itemMetPago()
            {
                AplicaFactor = _item.GetAplicaFactor,
                Banco = _item.GetBanco,
                CheqRefTranf = _item.GetCheqRefTranf,
                DetalleOp = _item.GetDetalleOp,
                FactorCambio = _item.GetFactorCambio,
                FechaOp = _item.GetFechaOp,
                ImporteMonAct = _item.ImporteMonAct,
                ImporteMonDiv = _item.ImporteMonDiv,
                Lote = _item.GetLote,
                MetCobro = _item.GetMetCobro,
                Monto = _item.GetMonto,
                NroCta = _item.GetNroCta,
                Referencia = _item.GetReferencia,
                DescMetCobro = _item.GetMetCobro.desc,
            };
        }
    }
}