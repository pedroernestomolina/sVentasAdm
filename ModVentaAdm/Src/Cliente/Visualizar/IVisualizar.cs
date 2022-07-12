using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Visualizar
{
    
    public interface IVisualizar: IGestion
    {

        void setFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha);

    }

}