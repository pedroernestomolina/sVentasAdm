using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Configuracion
{
    
    public class Gestion
    {


        public Gestion() 
        {
        }


        public void Inicializa() 
        {
        }

        private ConfiguracionFrm frm;
        public void Inica() 
        {
            if (CargarData()) 
            {
                if (frm==null)
                {
                    frm=new ConfiguracionFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }

    }

}