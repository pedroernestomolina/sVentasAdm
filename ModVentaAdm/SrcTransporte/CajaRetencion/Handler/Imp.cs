using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.CajaRetencion.Handler
{
    public class Imp: Vista.IHnd
    {
        private decimal _factorCambio;
        private decimal _montoProcesarMonDiv;
        private bool _procesarIsOK;
        private bool _abandonarIsOK;
        private Utils.Componente.AplicaRetencionIslr.Vista.IHnd _retencion;
        private Utils.Componente.CajasUtilizar.Vista.IHnd _caja;


        public decimal Get_FactorCambio { get { return _factorCambio; } }
        public Utils.Componente.AplicaRetencionIslr.Vista.IHnd Retencion { get { return _retencion; } }
        public Utils.Componente.CajasUtilizar.Vista.IHnd Caja { get { return _caja; } }


        public Imp()
        {
            _factorCambio = 0m;
            _montoProcesarMonDiv = 0m;
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _retencion = new Utils.Componente.AplicaRetencionIslr.Handler.Imp();
            _caja = new Utils.Componente.CajasUtilizar.Handler.Imp();

        }
        public void Inicializa()
        {
            _factorCambio = 0m;
            _montoProcesarMonDiv = 0m;
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _retencion.Inicializa();
            _caja.Inicializa();
        }
        Vista.Frm frm;
        public void Inicia()
        {
            if (cargarData())
            {
                frm = new Vista.Frm();
                frm.setControlador(this);
                frm.ShowDialog();
            }
        }
        public void setFactorCambio(decimal factor)
        {
            _factorCambio = factor;
            _retencion.setFactorCambio(factor);
            _retencion.setMontoAplicarRetencionMonAct(_montoProcesarMonDiv * _factorCambio);
            _caja.setFactorCambio(factor);
        }
        public void setMontoCajaProcesarMonDiv(decimal montoCaja)
        {
            _montoProcesarMonDiv = montoCaja;
        }
        public void ActualizarSaldoCaja()
        {
            var _monto = _montoProcesarMonDiv;
            _monto = Math.Round(_monto, 2, MidpointRounding.AwayFromZero);
            _caja.setFactorCambio(_factorCambio);
            _caja.setMontoPendDiv(_monto);
            _caja.ActualizarSaldosPend();
        }


        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = verificarData();
        }

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _factorCambio = r01.Entidad;
                _retencion.setMontoAplicarRetencionMonAct(_montoProcesarMonDiv * _factorCambio);
                //
                var _lst = new List<Utils.Componente.CajasUtilizar.Vista.Idata>();
                var r02 = Sistema.MyData.Transporte_Caja_GetLista();
                foreach (var rg in r02.ListaD.OrderBy(o => o.descripcion).ToList())
                {
                    var nr = new Utils.Componente.CajasUtilizar.Handler.data(rg);
                    _lst.Add(nr);
                }
                _caja.setDataCargar(_lst);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private bool verificarData()
        {
            if (!_retencion.IsOk())
                return false;
            if (!_caja.IsOk())
                return false;
            return true;
        }
    }
}