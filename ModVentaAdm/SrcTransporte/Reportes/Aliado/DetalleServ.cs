using Microsoft.Reporting.WinForms;
using ModVentaAdm.Helpers.Imprimir.Grafico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Aliado
{
    public class DetalleServ: Src.IReporte
    {
        public DetalleServ()
        {
        }

        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteReporte_AliadoDetalleServ ();
                imprimir(r01.ListaD);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }

        private void imprimir(List<OOB.Transporte.Reporte.AliadoDetalleServ> lst)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\AliadoDetalleServ.rdlc";
            var ds = new DS_TRANSP();

            foreach (var it in lst.OrderBy(o => o.aliadoNombre).ToList())
            {
                DataRow rt = ds.Tables["AliadoDetalleServ"].NewRow();
                rt["aliado"] = it.aliadoCiRif + Environment.NewLine + it.aliadoNombre;
                rt["servcio"] = it.servDesc;
                rt["cliente"] = it.clienteCiRif+Environment.NewLine+ it.clienteNombre;
                rt["docNumero"] = it.numDoc;
                rt["docFecha"] = it.fechaDoc;
                rt["docNombre"] = it.nombreDoc;
                rt["importe"] = it.importeServ;
                rt["notas"] = it.servDesc;
                ds.Tables["AliadoDetalleServ"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("AliadoDetalleServ", ds.Tables["AliadoDetalleServ"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}