using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Fabrica.Transporte
{
    public partial class ModoTransporte: IFabrica
    {
        public bool AnularDocumentoVenta(Src.Administrador.data GetItemActual, Src.Anular.Gestion _gAnular)
        {
            if (GetItemActual == null)
            {
                return false;
            }
            if (GetItemActual.IsAnulado)
            {
                Helpers.Msg.Error("DOCUMENTO YA SE ENCUENTRA ANULADO, VERIFIQUE POR FAVOR");
                return false;
            }
            try
            {
                var r00 = Sistema.MyData.Permiso_Adm_AnularDocumento(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r00.Mensaje);
                }
                if (!ModVentaAdm.Src.Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    return false;
                }
                _gAnular.Inicializa();
                _gAnular.Inicia();
                if (!_gAnular.ProcesarIsOK)
                {
                    return false;
                }

                var msg = MessageBox.Show("Estas Seguro De Anular Este Documento ?", "** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (msg == DialogResult.No)
                {
                    return false;
                }

                var rt = false;
                var motivo = _gAnular.Motivo;
                switch (GetItemActual.DocTipo)
                {
                    case Src.Administrador.data.enumTipoDoc.Presupuesto:
                        rt = AnularPresupuesto(GetItemActual, motivo);
                        break;
                    case Src.Administrador.data.enumTipoDoc.NotaEntrega:
                    case Src.Administrador.data.enumTipoDoc.Factura:
                        rt = AnularVenta(GetItemActual, motivo);
                        break;
                }
                return rt;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private bool AnularPresupuesto(Src.Administrador.data doc , string motivo)
        {
            var ficha = new OOB.Transporte.Documento.Anular.Presupuesto.Ficha()
            {
                idDoc = doc.idDocumento,
                auditoria = new OOB.Transporte.Documento.Anular.Presupuesto.FichaAuditoria
                {
                    idSistemaDocumento = Sistema.Id_SistDocumento_Presupuesto,
                    idUsuario = Sistema.Usuario.id,
                    codigo = Sistema.Usuario.codigo,
                    estacion = Sistema.EquipoEstacion,
                    motivo = motivo,
                    usuario = Sistema.Usuario.nombre,
                },
            };
            var r01 = Sistema.MyData.TransporteDocumento_AnularPresupuesto(ficha);
            return true;
        }
        private bool AnularVenta(Src.Administrador.data doc, string motivo)
        {
            var _idSistDoc = "";
            switch (doc.DocCodigo) 
            {
                case "01":
                    _idSistDoc = Sistema.Id_SistDocumento_Factura;
                    break;
                case "04":
                    _idSistDoc = Sistema.Id_SistDocumento_NotaEntrega;
                    break;
            }
            var ficha = new OOB.Transporte.Documento.Anular.Venta.Ficha()
            {
                idDocVenta = doc.idDocumento,
                auditoria = new OOB.Transporte.Documento.Anular.Venta.FichaAuditoria
                {
                    idSistemaDocumento = _idSistDoc,
                    idUsuario = Sistema.Usuario.id,
                    codigo = Sistema.Usuario.codigo,
                    estacion = Sistema.EquipoEstacion,
                    motivo = motivo,
                    usuario = Sistema.Usuario.nombre,
                },
            };
            var r01 = Sistema.MyData.TransporteDocumento_AnularVenta(ficha);
            return true;
        }
        public void VisualizarDocumento(Src.Administrador.data doc)
        {
            if (doc == null)
            {
                return;
            }
            switch (doc.DocCodigo.Trim().ToUpper()) 
            {
                case "05":
                    CargarPresupuesto(doc.idDocumento);
                    break;
                case "01":
                case "04":
                    CargarVenta(doc.idDocumento);
                    break;
            }
        }


        private void CargarVenta(string idDoc)
        {
            try
            {
                var r01 = Sistema.MyData.TransporteDocumento_EntidadVenta_GetById(idDoc);
                Helpers.Msg.OK("VENTA");
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        private void CargarPresupuesto(string idDoc)
        {
            try
            {
                var r01 = Sistema.MyData.TransporteDocumento_EntidadPresupuesto_GetById(idDoc);
                Helpers.Msg.OK("PRESUPUESTO");
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}