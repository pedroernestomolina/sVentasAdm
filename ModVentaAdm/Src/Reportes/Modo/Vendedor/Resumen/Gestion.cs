using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.Vendedor.Resumen
{
    public class Gestion: IGestion
    {
        private Reportes.Filtro.IFiltro _filtro;


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
            try
            {
                var filtro = new OOB.Reportes.Vendedor.Filtro()
                {
                    desde = data.GetDesde,
                    hasta = data.GetHasta,
                    codSucursal = data.GetCodigoSucursal,
                };
                var _filtrar = data.GetFiltros();
                var r01 = Sistema.MyData.ReportesAdm_VentasPorVendedor_Resumen(filtro);
                Imprimir(r01.ListaD, _filtrar);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }
        private void Imprimir(List<OOB.Reportes.Vendedor.Resumen.Ficha> list, string filtrar)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\VentasPorVendedor_Resumen.rdlc";
            var ds = new DS();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["VentaxPorVendedorResumen"].NewRow();
                rt["vendedor"] = "( " + it.codigoVend.Trim() + " )" + Environment.NewLine + it.nombreVend.Trim();
                rt["cntDoc"] = it.cntDoc;
                rt["netoMonLocal"] = it.netoMonLocal;
                rt["netoDivisa"] = it.netoDivisa;
                ds.Tables["VentaxPorVendedorResumen"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            pmt.Add(new ReportParameter("FILTRAR", filtrar));
            Rds.Add(new ReportDataSource("VentaxPorVendedorResumen", ds.Tables["VentaxPorVendedorResumen"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}