using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.Reportes.ListaDocPend
{
    
    public class RepDocPend: IRepDocPend
    {


        private List<DocumentosPend.ListaDocPend.data> _lst;


        public RepDocPend()
        {
        }


        public void setListaDoc(List<DocumentosPend.ListaDocPend.data> lst)
        {
            _lst = lst;
        }

        public void Generar()
        {
            Imprimir();
        }

        private void Imprimir()
        {
            if (_lst == null)
                return;

            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Src\CxC\Tools\Reportes\ListaDocPend.rdlc";
            var ds = new DS_CxC();

            foreach (var it in _lst.ToList())
            {
                DataRow rt = ds.Tables["ListaDocPend"].NewRow();
                rt["documento"] = it.numeroDoc;
                rt["tipo"] = it.tipoDoc;
                //rt["acumulado"] = it.montoAcumulado;
                //rt["resta"] = it.montoResta;
                //rt["cntDocPend"] = it.cntDocPend;
                //rt["cntFactPend"] = it.cntFactPend;
                //rt["montoLimiteCredito"] = it.montoLimiteCredito;
                ds.Tables["ListaDocPend"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.DatosEmpresa.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.DatosEmpresa.Nombre));
            //pmt.Add(new ReportParameter("Filtros", _filtros));
            Rds.Add(new ReportDataSource("ListaDocPend", ds.Tables["ListaDocPend"]));

            var frp = new ModVentaAdm.Src.Reportes.ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}