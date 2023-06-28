using ModVentaAdm.Data.Prov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm
{
    public class Gestion
    {
        private Src.Principal.Gestion _gestionPrincipal;
        

        public Gestion()
        {
            _gestionPrincipal = new Src.Principal.Gestion();
        }


        public void Inicia()
        {
            if (CargarData()) 
            {
                _gestionPrincipal.Inicializa();
                _gestionPrincipal.Inicia();
            }
        }

        private bool CargarData()
        {
            Sistema.EquipoEstacion = Environment.MachineName;
            return true;
        }
    }
}