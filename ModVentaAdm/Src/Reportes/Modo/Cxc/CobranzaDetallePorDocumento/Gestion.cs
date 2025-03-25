using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.Cxc.CobranzaDetallePorDocumento
{
    public class Gestion : IGestion
    {
        private Reportes.Filtro.IFiltro _filtro;
        //
        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }
        //
        public Gestion()
        {
            _filtro = new Filtro();
        }
        public void Generar(Reportes.Filtro.data data)
        {
            var filtro = new OOB.ReportesCxc.DetallePorDoc.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal = data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.ReportesCxc_DetalleCobranza_PorDocumentos(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            Imprimir(r01.ListaD);
        }
        private void Imprimir(List<OOB.ReportesCxc.DetallePorDoc.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\CxcCobranzaDetPorDocumento.rdlc";
            var ds = new DS();
            foreach (var it in list.ToList())
            {
                var xref="";
                DataRow rt = ds.Tables["CxcCobranzaDetPorDocumento"].NewRow();
                rt["numeroRec"] = it.numeroRecibo;
                rt["fechaRec"] = it.fechaRecibo.ToShortDateString();
                rt["entidad"] = it.nombreEntidad+Environment.NewLine+it.ciRifEntidad;
                rt["importeRec"] = it.importeRecibo;
                rt["numeroDoc"] = it.numeroDoc;
                rt["descDoc"] = it.tipoDoc;
                rt["fechaDoc"] = it.fechaDoc.ToShortDateString();
                rt["importeDoc"] = it.importeDivisa;
                rt["notasDoc"] = it.notasDoc;
                if (it.montoPorAnticiposUsado>0)
                    xref+="ANT/U: "+it.montoPorAnticiposUsado.ToString("n2")+Environment.NewLine;
                if (it.montoPorNtCreditoUsado>0)
                    xref+="NCR/U: "+it.montoPorNtCreditoUsado.ToString("n2")+Environment.NewLine;
                if (it.montoPorAnticiposPorCargar>0)
                    xref+="ANT/C: "+it.montoPorAnticiposPorCargar.ToString("n2")+Environment.NewLine;
                var mn = it.importeRecibo-it.montoPorAnticiposUsado-it.montoPorNtCreditoUsado-it.montoPorAnticiposPorCargar;
                xref+="MD/PG: "+mn.ToString("n2");
                rt["xref"] = xref;
                ds.Tables["CxcCobranzaDetPorDocumento"].Rows.Add(rt);
            }
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("CxcCobranzaDetPorDocumento", ds.Tables["CxcCobranzaDetPorDocumento"]));
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}