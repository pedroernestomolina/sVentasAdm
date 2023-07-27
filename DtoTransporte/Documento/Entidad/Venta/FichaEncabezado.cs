﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Venta
{
    public class FichaEncabezado: baseEncabezado
    {
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public FichaEncabezado()
            :base()
        {
            docSolicitadoPor = "";
            docModuloCargar = "";
        }
    }
}