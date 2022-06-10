using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Maestro.Cliente.Lista
{
    
    public class Filtro
    {

        public string cadena { get; set; }
        public Enumerados.enumMetodoBusqueda metodoBusqueda { get; set; }


        public Filtro()
        {
            cadena = "";
            metodoBusqueda = Enumerados.enumMetodoBusqueda.SinDefinir;
        }

        public bool IsOK()
        {
            var rt=false;

            if (cadena.Trim() != "") 
            {
                rt = true;
            }

            return rt;
        }

    }

}