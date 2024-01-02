using Microsoft.Reporting.WinForms;
using ModVentaAdm.Src.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Reportes.ListaAdm
{
    public class Imp: IRepAdm
    {
        private string _filtros;
        private List<Administrador.Vistas.IdataItem> _lst;


        public Imp()
        {
            _filtros = "";
            _lst = new List<Administrador.Vistas.IdataItem>();
        }
        public void setDataCargar(IEnumerable<object> lista)
        {
            _lst.Clear();
            foreach (Administrador.Handler.dataItem rg in lista) 
            {
                _lst.Add(rg);
            }
        }
        public void setFiltrosBusq(string filtros)
        {
            _filtros = filtros;
        }
        public void Generar()
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\ToolsCxC\Reportes\RepAdm_Cobro.rdlc";
            var ds = new DS_TOOLS_CXC();
            //
            foreach (var it in _lst)
            {
                var _importe = it.ImporteMov;
                var _anticipo= it.AnticipoMov;
                var _retencion = it.RetencionMov;
                var _montoRec = it.MontoRec;
                if (it.Estatus.Trim()!= "") 
                {
                    _importe = 0m;
                    _anticipo = 0m;
                    _retencion = 0m;
                    _montoRec = 0m;
                }
                DataRow rt = ds.Tables["ListaAdm"].NewRow();
                rt["recibo"] = it.NroRecibo; 
                rt["fecha"] = it.FechaMov; 
                rt["cliente"] = it.CiRif + Environment.NewLine + it.Nombre;
                rt["montoMov"] = _importe;
                rt["montoRec"] = _montoRec;
                rt["estatus"] = it.Estatus;
                rt["montoAnticipo"] = _anticipo;
                rt["montoRet"] = _retencion;
                ds.Tables["ListaAdm"].Rows.Add(rt);
            }
            //
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("ListaAdm", ds.Tables["ListaAdm"]));
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}