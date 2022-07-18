using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.Reportes.ListaDocPend
{
    
    public class RepDocPend: IRepDocPend
    {


        private List<DocumentosPend.ListaDocPend.data> _lst;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public RepDocPend()
        {
        }


        public void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha )
        {
            _cliente = ficha;
        }
        public void setListaDoc(List<DocumentosPend.ListaDocPend.data> lst)
        {
            _lst = lst;
        }

        public void Generar()
        {
            Imprimir();
        }

        private void Imprimir()
        {
            if (_lst == null)
                return;

            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Src\CxC\Tools\Reportes\ListaDocPend.rdlc";
            var ds = new DS_CxC();
            var _cli = "";
            if (_cliente !=null)
            {
                _cli = "Cliente: "+_cliente.ciRif.Trim() + Environment.NewLine;
                _cli += _cliente.razonSocial.Trim()+Environment.NewLine;
                _cli += _cliente.dirFiscal.Trim();
            }
            DataRow rt0 = ds.Tables["ListaDocPendEnc"].NewRow();
            rt0["cl_nombre"] = _cli;
            ds.Tables["ListaDocPendEnc"].Rows.Add(rt0);
            foreach (var it in _lst.ToList())
            {
                var _montoImporte = it.importeDoc * it.signoDoc;
                var _montoAcumulado = it.acumuladoDoc * it.signoDoc;
                var _montoResta = _montoImporte - _montoAcumulado;

                DataRow rt = ds.Tables["ListaDocPend"].NewRow();
                rt["documento"] = it.numeroDoc;
                rt["tipo"] = it.tipoDoc;
                rt["fechaEmision"] = it.fechaEmisionDoc;
                rt["fechaVence"] = it.fechaVencDoc;
                rt["diasVencida"] = it.diasVencida <= 0 ? "Por Vencer" : it.diasVencida.ToString()+" Dias";
                rt["importe"] = _montoImporte ;
                rt["tasa"] = it.tasaCambioDoc;
                rt["abonado"] = _montoAcumulado;
                rt["resta"] = _montoResta;
                ds.Tables["ListaDocPend"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.DatosEmpresa.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.DatosEmpresa.Nombre));
            //pmt.Add(new ReportParameter("Filtros", _filtros));
            Rds.Add(new ReportDataSource("ListaDocPend", ds.Tables["ListaDocPend"]));
            Rds.Add(new ReportDataSource("ListaDocPendEnc", ds.Tables["ListaDocPendEnc"]));
            var frp = new ModVentaAdm.Src.Reportes.ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}