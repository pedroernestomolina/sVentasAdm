﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspCaja
    {
        DtoLib.ResultadoLista<DtoTransporte.Caja.Lista.Ficha>
            Transporte_Caja_GetLista();
    }
}