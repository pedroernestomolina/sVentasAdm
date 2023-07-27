﻿using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen> 
            TransporteReporte_AliadoResumen()
        {
            return ServiceProv.TransporteReporte_AliadoResumen();
        }
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc> 
            TransporteReporte_AliadoDetalleDoc()
        {
            return ServiceProv.TransporteReporte_AliadoDetalleDoc();
        }
    }
}