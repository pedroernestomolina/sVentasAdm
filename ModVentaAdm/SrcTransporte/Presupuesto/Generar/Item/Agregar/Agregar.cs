using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.Agregar
{
    public class Agregar: ImpItem, IAgregar
    {
        public Agregar()
            :base()
        {
        }
        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Item.VerificarDatosIsOK()) 
            {
                var r= Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    _procesarIsOK = true;
                }
            }
        }
    }
}