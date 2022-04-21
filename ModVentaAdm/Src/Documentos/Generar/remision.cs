using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public class remision
    {

        private decimal _total;


        public string autoProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoSubGrupo { get; set; }
        public string autoTasaIva { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal cantidad { get; set; }
        public decimal precioNeto { get; set; }
        public string tarifaPrecio { get; set; }
        public decimal tasaIva { get; set; }
        public string tipoIva { get; set; }
        public string categroiaProducto { get; set; }
        public string decimalesProducto { get; set; }
        public string empaqueDesc { get; set; }
        public int empaqueCont { get; set; }
        public string estatusPesadoProducto { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromd { get; set; }
        public decimal costoPromdUnd { get; set; }
        public decimal dsctoPorct { get; set; }
        public string notas { get; set; }
        public decimal total { get { return _total; } }
        public decimal cantidadUnd { get; set; }
        public string autoDeposito { get; set; }


        public remision() 
        {
            autoDepartamento = "";
            autoGrupo = "";
            autoProducto = "";
            autoSubGrupo = "";
            autoTasaIva = "";
            codigoProducto = "";
            nombreProducto = "";
            cantidad = 0m;
            precioNeto = 0m;
            tarifaPrecio = "";
            tasaIva = 0m;
            tipoIva = "";
            categroiaProducto = "";
            decimalesProducto = "";
            empaqueCont = 0;
            empaqueDesc = "";
            estatusPesadoProducto = "";
            costo = 0m;
            costoPromd = 0m;
            costoPromdUnd = 0m;
            costoUnd = 0m;
            dsctoPorct = 0m;
            notas = "";
            autoDeposito = "";
            cantidadUnd = 0m;
        }

        public remision(OOB.Documento.Entidad.FichaItem it)
        {
            autoDepartamento = it.AutoDepartamento;
            autoGrupo = it.AutoGrupo;
            autoProducto = it.AutoProducto;
            autoSubGrupo = it.AutoSubGrupo;
            autoTasaIva = it.AutoTasa;
            codigoProducto = it.Codigo;
            nombreProducto = it.Nombre;
            cantidad = it.Cantidad;
            precioNeto = it.PrecioNeto;
            tarifaPrecio = it.Tarifa;
            tasaIva = it.Tasa;
            tipoIva = "";
            categroiaProducto = it.Categoria;
            decimalesProducto = it.Decimales;
            empaqueCont = it.ContenidoEmpaque;
            empaqueDesc = it.Empaque;
            estatusPesadoProducto = it.EstatusPesado;
            costo = it.CostoCompra;
            costoPromd = 0m;
            costoPromdUnd = it.CostoPromedioUnd;
            costoUnd = it.CostoUnd;
            dsctoPorct = it.Descuento1p;
            notas = it.Detalle;
            autoDeposito = it.AutoDeposito;
            cantidadUnd = it.CantidadUnd;
            Calcula();
        }

        private void Calcula()
        {
            var m = precioNeto * cantidad;
            var _mDscto = m * dsctoPorct / 100;
            m -= _mDscto;
            var _mIva = m * tasaIva / 100;
            _mIva = Math.Round(_mIva, 2, MidpointRounding.AwayFromZero);
            _total = m + _mIva;
        }

    }

}