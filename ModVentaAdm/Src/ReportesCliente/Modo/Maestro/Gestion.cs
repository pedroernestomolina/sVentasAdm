using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.ReportesCliente.Modo.Maestro
{
    
    public class Gestion: IGestion
    {

        private ReportesCliente.Filtro.IFiltro  _filtro;


        public ReportesCliente.Filtro.IFiltro Filtros { get { return _filtro; } }



        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(ReportesCliente.Filtro.data data)
        {
            var filtro = new OOB.ReporteCli.Maestro.Filtro();
            if (data.Grupo != null)
            {
                filtro.idGrupo = data.Grupo.Id;
            }
            if (data.Estado !=null)
            {
                filtro.idEstado = data.Estado.Id;
            }
            if (data.Zona != null)
            {
                filtro.idZona = data.Zona.Id;
            }
            if (data.Vendedor != null)
            {
                filtro.idVendedor = data.Vendedor.Id;
            }
            if (data.Cobrador != null)
            {
                filtro.idCobrador = data.Cobrador.Id;
            }
            if (data.Categoria!=null)
            {
                var desc = "";
                if (data.Categoria.Id != "00")
                    desc = data.Categoria.Descripcion;
                filtro.estCategoria = desc;
            }
            if (data.Nivel !=null)
            {
                var desc = "";
                if (data.Nivel.Id != "00")
                    desc = data.Nivel.Descripcion;
                filtro.estNivel = desc;
            }
            if (data.Tarifa!= null)
            {
                var desc = "";
                if (data.Tarifa.Id != "00")
                    desc = data.Tarifa.Descripcion;
                filtro.estTarifa= desc;
            }
            if (data.Estatus != null)
            {
                var desc = data.Estatus.Descripcion;
                filtro.estatus = desc;
            }
            if (data.Credito != null)
            {
                var desc = "1";
                if (data.Credito.Id == "02")
                    desc = "0";
                filtro.estCredito= desc;
            }

            var r01 = Sistema.MyData.ReportesCli_Maestro(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.ReporteCli.Maestro.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"ReportesCliente\Maestro.rdlc";
            var ds = new DS_CLI();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["Maestro"].NewRow();
                rt["codigo"] = it.codigo; 
                rt["nombre"] = it.ciRif +Environment.NewLine+ it.nombre;
                rt["dirFiscal"] = it.dirFiscal;
                rt["telefonos"] = it.telefono1.Trim()+", "+it.telefono2.Trim()+", "+it.celular.Trim();
                rt["estatus"] = it.estatus.Trim().ToUpper() == "ACTIVO" ? "" : "INACTIVO";
                ds.Tables["Maestro"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("Maestro", ds.Tables["Maestro"]));

            var frp = new Reportes.ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}