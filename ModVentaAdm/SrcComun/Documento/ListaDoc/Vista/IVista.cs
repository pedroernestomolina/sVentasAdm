﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaDoc.Vista
{
    public interface IVista: Src.IGestion
    {
        Utils.Control.Boton.Salir.ISalir BtSalida { get; }
        IListaDoc ListaDoc { get; }
    }
}