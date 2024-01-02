using Microsoft.Reporting.WinForms;
using ModVentaAdm.Src.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Reportes.ListaAdm
{
    public class Imp: IRepAdm
    {
        private string _filtros;
        private List<Administrador.Vistas.IdataItem> _lst;


        public Imp()
        {
            _filtros = "";
            _lst = new List<Administrador.Vistas.IdataItem>();
        }
        public void setDataCargar(IEnumerable<object> lista)
        {
            _lst.Clear();
            foreach (Administrador.Handler.dataItem rg in lista) 
            {
                _lst.Add(rg);
            }
        }
        public void setFiltrosBusq(string filtros)
        {
            _filtros = filtros;
        }
        public void Generar()
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\ClienteAnticipo\Reportes\RepAdm_AnticipoCliente.rdlc";
            var ds = new DS_AnticipoCliente();
            //
            foreach (var it in _lst.Where(w=>w.Estatus=="").ToList())
            {
                DataRow rt = ds.Tables["ListaAdm"].NewRow();
                rt["fecha"] = it.FechaMov; 
                rt["cliente"] = it.CiRif + Environment.NewLine + it.Nombre;
                rt["montoMov"] = it.MontoMov;
                rt["aplicaRet"] = it.AplicaRet;
                rt["montoRec"] = it.MontoRec;
                rt["estatus"] = it.Estatus;
                //rt["docNumero"] = it.numDoc;
                //rt["docFecha"] = it.fechaDoc;
                //rt["docNombre"] = it.nombreDoc;
                //rt["importe"] = it.importe;
                //rt["acumulado"] = it.acumulado;
                //rt["saldo"] = it.importe - it.acumulado;
                ds.Tables["ListaAdm"].Rows.Add(rt);
            }
            //
            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("ListaAdm", ds.Tables["ListaAdm"]));
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}