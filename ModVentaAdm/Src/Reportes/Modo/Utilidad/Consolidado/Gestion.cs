using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.Utilidad.Consolidado
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
            var filtro = new OOB.Reportes.Utilidad.Filtro ()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal= data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.Reportes_UtilidadConsolidado (filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD, data.GetFiltros());
        }

        private void Imprimir(List<OOB.Reportes.Utilidad.Consolidado.Ficha> list, string filtro)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\UtilidadConsolidado.rdlc";
            var ds = new DS();

            foreach (var it in list.OrderBy(o=>o.nombreSuc).ToList())
            {
                DataRow rt = ds.Tables["UtilidadConsolidado"].NewRow();
                rt["sucursal"] = it.codigoSuc.Trim() + Environment.NewLine + it.nombreSuc;
                rt["Documento"] = it.tipoDoc + Environment.NewLine + it.nombreDoc ;
                rt["costo"] = it.vCosto ;
                rt["venta"] = it.vVenta ;
                rt["utilidad"] = it.vUtilidad;
                rt["utilidadPorct"] = 100*((it.vVenta /it.vCosto)-1) ;
                ds.Tables["UtilidadConsolidado"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            pmt.Add(new ReportParameter("FILTRO", filtro));
            Rds.Add(new ReportDataSource("UtilidadConsolidado", ds.Tables["UtilidadConsolidado"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}