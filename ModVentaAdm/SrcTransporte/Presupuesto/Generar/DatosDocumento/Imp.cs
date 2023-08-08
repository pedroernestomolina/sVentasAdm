using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.DatosDocumento
{
    public class Imp: IDatosDoc,  Remision.IObservador, DocVenta.Generar.IItemObservador
    {
        private data _data;
        private bool _habilitarBusquedaCliente;
        private Utils.Buscar.IBuscar _cliente;


        public data Data { get { return _data; } }


        public Imp(Remision.IRemision _remision)
        {
            if (_remision != null)
            {
                _remision.AgregarObservador(this);
            }
            _abandonarIsOK = false;
            _procesarIsOK = false;
            _data = new data();
            _habilitarBusquedaCliente = true;
        }


        public void Inicializa()
        {
            _habilitarBusquedaCliente = true;
            _abandonarIsOK = false;
            _procesarIsOK = false;
            _data.Inicializa();
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

        private bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.FechaServidor();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _data.setFechaSistema(r01.Entidad);
                //
                var _lst = new List<ficha>();
                _lst.Add(new ficha() { id = "01", codigo = "", desc = "CONTADO" });
                _lst.Add(new ficha() { id = "02", codigo = "", desc = "CREDITO" });
                _data.CondicionPagoCargar(_lst);
                //
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool _procesarIsOK;
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = false; ;
            if (_data.DatosIsOK()) 
            {
                _procesarIsOK = true;
            }
        }


        public void BuscarCliente()
        {
            if (!_habilitarBusquedaCliente) 
            {
                Helpers.Msg.Alerta("OPCION NO ACTIVA");
                return; 
            }
            if (_cliente ==null)
            {
                _cliente = new Utils.Buscar.Cliente.Imp();
            }
            _cliente.Inicializa();
            _cliente.Inicia();
            if (_cliente.ItemSeleccionadoIsOk)
            {
                getCliente((string)_cliente.ItemSeleccionadoGetId);
            }
        }


        private void getCliente(string id)
        {
            try
            {
                var r01 = Sistema.MyData.Cliente_GetFicha(id);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _data.setCliente(r01.Entidad);
                _data.setDiasCredito(r01.Entidad.diasCredito);
                _data.CondicionPago.setFichaById("01");
                if (r01.Entidad.estatusCredito.Trim() == "1")
                {
                    _data.CondicionPago.setFichaById("02");
                }
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }

        public void NotificarRemisionDocPresupuesto(OOB.Transporte.Documento.Entidad.Presupuesto.Ficha ficha)
        {
            _data.setSolicitadoPor(ficha.encabezado.docSolicitadoPor);
            _data.setModuloCargar(ficha.encabezado.docModuloCargar);
            _habilitarBusquedaCliente = false;
        }
        public void setHabilitarBusquedaCliente(bool act)
        {
            _habilitarBusquedaCliente = act;
        }


        private DocVenta.Generar.dataItem _itemsVenta;
        public void setEscucha(DocVenta.Generar.dataItem items)
        {
            _itemsVenta = items;
            items.RegistrarObservador(this);
        }
        public void OnItemAgregado()
        {
            verificarActivarBusquedaCliente();
        }
        public void OnItemEliminado()
        {
            verificarActivarBusquedaCliente();
        }
        private void verificarActivarBusquedaCliente()
        {
            if (_itemsVenta.Cnt_Get > 0)
            {
                _habilitarBusquedaCliente = false;
            }
        }

        public void NotificarDocPresupuesto(OOB.Transporte.Documento.Entidad.Presupuesto.Ficha ficha)
        {
            try
            {
                var r01 = Sistema.MyData.Cliente_GetFicha(ficha.encabezado.clienteId);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _data.setCliente(r01.Entidad);
                var _lst = new List<ficha>();
                _lst.Add(new ficha() { id = "01", codigo = "", desc = "CONTADO" });
                _lst.Add(new ficha() { id = "02", codigo = "", desc = "CREDITO" });
                _data.CondicionPagoCargar(_lst);

                var _idCondPago = "01";
                if (ficha.encabezado.condPago == "CREDITO")
                {
                    _idCondPago = "02";
                }
                _data.CondicionPago.setFichaById(_idCondPago);
                _data.setDiasCredito(ficha.encabezado.diasCredito);
                _data.setFechaSistema(DateTime.Now.Date);
                _data.setSolicitadoPor(ficha.encabezado.docSolicitadoPor);
                _data.setModuloCargar(ficha.encabezado.docModuloCargar);
                _habilitarBusquedaCliente = false;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}