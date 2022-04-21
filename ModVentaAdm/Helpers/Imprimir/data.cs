using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Helpers.Imprimir
{
    
    public class data
    {

        public class Negocio 
        {
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string CiRif { get; set; }
            public string Telefonos { get; set; }

            public Negocio()
            {
                Nombre = "";
                Direccion = "";
                CiRif = "";
                Telefonos = "";
            }
        }

        public class Encabezado
        {
            public string DocumentoNombre { get; set; }
            public string DocumentoNro { get; set; }
            public DateTime DocumentoFecha { get; set; }
            public string DocumentoControl { get; set; }
            public string DocumentoSerie { get; set; }
            public string DocumentoCondicionPago { get; set; }
            public DateTime DocumentoFechaVencimiento { get; set; }
            public int DocumentoDiasCredito { get; set; }
            public string DocumentoAplica { get; set; }

            public string NombreCli { get; set; }
            public string DireccionCli { get; set; }
            public string CiRifCli { get; set; }
            public string CodigoCli { get; set; }

            public decimal FactorCambio { get; set; }
            public decimal SubTotalNeto { get; set; }
            public decimal DescuentoPorct { get; set; }
            public decimal Descuento { get; set; }
            public decimal CargoPorct { get; set; }
            public decimal Cargo { get; set; }
            public decimal Total { get; set; }
            public decimal TotalDivisa { get; set; }

            public decimal MontoIva { get; set; }
            public decimal MontoBase { get; set; }
            public decimal MontoExento { get; set; }
            public string Notas { get; set; }
            public decimal DescuentoNeto { get; set; }
            public decimal CargoNeto { get;set;}
            public decimal SubTotal { get; set; }

            public decimal MontoBase1 { get; set; }
            public decimal MontoBase2 { get; set; }
            public decimal MontoBase3 { get; set; }
            public decimal MontoIva1 { get; set; }
            public decimal MontoIva2 { get; set; }
            public decimal MontoIva3 { get; set; }
            public decimal Tasa1 { get; set; }
            public decimal Tasa2 { get; set; }
            public decimal Tasa3 { get; set; }


            public Encabezado()
            {
                NombreCli = "";
                DireccionCli = "";
                CiRifCli = "";
                CodigoCli = "";

                DocumentoAplica = "";
                DocumentoCondicionPago = "";
                DocumentoControl = "";
                DocumentoDiasCredito = 0;
                DocumentoFecha = DateTime.Now.Date;
                DocumentoFechaVencimiento = DateTime.Now.Date;
                DocumentoNombre = "";
                DocumentoNro = "";
                DocumentoSerie = "";

                FactorCambio = 0.0m;
                SubTotalNeto = 0.0m;
                DescuentoPorct=0m;
                Descuento = 0.0m;
                DescuentoNeto = 0m;
                CargoPorct=0m;
                Cargo=0m;
                CargoNeto = 0m;
                SubTotal = 0m;
                Total = 0.0m;
                TotalDivisa = 0.0m;
                MontoBase = 0m;
                MontoExento = 0m;
                MontoIva = 0m;
                Notas = "";

                MontoBase1 = 0m;
                MontoBase2 = 0m;
                MontoBase3 = 0m;
                MontoIva1 = 0m;
                MontoIva2 = 0m;
                MontoIva3 = 0m;
                Tasa1 = 0m;
                Tasa2 = 0m;
                Tasa3 = 0m;
            }

        }

        public class Item
        {
            public string NombrePrd { get; set; }
            public string CodigoPrd { get; set; }
            public decimal Cantidad { get; set; }
            public string Empaque { get; set; }
            public int Contenido { get; set; }
            public string DepositoCodigo { get; set; }
            public string DepositoDesc { get; set; }
            public decimal Precio { get; set; }
            public decimal PrecioDivisa { get; set; }
            public decimal Importe { get; set; }
            public decimal ImporteDivisa { get; set; }
            public decimal TotalUnd { get; set; }


            public Item()
            {
                NombrePrd = "";
                CodigoPrd = "";
                Cantidad = 0.0m;
                Empaque = "";
                Contenido = 0;
                DepositoCodigo = "";
                DepositoDesc = "";
                Precio = 0.0m;
                PrecioDivisa = 0.0m;
                Importe = 0.0m;
                ImporteDivisa = 0.0m;
                TotalUnd = 0.0m;
            }
        }


        public Negocio negocio { get; set; }
        public Encabezado encabezado { get; set; }
        public List<Item> item { get; set; }

    }

}
