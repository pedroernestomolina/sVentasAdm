using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.ListaDocumento
{
    
    public class Gestion
    {


        private List<Administrador.data> _lst;
        private string _filtros;


        public Gestion()
        {
            _filtros = "";
        }


        public void setListaDoc(List<Administrador.data> lst)
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

            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\ListaDoc.rdlc";
            var ds = new DS();

            foreach (var it in _lst.ToList())
            {
                DataRow rt = ds.Tables["GeneralDocumento"].NewRow();
                rt["FechaHora"] = it.FechaHora;
                rt["Documento"] = it.Documento;
                rt["Cliente"] = it.ClienteCiRif.Trim() + Environment.NewLine + it.ClienteNombre.Trim();
                rt["monto"] = it.Importe * it.Signo;
                rt["montoDivisa"] = it.ImporteDivisa* it.Signo;
                rt["nombreDoc"] = it.DocNombre;
                rt["estatusDoc"] = it.Estatus;
                rt["docAplica"] = it.Aplica;
                ds.Tables["GeneralDocumento"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.DatosEmpresa.CiRif));
            pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.DatosEmpresa.Nombre));
            pmt.Add(new ReportParameter("Filtros", _filtros));
            Rds.Add(new ReportDataSource("GeneralDocumento", ds.Tables["GeneralDocumento"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

        public void setFiltros(string p)
        {
            _filtros = p;
        }

    }

}