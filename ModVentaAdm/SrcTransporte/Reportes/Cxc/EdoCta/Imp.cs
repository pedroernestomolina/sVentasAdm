using Microsoft.Reporting.WinForms;
using ModVentaAdm.Src.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Cxc.EdoCta
{
    public class Imp : IReporteConFiltro
    {
        private Filtro.Vista.IFiltro _filtros;


        public Imp()
        {
        }
        public void setFiltros(Filtro.Vista.IFiltro filtros)
        {
            _filtros = filtros;
        }
        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteReporte_Cxc_EdoCta(_filtros.idCliente);
                Imprimir(r01.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }

        private void Imprimir(OOB.Transporte.Reporte.Cxc.EdoCta.Ficha ficha)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Cxc_EdoCtaCliente.rdlc";
            var ds = new DS_TRANSP();

            DataRow rt_enc = ds.Tables["EdoCta_Enc"].NewRow();
            rt_enc["cliente"] = ficha.entidad.ciRifCli + Environment.NewLine + ficha.entidad.nombreCli + Environment.NewLine + ficha.entidad.dirCli;
            ds.Tables["EdoCta_Enc"].Rows.Add(rt_enc);
            //
            var _importe = 0m;
            var _signo = "";
            var _saldo = 0m;
            foreach (var it in ficha.movimientos)
            {
                _importe = it.importeDiv * it.signoDoc;
                _signo = "+";
                if (it.signoDoc < 0)
                {
                    _signo = "-";
                }
                _saldo += _importe;
                DataRow rt = ds.Tables["EdoCta"].NewRow();
                rt["fechaDoc"] = it.fechaDoc ;
                rt["nroDoc"] = it.nroDoc;
                rt["tipoDoc"] = it.tipoDoc;
                rt["importe"] = it.importeDiv;
                rt["signo"] = _signo;
                rt["notas"] = it.notasDoc;
                rt["saldo"] = _saldo;
                ds.Tables["EdoCta"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("EdoCta_Enc", ds.Tables["EdoCta_Enc"]));
            Rds.Add(new ReportDataSource("EdoCta", ds.Tables["EdoCta"]));
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }


        public void setFiltros(SrcTransporte.Filtro.Vistas.IdataFiltrar dataFiltrar)
        {
        }
    }
}