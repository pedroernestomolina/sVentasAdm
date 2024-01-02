using Microsoft.Reporting.WinForms;
using ModVentaAdm.Src.Reportes;
using ModVentaAdm.SrcTransporte.ToolsCxC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Reportes.Planilla
{
    public class Imp: IRepPlanilla
    {
        private int _idMov;

        public Imp()
        {
            _idMov = -1;
        }
        public void setItemCargar(object idMov)
        {
            _idMov = (int)idMov;
        }
        public void Generar()
        {
            try
            {
                var r01 = Sistema.MyData.Transporte_Cliente_Anticipo_Movimiento_GetById(_idMov);
                imprimir(r01.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        private void imprimir(OOB.Transporte.ClienteAnticipo.Entidad.Ficha ficha)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\ClienteAnticipo\Reportes\RepPlanilla_AnticipoCliente.rdlc";
            var ds = new DS_AnticipoCliente();
            //
            var mov = ficha.Mov;
            DataRow rt = ds.Tables["AnticipoMov"].NewRow();
            rt["reciboNro"] = mov.reciboNro ;
            rt["fecha"] = mov.fechaEmision;
            rt["tasaFactor"] = mov.tasaFactor;
            rt["montoPago"] = mov.montoMovMonDiv ;
            rt["cliente"] = mov.ciRifCliente+ Environment.NewLine + mov.nombreCliente;
            rt["motivo"] = mov.motivo;
            rt["retTasa"] = mov.tasaRet;
            rt["retSustraendo"] = mov.sustraendoRet;
            rt["retencion"] = mov.montoRet;
            rt["retMonto"] = mov.totalRet / ficha.Mov.tasaFactor;
            rt["montoPagado"] = mov.montoRecMonDiv;
            rt["isAnulado"] = "";
            ds.Tables["AnticipoMov"].Rows.Add(rt);
            foreach (var sv in ficha.CajMov)
            {
                DataRow rtDt = ds.Tables["AnticipoCaj"].NewRow();
                rtDt["desc"] = "(" + sv.cjCodigo.Trim() + ")" + sv.cjDesc.Trim();
                rtDt["monto"] = sv.monto;
                rtDt["esDivisa"] = sv.esDivisa.Trim().ToUpper() == "1" ? "$" : "";
                ds.Tables["AnticipoCaj"].Rows.Add(rtDt);
            }
            //
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMP_CIRIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMP_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMP_DIR", Sistema.Negocio.DireccionFiscal));
            Rds.Add(new ReportDataSource("AnticipoMov", ds.Tables["AnticipoMov"]));
            Rds.Add(new ReportDataSource("AnticipoCaj", ds.Tables["AnticipoCaj"]));
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}