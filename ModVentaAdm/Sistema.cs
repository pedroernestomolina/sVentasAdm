using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm
{
    
    public class Sistema
    {

        static public IData MyData;
        static public string Instancia;
        static public string BaseDatos;
        public static OOB.Usuario.Entidad.Ficha Usuario;
        public static OOB.Sistema.Empresa.Entidad.Ficha DatosEmpresa;
        public static OOB.Sucursal.Entidad.Ficha Sucursal;
        public static string EquipoEstacion;
        public static string IdEquipo;

        public static string Id_SistDocumento_Factura     = "0000000001";
        public static string Id_SistDocumento_NotaCredito = "0000000003";
        public static string Id_SistDocumento_NotaEntrega = "0000000004";
        public static string Id_SistDocumento_Presupuesto = "0000000005";
        public static string Id_SistDocumento_Pedido      = "0000000006";

    }

}