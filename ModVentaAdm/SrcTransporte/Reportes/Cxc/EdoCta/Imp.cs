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
    public class Imp : IReporteConFiltroMasFecha
    {
        private Filtro.Vista.IFiltro _filtros;
        private DateTime _fechaInicio;
        //
        public DateTime Desde { get { return _fechaInicio; } }
        //
        public Imp()
        {
            _fechaInicio = DateTime.Now.Date;
        }
        public void setFiltros(Filtro.Vista.IFiltro filtros)
        {
            _filtros = filtros;
        }
        public void setDesde(DateTime fecha)
        {
            _fechaInicio = fecha;
        }
        public void setFiltros(SrcTransporte.Filtro.Vistas.IdataFiltrar dataFiltrar)
        {
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
        //
        private void Imprimir(OOB.Transporte.Reporte.Cxc.EdoCta.Ficha ficha)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Cxc_EdoCtaCliente.rdlc";
            var ds = new DS_TRANSP();

            DataRow rt_enc = ds.Tables["EdoCta_Enc"].NewRow();
            rt_enc["cliente"] = ficha.entidad.ciRifCli + Environment.NewLine + ficha.entidad.nombreCli + Environment.NewLine + ficha.entidad.dirCli+
                Environment.NewLine+" Monto Por Anticipos A Favor: "+ficha.entidad.montoAnticipos.ToString("n2")+
                Environment.NewLine+" Monto Por Notas De Credito A Favor: "+ficha.entidad.montoNtCredito.ToString("n2");
            ds.Tables["EdoCta_Enc"].Rows.Add(rt_enc);
            //
            var _importe = 0m;
            var _signo = "";
            var _saldo = 0m;
            var _lst2 = ficha.movimientos.OrderBy(o => o.idDoc).ThenBy(o => o.fechaDoc).ToList();

            var _lst = ficha.movimientos.ToList();
            _lst = _lst.OrderBy(o => o.fechaDoc).ToList();

            foreach (var it in _lst.Where(w=>w.fechaDoc<_fechaInicio).ToList())
            {
                _importe = it.importeDiv * it.signoDoc;
                _signo = "+";
                if (it.signoDoc < 0)
                {
                    _signo = "-";
                    if (it.tipoDoc.Trim().ToUpper() == "PAG")
                    {
                        _saldo += _importe;
                    }
                    else
                    {
                        _signo = "";
                    }
                }
                else
                {
                    _saldo += _importe;
                }
                if (_saldo < 0m)
                {
                    _saldo = 0m;
                }
            }
            DataRow rt = ds.Tables["EdoCta"].NewRow();
            rt["fechaDoc"] = _fechaInicio.AddDays(-1);
            rt["nroDoc"] = "";
            rt["tipoDoc"] = "";
            rt["importe"] = 0m;
            rt["signo"] = _signo;
            rt["notas"] = "";
            rt["saldo"] = _saldo;
            ds.Tables["EdoCta"].Rows.Add(rt);

            //
            foreach (var it in _lst.Where(w=>w.fechaDoc>=_fechaInicio).ToList()) 
            {
                _importe = it.importeDiv * it.signoDoc;
                _signo = "+";
                if (it.signoDoc < 0)
                {
                    _signo = "-";
                    if (it.tipoDoc.Trim().ToUpper() == "PAG")
                    {
                        _saldo += _importe;
                    }
                    else 
                    {
                        _signo = "";
                    }
                }
                else 
                {
                    _saldo += _importe;
                }
                if (_saldo < 0m)
                {
                    _saldo = 0m;
                }
                DataRow rt2 = ds.Tables["EdoCta"].NewRow();
                rt2["fechaDoc"] = it.fechaDoc ;
                rt2["nroDoc"] = it.nroDoc;
                rt2["tipoDoc"] = it.tipoDoc;
                rt2["importe"] = it.importeDiv;
                rt2["signo"] = _signo;
                rt2["notas"] = it.notasDoc;
                rt2["saldo"] = _saldo;
                ds.Tables["EdoCta"].Rows.Add(rt2);
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
    }
}