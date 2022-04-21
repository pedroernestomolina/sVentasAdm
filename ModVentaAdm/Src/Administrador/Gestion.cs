using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Administrador
{
    
    public class Gestion
    {

        private IGestion _miGestion;


        public Gestion()
        {
        }


        public void setGestion(IGestion gestion)
        {
            _miGestion = gestion;
        }

        public void Inicializa()
        {
            _miGestion.Inicializa();
        }

        public void Inicia()
        {
            _miGestion.Inicia();
        }

    }

}