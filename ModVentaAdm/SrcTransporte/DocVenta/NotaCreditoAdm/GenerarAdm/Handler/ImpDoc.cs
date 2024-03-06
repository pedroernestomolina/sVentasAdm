using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Handler
{
    public class ImpDoc: Vista.IDoc
    {
        private string _cadenaBusq;
        private Vista.IDocGenerar _docGenerar;
        private OOB.Maestro.Cliente.Entidad.Ficha _entidad_AplicarNotaCredito;
        private string _entidadAplicaNotaCredito_DatosCliente;
        private DateTime _fechaServidor;
        //
        public string Get_CadenaBusq { get { return _cadenaBusq; } }
        public bool BusquedaIsOk { get { return _entidad_AplicarNotaCredito != null; } }
        public Vista.IDocGenerar DocGenerar { get { return _docGenerar; } }
        public Vista.IdataGuardar Get_DatosGuardar { get { return dataGuardar(); } }
        public string Get_EntidadAplicarNotaCredito_DatosCliente { get { return _entidadAplicaNotaCredito_DatosCliente; } }
        //
        public ImpDoc()
        {
            _cadenaBusq = "";
            _fechaServidor = DateTime.Now.Date;
            _entidad_AplicarNotaCredito = null;
            _entidadAplicaNotaCredito_DatosCliente = "";
            _docGenerar = new ImpDocGenerar();
        }
        public void setFechaServidor(DateTime fecha)
        {
            _fechaServidor = fecha;
        }
        public void setCadenaBuscar(string cadena)
        {
            _cadenaBusq = cadena;
        }
        public void Inicializa()
        {
            _cadenaBusq = "";
            _entidad_AplicarNotaCredito = null;
            _entidadAplicaNotaCredito_DatosCliente = "";
            _docGenerar.Inicializa();
        }
        public void BuscarCliente()
        {
            if (_cadenaBusq.Trim() == "") { return; }
            cargar_Cliente_AplicarNotaCredito(listar_Clientes());
            _cadenaBusq = "";
        }
        //
        private void cargar_Cliente_AplicarNotaCredito(string idCliente)
        {
            if (idCliente == "") { return; }
            try
            {
                _entidadAplicaNotaCredito_DatosCliente = "";
                var r01 = Sistema.Fabrica.DataCliente.ObtenerFicha_Cliente_PorId(idCliente);
                _entidad_AplicarNotaCredito = r01;
                _entidadAplicaNotaCredito_DatosCliente = r01.ciRif + Environment.NewLine +
                                                        r01.razonSocial + Environment.NewLine +
                                                        r01.dirFiscal;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        ListaClientes.Vista.IVista _listCli;
        private string listar_Clientes()
        {
            var idCliente= "";
            //
            if (_listCli == null)
            {
                _listCli = new ListaClientes.Handler.ImpVista();
            }
            _listCli.Inicializa();
            _listCli.setObjectoFiltrar(_cadenaBusq);
            _listCli.Inicia();
            if (_listCli.ItemSeleccionadoIsOk)
            {
                var it = (ListaClientes.Handler.data)_listCli.Get_ItemSeleccionado;
                idCliente = it.Get_Ficha.id;
            }
            //
            return idCliente;
        }
        public void Limpiar()
        {
            _cadenaBusq = "";
            _entidad_AplicarNotaCredito=null;
            _entidadAplicaNotaCredito_DatosCliente = "";
            _docGenerar.Limpiar();
        }
        private Vista.IdataGuardar dataGuardar()
        {
            Vista.IdataGuardar rt = new dataGuardar()
            {
                EntidadAplicarNtCredito = _entidad_AplicarNotaCredito,
                FechaEmision = _docGenerar.Get_FechaEmision,
                DocNumero = _docGenerar.Get_DocNumero,
                TasaCambio = _docGenerar.Get_TasaCambio,
                Motivo = _docGenerar.Get_Motivo,
                MontoBase = _docGenerar.Get_Subt_Base,
                MontoImp = _docGenerar.Get_Subt_Imp,
                MontoTotal = _docGenerar.Get_Total,
                Exento = _docGenerar.MontoExento,
                MontoFisal_1 = _docGenerar.MontoFiscal_1,
                MontoFisal_2 = _docGenerar.MontoFiscal_2,
                MontoFisal_3 = _docGenerar.MontoFiscal_3,
            };
            return rt;
        }
        public bool ValidarDataIsOk()
        {
            try
            {
                _docGenerar.ValidarDataIsOk();
                if (_docGenerar.Get_FechaEmision > _fechaServidor)
                {
                    throw new Exception("FECHA EMISION DOCUMENTO INCORRECTA");
                }
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Alerta(e.Message);
                return false;
            }
        }
    }
}