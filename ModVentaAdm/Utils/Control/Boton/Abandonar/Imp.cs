using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Control.Boton.Abandonar
{
    public class Imp: baseImp, IAbandonar
    {
        public Imp()
            :base()
        {
        }
        public override void Opcion()
        {
            _opcion = Helpers.Msg.Abandonar();
        }
    }
}