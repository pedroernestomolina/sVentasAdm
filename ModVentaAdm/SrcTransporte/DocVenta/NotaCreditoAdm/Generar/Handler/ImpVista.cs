using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler
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
                var _imp= data.MontoFisal_1.Get_Iva+ data.MontoFisal_2.Get_Iva+data.MontoFisal_3.Get_Iva;
                var _neto=data.MontoFisal_1.Get_Base+ data.MontoFisal_2.Get_Base+data.MontoFisal_3.Get_Base+ data.Exento.Get_Base;
                var _montoDiv= data.MontoTotal / data.DocAplicarNtCredito.factorCambio;
                var _subt = _neto + _imp;
                var fichaOOB = new OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Ficha()
                {
                    estacion = Environment.MachineName,
                    PrefijoSuc = "",
                    serieDocDesc = r01.siglas,
                    serieDocId = r01.id,
                    Doc = new OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Documento()
                    {
                        CiRif = data.DocAplicarNtCredito.clienteCiRif,
                        cntRenglones = 0,
                        codCliente = data.DocAplicarNtCredito.clienteCodigo,
                        codSucursal = data.DocAplicarNtCredito.codigoSucursal,
                        codUsuario = Sistema.Usuario.codigo,
                        codVendedor = data.DocAplicarNtCredito.vendedorCodigo,
                        condPago = "CONTADO",
                        diasValidez = 0,
                        DirFiscal = data.DocAplicarNtCredito.clienteDirFiscal,
                        docCodigo = r01.codigo,
                        docNombre = r01.descripcion,
                        docRemision = data.DocAplicarNtCredito.docNumero,
                        docSiglas = r01.siglas,
                        factorCambio = data.DocAplicarNtCredito.factorCambio,
                        idCliente = data.DocAplicarNtCredito.clienteId,
                        idRemision = data.DocAplicarNtCredito.idDoc,
                        idUsuario = Sistema.Usuario.id,
                        idVendedor = data.DocAplicarNtCredito.vendedorId,
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
                        RazonSocial = data.DocAplicarNtCredito.clienteNombre,
                        signo = r01.signo,
                        subTotal = _neto + _imp,
                        subTotalImpuesto = _imp,
                        subTotalMonDivisa = _subt / data.DocAplicarNtCredito.factorCambio,
                        subTotalNeto = _neto,
                        Tasa1 = data.MontoFisal_1.Get_Tasa,
                        Tasa2 = data.MontoFisal_2.Get_Tasa,
                        Tasa3 = data.MontoFisal_3.Get_Tasa,
                        telefono = data.DocAplicarNtCredito.clienteTelefono,
                        TipoDoc = r01.tipo,
                        tipoRemision = data.DocAplicarNtCredito.docCodigoTipo,
                        Total = data.MontoTotal,
                        usuario = Sistema.Usuario.nombre,
                        vendedor = data.DocAplicarNtCredito.vendedorNombre
                    }
                };
                var r02 = Sistema.Fabrica.DataDocumentos.Agregar_Nuevo_Documento_NotaCredito(fichaOOB);
                var _autoDoc = r02;
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