using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IDatosDocumento
    {

        bool HabilitarDiasValidez { get; }
        bool HabilitarDirDespacho { get; }
        bool HabilitarOrdenCompra { get; }
        bool HabilitarPedido { get; }

    }

}