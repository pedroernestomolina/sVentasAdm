using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Documentos
{
    
    public class Gestion
    {


        private string _autoCli;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;
        private BindingSource _bs;
        private Filtro _filtro;
        private List<data> _ldata;
        private Helpers.Imprimir.IDocumento _gestionVisualizarDoc;
        private bool _habilitarSeleccionarDocumento;
        private bool _seleccionarDocumentoIsOk;
        private string _idDocumentoSeleccionado;
        private bool _habilitarVisualizarDocumento;


        public string Cliente { get { return _cliente.ciRif+Environment.NewLine+_cliente.razonSocial; } }
        public BindingSource Source { get { return _bs; } }
        public DateTime Desde { get { return _filtro.desde; } }
        public DateTime Hasta { get { return _filtro.hasta; } }
        public int ItemsCnt { get { return _ldata.Count; } }
        public bool SeleccionarDocumentoIsOk { get { return _seleccionarDocumentoIsOk; } }
        public string IdDocumentoSeleccionado { get { return _idDocumentoSeleccionado; } }


        public Gestion()
        {
            _autoCli = "";
            _filtro = new Filtro();
            _ldata= new List<data>();
            _bs = new BindingSource();
            _bs.DataSource = _ldata;
            _gestionVisualizarDoc = new Helpers.Imprimir.Grafico.Documento();
            _habilitarSeleccionarDocumento = false;
            _seleccionarDocumentoIsOk = false;
            _idDocumentoSeleccionado = "";
            _habilitarVisualizarDocumento = false;
        }


        public void Inicializa()
        {
            _autoCli = "";
            _filtro.Limpiar();
            _ldata.Clear();
            _habilitarSeleccionarDocumento = false;
            _seleccionarDocumentoIsOk = false;
            _idDocumentoSeleccionado = "";
        }

        public void setCliente(Administrador.data Item)
        {
            _autoCli = Item.Id;
        }

        public void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _cliente = ficha;
            _autoCli = "";
        }

        DocumentosFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new DocumentosFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;

            if (_autoCli != "")
            {
                var r01 = Sistema.MyData.Cliente_GetFicha(_autoCli);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return false;
                }
                _cliente = r01.Entidad;
            }
            _filtro.setCliente(_cliente.id);

            return rt;
        }

        public void setDesde(DateTime fecha)
        {
            _filtro.setDesde(fecha);
        }

        public void setHasta(DateTime fecha)
        {
            _filtro.setHasta(fecha);
        }

        public void Buscar()
        {
            if (_filtro.IsOk()) 
            {
                var filtroOOB = new OOB.Maestro.Cliente.Documento.Filtro()
                {
                    desde = _filtro.desde,
                    hasta = _filtro.hasta,
                    autoCliente = _filtro.autoCliente,
                };
                var r01 = Sistema.MyData.Cliente_Documentos_GetLista(filtroOOB);
                if (r01.Result ==  OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                setLista(r01.ListaD);
            }
        }

        public void Limpiar()
        {
            _filtro.Limpiar();
            _filtro.setCliente(_cliente.id);
            _ldata.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void Imprimir()
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"ReportesCliente\Documento.rdlc";
            var ds = new ReportesCliente.DS_CLI();

            foreach (var it in _ldata.ToList())
            {
                DataRow rt = ds.Tables["Documento"].NewRow();
                rt["fecha"] = it.Fecha.Date;
                rt["tipo"] = it.Tipo;
                rt["serie"] = it.Serie;
                rt["documento"] = it.Documento;
                rt["importeDivisa"] = it.ImporteDivisa;
                ds.Tables["Documento"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("Documento", ds.Tables["Documento"]));

            var frp = new Reportes.ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

        public void setLista(List<OOB.Maestro.Cliente.Documento.Ficha> list)
        {
            _ldata.Clear();
            foreach (var it in list.OrderByDescending(o=>o.fecha).ThenBy(o=>o.codTipoDoc).ThenByDescending(o=>o.documento).ToList())
            {
                var nr = new data(it);
                _ldata.Add(nr);
            }
            _bs.CurrencyManager.Refresh();
        }

        public void setHabilitarVisualizarDocumento(bool modo)
        {
            _habilitarVisualizarDocumento = modo;
        }

        public void VisualizarDocumento()
        {
            if (_habilitarVisualizarDocumento)
            {
                if (_bs.Current != null)
                {
                    var it = (data)_bs.Current;
                    var r01 = Helpers.Imprimir.Documento.CargarDataDocumento(it.id);
                    if (r01 != null)
                    {
                        _gestionVisualizarDoc.setData(r01);
                        _gestionVisualizarDoc.ImprimirDoc();
                    }
                }
            }
        }

        public void setHabilitarSeleccionarDocumento(bool modo) 
        {
            _habilitarSeleccionarDocumento = modo;
        }

        public void SeleccionarDocumento()
        {
            if (_habilitarSeleccionarDocumento) 
            {
                if (_bs.Current != null)
                {
                    var it = (data)_bs.Current;
                    _idDocumentoSeleccionado = it.id;
                    _seleccionarDocumentoIsOk = true;
                }
            }
        }

        public void setCliente(string _idCliente)
        {
            _autoCli = _idCliente;
        }

    }

}