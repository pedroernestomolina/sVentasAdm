﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ITranspCxCMovCobro
    {
        DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha>
            Transporte_CxcMovCobro_GetLista(DtoTransporte.CxcMovCobro.ListaMov.Filtro filtro);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Anular.Ficha>
            Transporte_CxcMovCobro_Anular_ObtenerData(string idRecibo);
        DtoLib.Resultado
            Transporte_CxcMovCobro_Anular(DtoTransporte.CxcMovCobro.Anular.Ficha ficha);
    }
}