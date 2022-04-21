using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Helpers.Imprimir.Grafico
{

    public class Documento: IDocumento
    {

        private data _ds;


        public Documento()
        {
        }


        public void ImprimirDoc()
        {
            Imprimir();
        }

        public void ImprimirCopiaDoc()
        {
            Imprimir();
        }

        private void Imprimir()
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Helpers\Imprimir\Grafico\Documento.rdlc";
            var ds = new ds();
            var factor = _ds.encabezado.FactorCambio;

            //NEGOCIO
            DataRow N = ds.Tables["DatosNegocio"].NewRow();
            N["Nombre"] = _ds.negocio.Nombre;
            N["CiRif"] = _ds.negocio.CiRif;
            N["Direccion"] = _ds.negocio.Direccion;
            ds.Tables["DatosNegocio"].Rows.Add(N);

            //ENCABEZADO
            DataRow E = ds.Tables["Encabezado"].NewRow();
            E["NombreCli"] = _ds.encabezado.NombreCli;
            E["DireccionCli"] = _ds.encabezado.DireccionCli;
            E["CiRifCli"] = _ds.encabezado.CiRifCli;
            E["CodigoCli"] = _ds.encabezado.CodigoCli;
            E["DocNombre"] = _ds.encabezado.DocumentoNombre;
            E["DocNro"] = _ds.encabezado.DocumentoNro;
            E["DocFecha"] = _ds.encabezado.DocumentoFecha;
            E["SubTotalNeto"] = _ds.encabezado.SubTotalNeto;
            E["DescuentoNeto"] = _ds.encabezado.DescuentoNeto;
            E["CargoNeto"] = _ds.encabezado.CargoNeto;
            E["Descuento"] = _ds.encabezado.Descuento;
            E["Cargo"] = _ds.encabezado.Cargo;
            E["Total"] = _ds.encabezado.Total;
            E["TotalDivisa"] = _ds.encabezado.TotalDivisa;
            E["Iva"] = _ds.encabezado.MontoIva;
            E["Base"] = _ds.encabezado.MontoBase;
            E["Exento"] = _ds.encabezado.MontoExento;
            E["Notas"] = _ds.encabezado.Notas;
            E["SubTotal2"] = (_ds.encabezado.SubTotal - _ds.encabezado.Descuento + _ds.encabezado.Cargo) ;
            E["SubTotalNeto2"] = (_ds.encabezado.SubTotalNeto - _ds.encabezado.DescuentoNeto + _ds.encabezado.CargoNeto) ;
            E["TextoDescuento"] = "Descuento (" + _ds.encabezado.DescuentoPorct.ToString("n2") + "%)";
            E["TextoCargo"] = "Cargo (" + _ds.encabezado.CargoPorct.ToString("n2") + "%)";
            E["factorCambio"] = _ds.encabezado.FactorCambio;
            E["montoBase1"] = _ds.encabezado.MontoBase1;
            E["montoBase2"] = _ds.encabezado.MontoBase2;
            E["montoBase3"] = _ds.encabezado.MontoBase3;
            E["montoIva1"] = _ds.encabezado.MontoIva1;
            E["montoIva2"] = _ds.encabezado.MontoIva2;
            E["montoIva3"] = _ds.encabezado.MontoIva3;
            E["tasa1"] = _ds.encabezado.Tasa1;
            E["tasa2"] = _ds.encabezado.Tasa2;
            E["tasa3"] = _ds.encabezado.Tasa3;
            E["textoBase1"] = "Monto Base ("+_ds.encabezado.Tasa1.ToString("n2")+"%): ";
            E["textoBase2"] = "Monto Base (" + _ds.encabezado.Tasa2.ToString("n2") + "%): ";
            E["textoBase3"] = "Monto Base (" + _ds.encabezado.Tasa3.ToString("n2") + "%): ";
            ds.Tables["Encabezado"].Rows.Add(E);

            //ITEM
            foreach (var rg in _ds.item)
            {
                DataRow p = ds.Tables["Item"].NewRow();
                p["NombrePrd"] = rg.NombrePrd;
                p["CodigoPrd"] = rg.CodigoPrd;
                p["Cantidad"] = rg.Cantidad;
                p["Empaque"] = rg.Empaque+Environment.NewLine+"( "+rg.Contenido.ToString().Trim()+" )";
                p["Deposito"] = rg.DepositoDesc;
                p["Precio"] = rg.Precio;
                p["PrecioDivisa"] = rg.PrecioDivisa/factor;
                p["Importe"] = rg.Importe;
                p["ImporteDivisa"] = rg.ImporteDivisa/factor;
                p["TotalUnd"] = rg.TotalUnd ;
                ds.Tables["Item"].Rows.Add(p);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            Rds.Add(new ReportDataSource("DatosNegocio", ds.Tables["DatosNegocio"]));
            Rds.Add(new ReportDataSource("Encabezado", ds.Tables["Encabezado"]));
            Rds.Add(new ReportDataSource("Item", ds.Tables["Item"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }


        public void setData(data ds)
        {
            _ds = ds;
        }

    }

}