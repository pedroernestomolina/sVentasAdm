using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Handler
{
    public class Imp: Vistas.IHnd
    {
        private bool _procesarIsOK;
        private bool _abandonarIsOK;
        private string _idCliente;
        private Vistas.Idata _data;
        private Vistas.IHndCaja _caja;


        public Vistas.Idata data { get { return _data; } }
        public Vistas.IHndCaja caja { get { return _caja; } }


        public Imp()
        {
            _idCliente= "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data = new data();
            _caja = new HndCaja();
        }


        public void Inicializa()
        {
            _idCliente = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data.Inicializa();
            _caja.Inicializa();
        }
        Vistas.Frm frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                frm = new Vistas.Frm();
                frm.setControlador(this);
                frm.ShowDialog();
            }
        }

        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = false;
            if (_data.VerificarData()) 
            {
                var _monto = _data.Get_MontoAbonoMonAct;
                if ((caja.MontoCajaPago-_monto)==0m)
                {
                    if (Helpers.Msg.ProcesarGuardar())
                    {
                        GuardarFicha();
                    }
                }
                else 
                {
                    Helpers.Msg.Alerta("MONTO PAGO CAJA INCORRECTOS");
                }
            }
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
                var r00 = Sistema.MyData.Cliente_GetFicha(_idCliente);
                _data.setCliente(r00.Entidad);
                //
                var r01 = Sistema.MyData.FechaServidor();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _data.setFechaServidor(r01.Entidad);
                _data.setFechaAnticipo(r01.Entidad);
                //
                var r02 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r02.Mensaje);
                }
                _data.setTasaFactorCambio(r02.Entidad);
                //
                var _lst = new List<Vistas.IdataCaja>();
                var r03 = Sistema.MyData.Transporte_Caja_GetLista();
                foreach (var rg in r03.ListaD.OrderBy(o => o.descripcion).ToList())
                {
                    var nr = new dataCaja(rg);
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
        public void setClienteCargar(string id)
        {
            _idCliente = id;
        }
        public void ActualizarSaldoCaja()
        {
            _caja.setFactorCambio(_data.Get_TasaFactorCambio);
            _caja.setMontoPendDiv(_data.Get_MontoAbonoMonDiv);
            _caja.ActualizarSaldosPend();
        }


        private void GuardarFicha()
        {
            try
            {
                var fichaOOB = new OOB.Transporte.ClienteAnticipo.Agregar.Ficha();
                fichaOOB.mov = new OOB.Transporte.ClienteAnticipo.Agregar.Movimiento()
                {
                    aplicaRet = _data.Get_AplicaRet ? "1" : "0",
                    ciRifCliente = _data.Get_Cliente.ciRif,
                    idCliente = _data.Get_Cliente.id,
                    fechaEmision = _data.Get_FechaAnticipo,
                    montoAnticipoMonAct = _data.Get_MontoAnticipoMonAct,
                    montoAnticipoMonDiv = _data.Get_MontoAnticipoMonDiv,
                    montoRecibidoMonAct = _data.Get_MontoAbonoMonAct,
                    montoRecibidoMonDiv = _data.Get_MontoAbonoMonDiv,
                    nombreRazonSocialCliente = _data.Get_Cliente.razonSocial,
                    reciboNumero = "",
                    retencion =( _data.Get_MontoRetencion - _data.Get_MontoSustraendo),
                    totalRet = _data.Get_MontoRetencion,
                    motivo = _data.Get_Motivo,
                    sustraendoRet = _data.Get_MontoSustraendo,
                    tasaFactor = _data.Get_TasaFactorCambio,
                    tasaRet = _data.Get_TasaRetencion,
                };
                var _lstCaja = new List<OOB.Transporte.ClienteAnticipo.Agregar.Caja>();
                foreach (var rg in _caja.Get_Lista.Where(w => w.montoAbonar > 0).ToList())
                {
                    var cj = (dataCaja)rg;
                    var nr = new OOB.Transporte.ClienteAnticipo.Agregar.Caja()
                    {
                        idCaja = cj.Get_Ficha.id,
                        monto = cj.montoAbonar,
                        codCaja = cj.Get_Ficha.codigo,
                        descCaja = cj.Get_Ficha.descripcion,
                        cajaMov = new OOB.Transporte.ClienteAnticipo.Agregar.CajaMov()
                     {
                         descMov = _data.Get_Motivo,
                         factorCambio = _data.Get_TasaFactorCambio,
                         fechaMov = _data.Get_FechaAnticipo,
                         montoMovMonAct = cj.esDivisa ? cj.montoAbonar * _data.Get_TasaFactorCambio : cj.montoAbonar,
                         montoMovMonDiv = cj.esDivisa ? cj.montoAbonar : cj.montoAbonar / _data.Get_TasaFactorCambio,
                         movFueDivisa = cj.esDivisa,
                     }
                    };
                    _lstCaja.Add(nr);
                }
                fichaOOB.caja = _lstCaja;
                var r01 = Sistema.MyData.Transporte_Cliente_Anticipo_Agregar(fichaOOB);
                _procesarIsOK = true;
                Helpers.Msg.AgregarOk();
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }
    }
}