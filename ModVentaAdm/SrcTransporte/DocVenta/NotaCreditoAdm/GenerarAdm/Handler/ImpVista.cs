using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Handler
{
    public class ImpVista: SrcComun.Documento.NotaCreditoAdm.Generar.Handler.baseVista, 
                            Vista.IVista 
    {
        private Vista.IDoc _doc;
        private bool _docProcesarIsOk;
        //
        public Vista.IDoc Doc { get { return _doc; } }
        public bool ProcesarDocIsOk { get { return _docProcesarIsOk; } }
        //
        public ImpVista()
            :base()
        {
            _doc = new ImpDoc();
            _docProcesarIsOk = false;
        }
        public override void Inicializa()
        {
            base.Inicializa();
            _doc.Inicializa();
            _docProcesarIsOk = false;
        }
        Vista.Frm frm;
        public override void Inicia()
        {
            if (cargarDataIsOk())
            {
                if (frm == null)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void LimpiarDoc()
        {
            _doc.Limpiar();
        }
        public void ProcesarDoc()
        {
            _docProcesarIsOk = false;
            if (_doc.BusquedaIsOk)
            {
                if (_doc.ValidarDataIsOk()) 
                {
                    BtProcesar.Opcion();
                    if (BtProcesar.OpcionIsOK) 
                    {
                        guardarDocumento(_doc.Get_DatosGuardar);
                    }
                }
            }
            else 
            {
                Helpers.Msg.Alerta("DEBES SELECCIONAR UN DOCUMENTO EL CUAL APLICAR LA NOTA DE CREDITO");
            }
        }
        //
        private bool cargarDataIsOk()
        {
            try
            {
                var r01 = Sistema.MyData.FechaServidor();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    throw new Exception(r01.Mensaje);
                }
                _doc.setFechaServidor(r01.Entidad);
                var r02 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r02.Mensaje);
                }
                _doc.DocGenerar.setFactorCambio(r02.Entidad);
                var r03 = Sistema.MyData.Sistema_TasaFiscal_GetLista ();
                if (r03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r03.Mensaje);
                }
                _doc.DocGenerar.MontoExento.setTasa(0m);
                _doc.DocGenerar.MontoFiscal_1.setTasa(r03.ListaD[0].tasa);
                _doc.DocGenerar.MontoFiscal_2.setTasa(r03.ListaD[1].tasa);
                _doc.DocGenerar.MontoFiscal_3.setTasa(r03.ListaD[2].tasa);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void guardarDocumento(Vista.IdataGuardar data)
        {
            try
            {
                var r01 =  Sistema.Fabrica.DataDocumentos.ObtenerFicha_TipoDocumento_Venta(Sistema.Id_SistDocumento_NotaCredito);
                var r02 = Sistema.Fabrica.DataDocumentos.ObtenerFicha_TipoDocumento_Venta(Sistema.Id_SistDocumento_Factura);
                var r03 = Sistema.MyData.Sistema_GetCodigoSucursal();
                if (r03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r03 .Mensaje);
                }
                var _codSucursal = r03.Entidad;
                var _imp = data.MontoFisal_1.Get_Iva + data.MontoFisal_2.Get_Iva + data.MontoFisal_3.Get_Iva;
                var _neto=data.MontoFisal_1.Get_Base+ data.MontoFisal_2.Get_Base+data.MontoFisal_3.Get_Base+ data.Exento.Get_Base;
                var _montoDiv = data.MontoTotal / data.TasaCambio;
                var _subt = _neto + _imp;
                var _entAplicarNtCredito = (OOB.Maestro.Cliente.Entidad.Ficha)data.EntidadAplicarNtCredito;
                var fichaOOB = new OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Ficha()
                {
                    estacion = Environment.MachineName,
                    PrefijoSuc = "",
                    serieDocDesc = r01.siglas,
                    serieDocId = r01.id,
                    Doc = new OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Documento()
                    {
                        CiRif = _entAplicarNtCredito.ciRif,
                        cntRenglones = 0,
                        codCliente = _entAplicarNtCredito.codigo,
                        codSucursal = _codSucursal,
                        codUsuario = Sistema.Usuario.codigo,
                        codVendedor = _entAplicarNtCredito.vendedorCodigo,
                        condPago = "CONTADO",
                        diasValidez = 0,
                        DirFiscal = _entAplicarNtCredito.dirFiscal,
                        docCodigo = r01.codigo,
                        docNombre = r01.descripcion,
                        docRemision = data.DocNumero,
                        docSiglas = r01.siglas,
                        factorCambio = data.TasaCambio,
                        idCliente = _entAplicarNtCredito.id,
                        idRemision = "",
                        idUsuario = Sistema.Usuario.id,
                        idVendedor = _entAplicarNtCredito.idVendedor,
                        montoBase = data.MontoBase,
                        montoBase1 = data.MontoFisal_1.Get_Base,
                        montoBase2 = data.MontoFisal_2.Get_Base,
                        montoBase3 = data.MontoFisal_3.Get_Base,
                        montoDivisa = _montoDiv,
                        montoExento = data.Exento.Get_Base,
                        montoImpuesto = data.MontoImp,
                        montoImpuesto1 = data.MontoFisal_1.Get_Iva,
                        montoImpuesto2 = data.MontoFisal_2.Get_Iva,
                        montoImpuesto3 = data.MontoFisal_3.Get_Iva,
                        neto = _neto,
                        nota = data.Motivo,
                        RazonSocial = _entAplicarNtCredito.razonSocial,
                        signo = r01.signo,
                        subTotal = _neto + _imp,
                        subTotalImpuesto = _imp,
                        subTotalMonDivisa = _subt / data.TasaCambio,
                        subTotalNeto = _neto,
                        Tasa1 = data.MontoFisal_1.Get_Tasa,
                        Tasa2 = data.MontoFisal_2.Get_Tasa,
                        Tasa3 = data.MontoFisal_3.Get_Tasa,
                        telefono = _entAplicarNtCredito.telefono1,
                        TipoDoc = r01.tipo,
                        tipoRemision = r02.codigo,
                        Total = data.MontoTotal,
                        usuario = Sistema.Usuario.nombre,
                        vendedor = _entAplicarNtCredito.vendedor,
                        fechaEmision = data.FechaEmision,
                    }
                };
                var r04 = Sistema.Fabrica.DataDocumentos.Agregar_Nuevo_Documento_NotaCredito(fichaOOB);
                var _autoDoc = r04;
                _docProcesarIsOk = true;
                Helpers.Msg.AgregarOk();
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}