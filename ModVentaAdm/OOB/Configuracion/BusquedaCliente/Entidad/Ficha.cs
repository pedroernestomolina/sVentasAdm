using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Configuracion.BusquedaCliente.Entidad
{

    public class Ficha
    {

        public string Usuario { get; set; }
        public Enumerado.EnumPreferenciaBusqueda ModoBusqueda
        {
            get
            {
                var modo = Enumerado.EnumPreferenciaBusqueda.SnDefinir;
                switch (Usuario.Trim().ToUpper())
                {
                    case "CODIGO":
                        modo = Enumerado.EnumPreferenciaBusqueda.Codigo;
                        break;
                    case "NOMBRE":
                        modo = Enumerado.EnumPreferenciaBusqueda.Nombre;
                        break;
                    case "CI/RIF":
                        modo = Enumerado.EnumPreferenciaBusqueda.CiRif;
                        break;
                }
                return modo;
            }
        }


        public Ficha()
        {
            Usuario = "";
        }

    }

}