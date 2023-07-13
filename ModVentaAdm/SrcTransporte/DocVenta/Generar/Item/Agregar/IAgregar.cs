﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item.Agregar
{
    public interface IAgregar: IItem
    {
        void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> tasas);
        void setCliente(string idCliente);
    }
}