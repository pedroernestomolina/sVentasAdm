using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Fabrica.General
{
    public partial class ModoGeneral: IFabrica
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
            if (!GetItemActual.IsDocVentaAdministrativo)
            {
                Helpers.Msg.Error("DOCUMENTO NO ES DE TIPO VENTA ADMINISTRATIVA, VERIFIQUE POR FAVOR");
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
            var ficha = new OOB.Documento.Anular.Presupuesto.Ficha()
            {
                autoDocumento = doc.idDocumento,
                auditoria = new OOB.Documento.Anular.Presupuesto.FichaAuditoria
                {
                    autoSistemaDocumento = Sistema.Id_SistDocumento_Presupuesto,
                    autoUsuario = Sistema.Usuario.id,
                    codigo = Sistema.Usuario.codigo,
                    estacion = Sistema.EquipoEstacion,
                    motivo = motivo,
                    usuario = Sistema.Usuario.nombre,
                },
            };
            var r01 = Sistema.MyData.Documento_Anular_Presupuesto(ficha);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                throw new Exception(r01.Mensaje);
            }
            return true;
        }
        private Helpers.Imprimir.IDocumento _gVisualizarDoc;
        public void VisualizarDocumento(Src.Administrador.data doc)
        {
            if (doc != null)
            {
                var r01 = Helpers.Imprimir.Documento.CargarDataDocumento(doc.idDocumento);
                if (r01 != null)
                {
                    if (_gVisualizarDoc == null) 
                    {
                        _gVisualizarDoc = new Helpers.Imprimir.Grafico.Documento();
                    }
                    _gVisualizarDoc.setData(r01);
                    _gVisualizarDoc.ImprimirDoc();
                }
            }
        }
        public OOB.Resultado.Lista<OOB.Documento.Lista.Ficha> DocumentosGetLista(OOB.Documento.Lista.Filtro filtro)
        {
            return Sistema.MyData.Documento_Get_Lista(filtro);
        }
    }
}