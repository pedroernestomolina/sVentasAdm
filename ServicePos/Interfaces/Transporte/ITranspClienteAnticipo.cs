using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspClienteAnticipo
    {
        DtoLib.ResultadoId
            Transporte_Cliente_Anticipo_Agregar(DtoTransporte.ClienteAnticipo.Agregar.Ficha ficha);
    }
}
