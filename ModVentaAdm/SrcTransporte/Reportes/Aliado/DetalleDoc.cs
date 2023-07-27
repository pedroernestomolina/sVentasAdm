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
    public class DetalleDoc: Src.IReporte
    {
        public DetalleDoc()
        {
        }

        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteReporte_AliadoDetalleDoc();
                imprimir(r01.ListaD);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }

        private void imprimir(List<OOB.Transporte.Reporte.AliadoDetalleDoc> lst)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\AliadoDetalleDoc.rdlc";
            var ds = new DS_TRANSP();

            foreach (var it in lst.OrderBy(o => o.nombreAliado).ToList())
            {
                DataRow rt = ds.Tables["AliadoDetalleDoc"].NewRow();
                rt["aliado"] = it.rifAliado+ Environment.NewLine + it.nombreAliado;
                rt["cliente"] = it.rifCliente+ Environment.NewLine + it.nombreCliente;
                rt["docNumero"] = it.numDoc;
                rt["docFecha"] = it.fechaDoc;
                rt["docNombre"] = it.nombreDoc;
                rt["importe"] = it.importe ;
                rt["acumulado"] = it.acumulado;
                rt["saldo"] = it.importe-it.acumulado;
                ds.Tables["AliadoDetalleDoc"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("AliadoDetalleDoc", ds.Tables["AliadoDetalleDoc"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}