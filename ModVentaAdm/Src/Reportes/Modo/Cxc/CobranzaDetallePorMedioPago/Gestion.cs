using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.Cxc.CobranzaDetallePorMedioPago
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
            var filtro = new OOB.ReportesCxc.DetallePorMedioPago.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal = data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.ReportesCxc_DetalleCobranza_PorMedioPago(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            Imprimir(r01.ListaD);
        }
        private void Imprimir(List<OOB.ReportesCxc.DetallePorMedioPago.Ficha> list)
        {
            /*
        public string numeroRec { get; set; }
        public DateTime fechaRec { get; set; }
        public string ciRifEntidad { get; set; }
        public string nombreEntidad { get; set; }
        public string notasRec { get; set; }
        public decimal importeDivisa { get; set; }
        public decimal montoPorAnticipoCargado { get; set; }
        public string codigoSuc { get; set; }
        public decimal montoPorAnticipoUsado { get; set; }
        public decimal montoPorNtCreditoUsado { get; set; }
        public string nombreMedio { get; set; }
        public string codigoMedio { get; set; }
        public decimal montoRecibido { get; set; }
        public string opBanco { get; set; }
        public string opNroCta { get; set; }
        public string opNroRef { get; set; }
        public DateTime opFecha { get; set; }
        public string opDetalle { get; set; }
        public decimal opMonto { get; set; }
        public decimal opTasa { get; set; }
        public string opAplicaConversion { get; set; }
             * */


            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\CxcCobranzaDetPorMedioPago.rdlc";
            var ds = new DS();
            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["CxcCobranzaDetPorMedioPago"].NewRow();
                rt["medio"] = it.codigoMedio + Environment.NewLine + it.nombreMedio;
                rt["montoRecibido"] = it.montoRecibido;
                rt["opMonto"] = it.opMonto;
                rt["reciboFecha"] = it.numeroRec+", "+it.fechaRec.ToShortDateString();
                rt["entidad"] = it.nombreEntidad+ Environment.NewLine+it.ciRifEntidad;
                rt["opBanco"] = it.opBanco;
                rt["opNroCta"] = it.opNroCta+", "+it.opNroRef+", "+it.notasRec;
                rt["opNroRef"] = it.opNroRef;
                rt["nota"] = it.notasRec;
                ds.Tables["CxcCobranzaDetPorMedioPago"].Rows.Add(rt);
            }
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("CxcCobranzaDetPorMedioPago", ds.Tables["CxcCobranzaDetPorMedioPago"]));
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}