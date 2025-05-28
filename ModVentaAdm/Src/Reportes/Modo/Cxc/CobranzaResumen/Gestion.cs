using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.Cxc.CobranzaResumen
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
            var filtro = new OOB.ReportesCxc.Resumen.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal = data.GetCodigoSucursal,
                idCliente =data.ClienteId,
            };
            var r01 = Sistema.MyData.ReportesCxc_ResumenCobranza(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            Imprimir(r01.ListaD);
        }
        private void Imprimir(List<OOB.ReportesCxc.Resumen.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\CxcCobranzaResumen.rdlc";
            var ds = new DS();
            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["CxcCobranzaResumen"].NewRow();
                rt["FechaHora"] = it.fechaRec.ToShortDateString();
                rt["Documento"] = it.numeroRec;
                rt["Cliente"] = it.nombreEntidad+Environment.NewLine+it.ciRifEntidad;
                rt["montoRecibido"] = it.importeDivisa;
                rt["montoDocPagado"] = (it.importeDivisa - it.montoPorAnticipoCargado) < 0m ? 0m : (it.importeDivisa - it.montoPorAnticipoCargado);
                rt["anticiposCargado"] = it.montoPorAnticipoCargado;
                rt["anticiposUsado"] = it.montoPorAnticipoUsado;
                rt["ntCreditoUsado"] = it.montoPorNtCreditoUsado;
                ds.Tables["CxcCobranzaResumen"].Rows.Add(rt);
            }
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("CxcCobranzaResumen", ds.Tables["CxcCobranzaResumen"]));
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}