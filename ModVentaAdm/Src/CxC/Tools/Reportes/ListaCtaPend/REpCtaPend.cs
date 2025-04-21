using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.Reportes.ListaCtaPend
{
    public class RepCtaPend: IRepCtaPend
    {
        private List<PanelPrincipal.ListaCtasPend.Idata> _lst;
        //
        public RepCtaPend()
        {
        }
        public void setListaDoc(List<PanelPrincipal.ListaCtasPend.Idata> lst)
        {
            _lst = lst;
        }
        public void Generar()
        {
            Imprimir();
        }
        //
        private void Imprimir()
        {
            if (_lst == null)
                return;
            //
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Src\CxC\Tools\Reportes\ListaCtaPend.rdlc";
            var ds = new DS_CxC();
            //
            foreach (var it in _lst.ToList())
            {
                var tt = (PanelPrincipal.ListaCtasPend.data)it;
                DataRow rt = ds.Tables["ListaCtaPend"].NewRow();
                rt["nombre"] = tt.ciRif +Environment.NewLine+tt.nombreRazonSocial;
                rt["importe"] = tt.montoImporte;
                rt["acumulado"] = tt.montoAcumulado;
                rt["resta"] = it.montoResta;
                rt["cntDocPend"] = tt.Ficha.cntDocPend;
                rt["cntFactPend"] = tt.cntFactPend;
                rt["montoLimiteCredito"] = tt.Ficha.limiteMontoCredito;
                rt["montoPorAnticipos"] = tt.Ficha.anticiposCliente;
                rt["montoPorCreditos"] = tt.Ficha.importePorNtCreditoPendPorSaldarMonDiv;
                ds.Tables["ListaCtaPend"].Rows.Add(rt);
            }
            //
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.DatosEmpresa.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.DatosEmpresa.Nombre));
            //pmt.Add(new ReportParameter("Filtros", _filtros));
            Rds.Add(new ReportDataSource("ListaCtaPend", ds.Tables["ListaCtaPend"]));
            //
            var frp = new ModVentaAdm.Src.Reportes.ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}