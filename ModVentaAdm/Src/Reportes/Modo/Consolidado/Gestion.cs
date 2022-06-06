using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.Consolidado
{

    public class Gestion : IGestion
    {

        private Reportes.Filtro.IFiltro _filtro;


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
            var filtro = new OOB.Reportes.Consolidado.Filtro()
            {
                codSucursal = data.GetCodigoSucursal,
                desde = data.GetDesde,
                hasta = data.GetHasta,
            };
            var r01 = Sistema.MyData.Reportes_Consolidado(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD, data.GetFiltros());
        }

        private void Imprimir(List<OOB.Reportes.Consolidado.Ficha> list, string filt)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\Consolidado.rdlc";
            var ds = new DS();

            var lg = list.GroupBy(g => new { g.fecha, g.codigoSuc, g.nombreSuc, g.caja, g.tipo, g.docNombre, g.aplica }).
                Select(g => new data
                {
                    fecha = g.Key.fecha,
                    codigoSuc = g.Key.codigoSuc,
                    nombreSuc = g.Key.nombreSuc,
                    caja = g.Key.caja,
                    tipo = g.Key.tipo,
                    aplica = g.Key.aplica,
                    docNombre = g.Key.docNombre,
                    inicio = g.Min(gg => gg.documento),
                    fin = g.Max(gg => gg.documento),
                    total = g.Sum(gg => gg.total * gg.signo),
                    totalDivisa = g.Sum(gg => gg.totalDivisa * gg.signo),
                }).OrderBy(o => o.fecha).ThenBy(o => o.nombreSuc).ThenBy(o => o.tipo).ToList();

            foreach (var it in lg.ToList())
            {
                DataRow rt = ds.Tables["Consolidado"].NewRow();
                rt["fecha"] = it.fecha.Date;
                rt["sucursal"] = "(Cod: " + it.codigoSuc.Trim() + ") " + it.nombreSuc;
                rt["caja"] = it.caja.ToString().Trim().PadLeft(2,'0');
                rt["docNombre"] = it.docNombre.Trim();
                rt["inicio"] = it.inicio;
                rt["fin"] = it.fin;
                rt["total"] = it.total;
                rt["totalDivisa"] = it.totalDivisa;
                rt["aplica"] = it.aplica.Trim();
                ds.Tables["Consolidado"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("Consolidado", ds.Tables["Consolidado"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}