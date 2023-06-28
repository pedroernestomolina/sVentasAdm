using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm
{
    public class ConfiguracionMotorDatos
    {
        public string Instancia { get; set; }
        public string BaseDatos { get; set; }
        public string Usuario { get; set; }
        public string GetHost
        {
            get
            {
                return Instancia + "/" + BaseDatos;
            }
        }
    }

    
    public class Sistema
    {
        static public ConfiguracionMotorDatos MotorDatos;
        static public Fabrica.IFabrica Fabrica;

        static public IData MyData;
        public static OOB.Usuario.Entidad.Ficha Usuario;
        public static OOB.Sistema.Empresa.Entidad.Ficha DatosEmpresa;
        public static OOB.Sucursal.Entidad.Ficha Sucursal;
        public static string EquipoEstacion;
        public static string IdEquipo;

        public static string NombreHerramienta; //            
        public static string Id_SistDocumento_Factura     = "0000000001";
        public static string Id_SistDocumento_NotaDebito  = "0000000002";
        public static string Id_SistDocumento_NotaCredito = "0000000003";
        public static string Id_SistDocumento_NotaEntrega = "0000000004";
        public static string Id_SistDocumento_Presupuesto = "0000000005";
        public static string Id_SistDocumento_Pedido      = "0000000006";

        public static string Id_SistemaDocumento_NOTA_DEBITO_ADMINISTRATIVA_POC_COBRAR  = "0000000009";
        public static string Id_SistemaDocumento_NOTA_CREDITO_ADMINISTRATIVA_POC_COBRAR = "0000000010";
    }
}