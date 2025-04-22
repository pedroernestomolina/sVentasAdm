using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.DocumentosPend
{

    public class DocPend : IDocPend
    {
        private bool _abandonarIsOK;
        private string _idCliente;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;
        private ListaDocPend.ILista _gListaDoc;
        private Cliente.Visualizar.IVisualizar _gVistaCliente;
        private Reportes.ListaDocPend.IRepDocPend _gRepDocPend;
        //
        public BindingSource DocPendGetSource { get { return _gListaDoc.DocPendGetSource; } }
        public ListaDocPend.data ItemActual { get { return _gListaDoc.ItemActual; } }
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public decimal GetMontoImporte { get { return _gListaDoc.MontoImporte; } }
        public decimal GetMontoAcumulado { get { return _gListaDoc.MontoAcumulado; } }
        public decimal GetMontoResta { get { return _gListaDoc.MontoPendientePorCobrar; } }
        public int GetCantDoc { get { return _gListaDoc.CntItems; } }
        public string GetNotas { get { return ItemActual != null ? ItemActual.notasDoc : ""; } }
        public string GetClienteData
        {
            get
            {
                var rt = "";
                if (_cliente != null)
                {
                    rt += _cliente.ciRif + Environment.NewLine;
                    rt += _cliente.razonSocial + Environment.NewLine;
                    rt += _cliente.dirFiscal;
                }
                return rt;
            }
        }
        public DocPend()
        {
            _abandonarIsOK = false;
            _idCliente = "";
            _cliente = null;
            _gListaDoc = new ListaDocPend.Lista();
            _gVistaCliente = new Cliente.Visualizar.Gestion();
            _gRepDocPend = new Reportes.ListaDocPend.RepDocPend();
        }
        public void Inicializa()
        {
            _abandonarIsOK = false;
            _idCliente = "";
            _cliente = null;
            _gListaDoc.Inicializa();
        }
        DocumentosPendFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new DocumentosPendFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }
        public void setIdCliente(string id)
        {
            _idCliente = id;
        }
        public void VerFichaCliente()
        {
            if (_cliente != null)
            {
                _gVistaCliente.Inicializa();
                _gVistaCliente.setFicha(_cliente);
                _gVistaCliente.Inicia();
            }
        }
        public void ReporteDocPend()
        {
            _gRepDocPend.setListaDoc(_gListaDoc.ListaItems.Where(w=>w.signoDoc==1).ToList());
            _gRepDocPend.setMontoCredito(_gListaDoc.ListaItems.Where(w => w.signoDoc == -1).Sum(s=>s.montoResta));
            ((Reportes.ListaDocPend.RepDocPend)_gRepDocPend).setCliente(_cliente);
            _gRepDocPend.Generar();
        }
        public void VisualizarDocumento()
        {
            if (ItemActual != null)
            {
                Sistema.Fabrica.VisualizarDocumento(ItemActual.autoDocVenta);
            }
        }
        public void AnularDocumento()
        {
            if (ItemActual != null)
            {
                if (Helpers.Msg.ProcesarGuardar("ESTAS SEGURO DE ANULAR ESTE DOCUMENTO ?"))
                {
                    try
                    {
                        if (!ItemActual.IsDocGeneradoPorModuloCxc)
                        {
                            throw new Exception("DOCUMENTO NO PUEDE SER ANULADO POR ESTE MODULO");
                        }
                        if (ItemActual.acumuladoDoc > 0m)
                        {
                            throw new Exception("DOCUMENTO POSEE UN ABONO");
                        }
                        var ficha = new OOB.CxC.Anular.CxcAdm.Ficha()
                        {
                            idCxcAdm = ItemActual.autoDoc,
                        };
                        var rt = Sistema.MyData.CxC_AnularCxcAdm(ficha);
                        _gListaDoc.setAnularItemActual();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Alerta(e.Message);
                    }
                }
            }
        }
        //
        private bool CargarData()
        {
            try
            {
                var filtroOOb = new OOB.CxC.DocumentosPend.Filtro()
                {
                    idCliente = _idCliente,
                };
                var r00 = Sistema.MyData.Cliente_GetFicha(_idCliente);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r00.Mensaje);
                }
                _cliente = r00.Entidad;
                var r01 = Sistema.MyData.CxC_DocumentosPend_GetLista(filtroOOb);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var lst = r01.ListaD.Select(s =>
                {
                    var nr = new ListaDocPend.data()
                    {
                        acumuladoDoc = s.acumuladoDoc,
                        autoDoc = s.autoDoc,
                        diasCreditoDoc = s.diasCreditoDoc,
                        fechaEmisionDoc = s.fechaEmisionDoc,
                        fechaVencDoc = s.fechaVencDoc,
                        importeDoc = s.importeDoc,
                        notasDoc = s.notasDoc,
                        numeroDoc = s.numeroDoc,
                        serieDoc = s.serieDoc,
                        signoDoc = s.signoDoc,
                        tasaCambioDoc = s.tasaCambioDoc,
                        tipoDoc = s.tipoDoc + (s.IsDocGeneradoPorModCxC?"/ADM":""),
                        autoDocVenta = s.autoDocVenta,
                        IsDocGeneradoPorModuloCxc = s.IsDocGeneradoPorModCxC,
                    };
                    return nr;
                }).ToList();
                _gListaDoc.setListaDocPend(lst.OrderBy(o => o.fechaEmisionDoc).ToList());
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}