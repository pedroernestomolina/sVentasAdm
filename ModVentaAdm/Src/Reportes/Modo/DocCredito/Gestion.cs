using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.DocCredito
{
    public class Gestion: IGestion
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
            var filtro = new OOB.Reportes.DocCredito.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal = data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.ReportesAdm_Ventas_DocCredito(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                throw new Exception(r01.Mensaje);
            }
            Imprimir(r01.ListaD);
        }
        private void Imprimir(List<OOB.Reportes.DocCredito.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\VentasDocCredito.rdlc";
            var ds = new DS();
            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["VentasDocCredito"].NewRow();
                rt["FechaHora"] = it.fechaEmiDoc.ToShortDateString();
                rt["numeroDoc"] = it.numeroDoc;
                rt["descDoc"] = it.codigoDoc+Environment.NewLine+it.nombreDoc;
                rt["entidad"] = it.razonSocialEntidad+Environment.NewLine+it.ciRifEntidad;
                rt["montoDivisa"] = it.montoDivisa;
                ds.Tables["VentasDocCredito"].Rows.Add(rt);
            }
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("VentasDocCredito", ds.Tables["VentasDocCredito"]));
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}