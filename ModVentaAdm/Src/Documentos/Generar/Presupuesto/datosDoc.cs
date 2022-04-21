using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.Presupuesto
{

    public class datosDoc: IDatosDocumento
    {

        public bool HabilitarDiasValidez { get { return true; } }
        public bool HabilitarDirDespacho{ get { return true; } }
        public bool HabilitarOrdenCompra { get { return false; } }
        public bool HabilitarPedido { get { return false; } }

    }

}