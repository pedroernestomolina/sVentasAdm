using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Control.Boton.Procesar
{
    public class Imp: baseImp, IProcesar
    {
        public Imp()
            :base()
        {
        }
        public override void Opcion()
        {
            _opcion = Helpers.Msg.ProcesarGuardar();
        }
        public void setOpcion(bool p)
        {
            _opcion = p;
        }
    }
}