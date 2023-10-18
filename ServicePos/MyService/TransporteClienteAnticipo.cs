using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoId 
            Transporte_Cliente_Anticipo_Agregar(DtoTransporte.ClienteAnticipo.Agregar.Ficha ficha)
        {
            return ServiceProv.Transporte_Cliente_Anticipo_Agregar(ficha);
        }
    }
}
