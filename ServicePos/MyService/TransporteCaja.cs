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
        public DtoLib.ResultadoLista<DtoTransporte.Caja.Lista.Ficha> 
            Transporte_Caja_GetLista()
        {
            return ServiceProv.Transporte_Caja_GetLista();
        }
    }
}