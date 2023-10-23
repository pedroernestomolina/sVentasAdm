using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal
{
    public class Tools : ITools
    {
        private ListaCtasPend.ILista _gListaCtasPend;
        private AgregarCta.IAgregar _gAgregar;
        private Reportes.ListaCtaPend.IRepCtaPend _gRepCtaPend;
        private DocumentosPend.IDocPend _gDocPend;
        private AgregarNotaAdm.IAgregar _gAgregarNotaAdm;
        private AgregarNotaAdm.IAgregarTipoNotaAdm _gAgregarNotaCreditoAdm;
        private AgregarNotaAdm.IAgregarTipoNotaAdm _gAgregarNotaDebitoAdm;
        private GestionPago.IGestionPago _gGestionPago;


        public decimal GetMontoPendientePorCobrar { get { return _gListaCtasPend.MontoPendientePorCobrar; } }
        public BindingSource CtasPendGetSource { get { return _gListaCtasPend.CtasPendGetSource; } }


        public Tools()
        {
            _gListaCtasPend = new ListaCtasPend.Lista();
            _gAgregar = new AgregarCta.Agregar();
            _gRepCtaPend = new Reportes.ListaCtaPend.RepCtaPend();
            _gDocPend = new DocumentosPend.DocPend();
            _gAgregarNotaAdm = new AgregarNotaAdm.Agregar();
            _gAgregarNotaCreditoAdm = new AgregarNotaAdm.Credito.AgregarNotaCreditoAdm();
            _gAgregarNotaDebitoAdm = new AgregarNotaAdm.Debito.AgregarNotaDebitoAdm();
            _gGestionPago = new GestionPago.GestionPago();
        }

        ToolsFrm frm;
        public void Inicializa()
        {
            _abandonarIsOk = false;
            _gListaCtasPend.Inicializa();
        }
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new ToolsFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }


        private bool _abandonarIsOk;
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }
        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var xmsg = "Abandonar / Cerrar Ficha ?";
            var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }

        public void BuscarCtasPendientes()
        {
            var filtroOOb = new OOB.CxC.Tools.CtasPendiente.Lista.Filtro()
            {
                codSucursal = Sistema.Sucursal.codigo,
            };
            var r01 = Sistema.MyData.CxC_Tool_CtasPendiente_GetLista(filtroOOb);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            if (r01.ListaD.Count == 0)
            {
                Helpers.Msg.Alerta("NO HAY CUENTAS PENDIENTES");
                return;
            }
            var lst = new List<ListaCtasPend.data>();
            foreach (var rg in r01.ListaD.OrderBy(o => o.nombreRazonSocial).ToList())
            {
                var nr = new ListaCtasPend.data()
                {
                    ciRif = rg.ciRif,
                    cntDocPend = rg.cntDocPend,
                    cntFactPend = rg.cntFactPend,
                    idCliente = rg.idCliente,
                    limiteFactPend = rg.limiteFactPend,
                    montoAcumulado = rg.acumulado,
                    montoImporte = rg.importe,
                    montoLimiteCredito = rg.limiteMontoCredito,
                    nombreRazonSocial = rg.nombreRazonSocial,
                };
                lst.Add(nr);
            }
            _gListaCtasPend.setListaCtasPend(lst);
        }


        public bool AgregarCtaIsOk { get { return _gAgregar.AgregarIsOk; } }
        public void AgregarCta()
        {
            var r00 = Sistema.MyData.Permiso_CxC_Tools_AgregarDoc(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gAgregar.Inicializa();
                _gAgregar.Inicia();
                if (_gAgregar.AgregarIsOk)
                {
                    BuscarCtasPendientes();
                }
            }
        }

        public void ListadoCtasPend()
        {
            var r00 = Sistema.MyData.Permiso_CxC_Tools_ReporteCtasPend(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gRepCtaPend.setListaDoc(_gListaCtasPend.ListaItems);
                _gRepCtaPend.Generar();
            }
        }


        public void DocDetallesPend()
        {
            if (_gListaCtasPend.ItemActual != null)
            {
                var r00 = Sistema.MyData.Permiso_CxC_Tools_VisualizarDocPend(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }
                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    var _item = _gListaCtasPend.ItemActual;
                    _gDocPend.Inicializa();
                    _gDocPend.setIdCliente(_item.idCliente);
                    _gDocPend.Inicia();
                }
            }
        }

        public bool AgregarNCrAdmIsOk { get { return _gAgregarNotaAdm.AgregarIsOk; } }
        public void AgregarNCrAdm()
        {
            var r00 = Sistema.MyData.Permiso_CxC_Tools_AgregarDocAdm_NCR(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gAgregarNotaCreditoAdm.Inicializa();
                _gAgregarNotaAdm.Inicializa();
                _gAgregarNotaAdm.setTipoNota(_gAgregarNotaCreditoAdm);
                _gAgregarNotaAdm.Inicia();
                if (_gAgregarNotaAdm.AgregarIsOk)
                {
                    BuscarCtasPendientes();
                }
            }
        }

        public bool AgregarNDbAdmIsOk { get { return _gAgregarNotaAdm.AgregarIsOk; } }
        public void AgregarNDbAdm()
        {
            var r00 = Sistema.MyData.Permiso_CxC_Tools_AgregarDocAdm_NDB(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gAgregarNotaDebitoAdm.Inicializa();
                _gAgregarNotaAdm.Inicializa();
                _gAgregarNotaAdm.setTipoNota(_gAgregarNotaDebitoAdm);
                _gAgregarNotaAdm.Inicia();
                if (_gAgregarNotaAdm.AgregarIsOk)
                {
                    BuscarCtasPendientes();
                }
            }
        }

        public void GestionPago()
        {
            if (_gListaCtasPend.ItemActual != null)
            {
                var r00 = Sistema.MyData.Permiso_CxC_Tools_GestionCobro(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }
                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    var _item = _gListaCtasPend.ItemActual;
                    _gGestionPago.Inicializa();
                    _gGestionPago.setIdCliente(_item.idCliente);
                    _gGestionPago.Inicia();
                }
            }
        }

        
        //
        //
        private SrcTransporte.ClienteAnticipo.Agregar.Vistas.IHnd _anticipo;
        public void AgregarAnticipo()
        {
            if (_gListaCtasPend.ItemActual != null)
            {
                if (_anticipo == null)
                {
                    _anticipo = new SrcTransporte.ClienteAnticipo.Agregar.Handler.Imp();
                }
                var item = _gListaCtasPend.ItemActual;
                _anticipo.Inicializa();
                _anticipo.setClienteCargar(item.idCliente);
                _anticipo.Inicia();
            }
        }
        //
        //
        private SrcTransporte.ClienteAnticipo.Administrador.Vistas.IAdm _admAnticipo;
        public void AdmDocAnticipos()
        {
            if (_admAnticipo == null) 
            {
                _admAnticipo = new SrcTransporte.ClienteAnticipo.Administrador.Handler.Imp();
            }
            _admAnticipo.Inicializa();
            _admAnticipo.Inicia();
        }
    }
}