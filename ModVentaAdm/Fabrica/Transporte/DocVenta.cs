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
                        rt = AnularNotaEntrega(GetItemActual, motivo);
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
        private bool AnularNotaEntrega(Src.Administrador.data doc, string motivo)
        {
            var ficha = new OOB.Transporte.Documento.Anular.NotaEntrega.Ficha()
            {
                idDocVenta = doc.idDocumento,
                auditoria = new OOB.Transporte.Documento.Anular.NotaEntrega.FichaAuditoria
                {
                    idSistemaDocumento = Sistema.Id_SistDocumento_NotaEntrega,
                    idUsuario = Sistema.Usuario.id,
                    codigo = Sistema.Usuario.codigo,
                    estacion = Sistema.EquipoEstacion,
                    motivo = motivo,
                    usuario = Sistema.Usuario.nombre,
                },
            };
            var r01 = Sistema.MyData.TransporteDocumento_AnularNotaEntrega(ficha);
            return true;
        }
    }
}