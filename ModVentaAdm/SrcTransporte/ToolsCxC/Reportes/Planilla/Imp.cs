using Microsoft.Reporting.WinForms;
using ModVentaAdm.Src.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Reportes.Planilla
{
    public class Imp : IRepPlanilla
    {
        private string _idMov;

        public Imp()
        {
            _idMov = "";
        }
        public void setItemCargar(object idMov)
        {
            _idMov = (string)idMov;
        }
        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteReporte_Cxc_CobroEmitido_Planilla(_idMov);
                imprimir(r01.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        private void imprimir(OOB.Transporte.Reporte.Cxc.PlanillaCobro.Ficha ficha)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"srcTransporte\ToolsCxC\Reportes\RepPlanilla_CxcRecPago.rdlc";
            var ds = new DS_TOOLS_CXC();
            //
            DataRow rt = ds.Tables["CxcRecDoc"].NewRow();
            rt["reciboNro"] = ficha.reciboNro;
            rt["fecha"] = ficha.fechaMov;
            rt["tasaCambio"] = ficha.tasaCambio;
            rt["montoPago"] = ficha.importeDiv;
            rt["notas"] = ficha.notasMov;
            rt["proveedor"] = ficha.ciRifProv + Environment.NewLine + ficha.nombreProv;
            rt["isAnulado"] = ficha.estatusMov.Trim().ToUpper() == "1" ? "ANULADO" : "";
            ds.Tables["CxcRecDoc"].Rows.Add(rt);
            //
            var _montoDiv = 0m;
            foreach (var sv in ficha.caja)
            {
                _montoDiv = sv.monto;
                if (sv.esDivisa.Trim().ToUpper() != "1")
                {
                    _montoDiv /= ficha.tasaCambio;
                }
                DataRow rtCja = ds.Tables["CxcRecDoc_Caja"].NewRow();
                rtCja["desc"] = "( " + sv.cjCod.Trim() + " ) " + sv.cjDesc;
                rtCja["monto"] = sv.monto;
                rtCja["esDivisa"] = sv.esDivisa.Trim().ToUpper() == "1" ? "$" : "";
                rtCja["montoDiv"] = _montoDiv;
                ds.Tables["CxcRecDoc_Caja"].Rows.Add(rtCja);
            }
            foreach (var sv in ficha.doc)
            {
                DataRow rtDoc = ds.Tables["CxcRecDoc_Doc"].NewRow();
                rtDoc["documento"] = sv.numeroDoc;
                rtDoc["fecha"] = sv.fechaEmisionDoc;
                rtDoc["siglas"] = sv.siglasDoc;
                rtDoc["montoDiv"] = sv.montoDiv;
                ds.Tables["CxcRecDoc_Doc"].Rows.Add(rtDoc);
            }
            foreach (var sv in ficha.metPago)
            {
                DataRow rtDoc = ds.Tables["CxcRecDoc_MetPag"].NewRow();
                rtDoc["metodo"] = sv.descMet;
                rtDoc["monto"] = sv.opMonto;
                rtDoc["fecha"] = sv.opFecha;
                rtDoc["referencia"] = "Banco: "+sv.opBanco.Trim()+", Lote:"+sv.opLote.Trim()+
                    ", Ref:"+sv.opRef.Trim()+ ", Cta Nro:"+sv.opNroCta.Trim()+", Tranf Nro:"+
                    sv.opNroTransf.Trim();
                ds.Tables["CxcRecDoc_MetPag"].Rows.Add(rtDoc);
            }
            DataRow rtDoc2 = ds.Tables["CxcRecDoc_MetPag"].NewRow();
            rtDoc2["metodo"] = "ANTICIPO";
            rtDoc2["monto"] = ficha.montoPorAnticipo;
            rtDoc2["fecha"] = ficha.fechaMov;
            rtDoc2["referencia"] = "ANTICIPO RECIBIDO";
            ds.Tables["CxcRecDoc_MetPag"].Rows.Add(rtDoc2);
            //
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMP_CIRIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMP_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMP_DIR", Sistema.Negocio.DireccionFiscal));
            Rds.Add(new ReportDataSource("CxcRecDoc", ds.Tables["CxcRecDoc"]));
            Rds.Add(new ReportDataSource("CxcRecDoc_Doc", ds.Tables["CxcRecDoc_Doc"]));
            Rds.Add(new ReportDataSource("CxcRecDoc_Caja", ds.Tables["CxcRecDoc_Caja"]));
            Rds.Add(new ReportDataSource("CxcRecDoc_MetPag", ds.Tables["CxcRecDoc_MetPag"]));
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}