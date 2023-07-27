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
    public class Resumen: Src.IReporte
    {
        public Resumen ()
        {
        }


        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteReporte_AliadoResumen();
                imprimir(r01.ListaD);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }

        private void imprimir(List<OOB.Transporte.Reporte.AliadoResumen> lst)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\AliadoResumen.rdlc";
            var ds = new DS_TRANSP();

            foreach (var it in lst.OrderBy(o=>o.aliado).ToList())
            {
                DataRow rt = ds.Tables["AliadoResumen"].NewRow();
                rt["aliado"] = it.ciRif + Environment.NewLine + it.aliado;
                rt["montoDebito"] = it.montoDebitoMonDivisa - it.montoDebitoAnuladoMonDivisa;
                rt["montoCredito"] = it.montoCreditoMonDivisa - it.montoCreditoAnuladoMonDivisa;
                rt["montoAnticipo"] = it.montoAnticiposMonDivisa - it.montoAnticiposAnuladoMonDivisa;
                ds.Tables["AliadoResumen"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("AliadoResumen", ds.Tables["AliadoResumen"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}